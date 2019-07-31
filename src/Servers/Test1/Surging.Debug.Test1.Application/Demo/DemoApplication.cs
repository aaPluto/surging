using Surging.Core.AutoMapper;
using Surging.Core.CPlatform.Runtime.Session;
using Surging.Core.Dapper.Repositories;
using Surging.Core.ProxyGenerator;
using Surging.Debug.Test1.Domain.Demo.Entities;
using Surging.Debug.Test1.Domain.UserInfo;
using Surging.Debug.Test1.IApplication.Demo;
using Surging.Debug.Test1.IApplication.Demo.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Surging.Debug.Test1.Application.Demo
{
    public class DemoApplication : ProxyServiceBase, IDemoApplication
    {
        private readonly ISurgingSession _surgingSession;

        public DemoApplication()
        {
            _surgingSession = NullSurgingSession.Instance;
        }


        public async Task<string> GetUserName(QueryUserInput input)
        {
            var id = await GetService<IServiceProxyProvider>().Invoke<string>(new Dictionary<string, object>() {
                { "id", Guid.NewGuid().ToString()},

            }, "v1/api/debug/demo/getuserid");
            return input.UserId + Guid.NewGuid() + id;
        }

        public async Task<string> GetUserId(string id)
        {
            var c = Surging.Core.CPlatform.AppConfig.CacheSectionOptions;
            return id;
        }

        public async Task CreatDemo(DemoInput input)
        {
            var demoRepository = GetService<IDapperRepository<DemoEntity, string>>();
            var entity = input.MapTo<DemoEntity>();
            await demoRepository.InsertAsync(entity);
        }

        public async Task<string> CreateUser()
        {
            //var userRepositroy = GetService<IDapperRepository<UserInfo, long>>();
            //await userRepositroy.InsertAsync(new UserInfo()
            //{
            //    Email = "1111",
            //    Password = "123qwe",
            //    EmployeeId = 1,
            //    Phone = "1111",
            //    UserName = "sdsds"
            //});

            var loginUserId = _surgingSession.UserId;

            return "OK";
        }
    }
}
