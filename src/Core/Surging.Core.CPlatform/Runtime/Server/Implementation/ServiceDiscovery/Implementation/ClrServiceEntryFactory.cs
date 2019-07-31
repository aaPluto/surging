using Surging.Core.CPlatform.Convertibles;
using Surging.Core.CPlatform.DependencyResolution;
using Surging.Core.CPlatform.Filters.Implementation;
using Surging.Core.CPlatform.Ids;
using Surging.Core.CPlatform.Routing.Template;
using Surging.Core.CPlatform.Runtime.Server.Implementation.ServiceDiscovery.Attributes;
using Surging.Core.CPlatform.Serialization;
using Surging.Core.CPlatform.Transport.Implementation;
using Surging.Core.CPlatform.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using static Surging.Core.CPlatform.Utilities.FastInvoke;

namespace Surging.Core.CPlatform.Runtime.Server.Implementation.ServiceDiscovery.Implementation
{
    /// <summary>
    /// Clr������Ŀ������
    /// </summary>
    public class ClrServiceEntryFactory : IClrServiceEntryFactory
    {
        #region Field

        private readonly CPlatformContainer _serviceProvider;
        private readonly IServiceIdGenerator _serviceIdGenerator;
        private readonly ITypeConvertibleService _typeConvertibleService;

        #endregion Field

        #region Constructor

        public ClrServiceEntryFactory(CPlatformContainer serviceProvider, IServiceIdGenerator serviceIdGenerator, ITypeConvertibleService typeConvertibleService)
        {
            _serviceProvider = serviceProvider;
            _serviceIdGenerator = serviceIdGenerator;
            _typeConvertibleService = typeConvertibleService;
        }

        #endregion Constructor

        #region Implementation of IClrServiceEntryFactory

        /// <summary>
        /// ����������Ŀ��
        /// </summary>
        /// <param name="service">�������͡�</param>
        /// <param name="serviceImplementation">����ʵ�����͡�</param>
        /// <returns>������Ŀ���ϡ�</returns>
        public IEnumerable<ServiceEntry> CreateServiceEntry(Type service)
        {
            var routeTemplate = service.GetCustomAttribute<ServiceBundleAttribute>();
            foreach (var methodInfo in service.GetTypeInfo().GetMethods())
            {
                yield return Create(methodInfo, service.Name, routeTemplate.RouteTemplate);
            }
        }

        #endregion Implementation of IClrServiceEntryFactory

        #region Private Method

        private ServiceEntry Create(MethodInfo method, string serviceName, string routeTemplate)
        {
            var serviceId = _serviceIdGenerator.GenerateServiceId(method);
            var attributes = method.GetCustomAttributes().ToList();
            var serviceDescriptor = new ServiceDescriptor
            {
                Id = serviceId,
                RoutePath = RoutePatternParser.Parse(routeTemplate, serviceName, method.Name)
            };
            var descriptorAttributes = method.GetCustomAttributes<ServiceDescriptorAttribute>();
            foreach (var descriptorAttribute in descriptorAttributes)
            {
                descriptorAttribute.Apply(serviceDescriptor);
            }
            serviceDescriptor.EnableAuthorization(true);
            serviceDescriptor.AuthType(AuthorizationType.JWT);

            //var authorization = attributes.Where(p => p is AuthorizationFilterAttribute).FirstOrDefault();
            //if (authorization != null)
            //    serviceDescriptor.EnableAuthorization(true);
            //if (authorization != null)
            //{
            //    serviceDescriptor.AuthType(((authorization as AuthorizationAttribute)?.AuthType)
            //        ?? AuthorizationType.AppSecret);
            //}
            var fastInvoker = GetHandler(serviceId, method);
            return new ServiceEntry
            {
                Descriptor = serviceDescriptor,
                RoutePath = serviceDescriptor.RoutePath,
                MethodName = method.Name,
                Type = method.DeclaringType,
                Attributes = attributes,
                Func = (key, parameters) =>
             {
                 var instance = _serviceProvider.GetInstances(key, method.DeclaringType);
                 var list = new List<object>();

                 foreach (var parameterInfo in method.GetParameters())
                 {
                     //�����Ƿ���Ĭ��ֵ���жϣ���Ĭ��ֵ�������û�û����ȡĬ��ֵ
                     if (parameterInfo.HasDefaultValue && !parameters.ContainsKey(parameterInfo.Name))
                     {
                         list.Add(parameterInfo.DefaultValue);
                         continue;
                     }
                     var value = parameters[parameterInfo.Name];
                     var parameterType = parameterInfo.ParameterType;
                     if (value != null)
                     {
                         var parameter = _typeConvertibleService.Convert(value, parameterType);
                         list.Add(parameter);
                     }
                     else
                     {
                         list.Add(value);
                     }
                     if (parameters.ContainsKey("payload"))
                     {
                         if (RpcContext.GetContext().GetAttachment("payload") == null)
                         {
                             var serializer = ServiceLocator.GetService<ISerializer<string>>();
                             var payloadString = serializer.Serialize(parameters["payload"], true);
                             RpcContext.GetContext().SetAttachment("payload", payloadString);
                         }
                     }

                 }
                 var result = fastInvoker(instance, list.ToArray());
                 return Task.FromResult(result);
             }
            };
        }

        private FastInvokeHandler GetHandler(string key, MethodInfo method)
        {
            var objInstance = ServiceResolver.Current.GetService(null, key);
            if (objInstance == null)
            {
                objInstance = FastInvoke.GetMethodInvoker(method);
                ServiceResolver.Current.Register(key, objInstance, null);
            }
            return objInstance as FastInvokeHandler;
        }

        #endregion Private Method
    }
}