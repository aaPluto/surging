﻿using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Surging.Core.SwaggerGen
{
    public class XmlCommentsMemberNameHelper
    {
        public static string GetMemberNameForMethod(MethodInfo method)
        {
            var builder = new StringBuilder("M:");

            builder.Append(QualifiedNameFor(method.DeclaringType));
            builder.Append($".{method.Name}");

            var parameters = method.GetParameters();
            if (parameters.Any())
            {
                var parametersNames = parameters.Select(p =>
                {
                    return p.ParameterType.IsGenericParameter
                        ? $"`{p.ParameterType.GenericParameterPosition}"
                        : QualifiedNameFor(p.ParameterType, expandGenericArgs: true);
                });
                builder.Append($"({string.Join(",", parametersNames)})");
            }

            return builder.ToString();
        }

        public static string GetMemberNameForType(Type type)
        {
            var builder = new StringBuilder("T:");
            builder.Append(QualifiedNameFor(type));

            return builder.ToString();
        }

        public static string GetMemberNameForMember(MemberInfo memberInfo)
        {
            var builder = new StringBuilder(((memberInfo.MemberType & MemberTypes.Field) != 0) ? "F:" : "P:");
            builder.Append(QualifiedNameFor(memberInfo.DeclaringType));
            builder.Append($".{memberInfo.Name}");

            return builder.ToString();
        }

        private static string QualifiedNameFor(Type type, bool expandGenericArgs = false)
        {
            if (type.IsArray)
                return $"{QualifiedNameFor(type.GetElementType(), expandGenericArgs)}[]";

            var builder = new StringBuilder();

            if (!string.IsNullOrEmpty(type.Namespace))
                builder.Append($"{type.Namespace}.");

            if (type.IsNested)
                builder.Append($"{type.DeclaringType.Name}.");

            if (type.IsConstructedGenericType && expandGenericArgs)
            {
                var nameSansGenericArgs = type.Name.Split('`').First();
                builder.Append(nameSansGenericArgs);

                var genericArgsNames = type.GetGenericArguments().Select(t =>
                {
                    return t.IsGenericParameter
                        ? $"`{t.GenericParameterPosition}"
                        : QualifiedNameFor(t, true);
                });

                builder.Append($"{{{string.Join(",", genericArgsNames)}}}");
            }
            else
            {
                builder.Append(type.Name);
            }

            return builder.ToString();
        }
    }
}