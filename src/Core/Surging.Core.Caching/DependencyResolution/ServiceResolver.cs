﻿using Surging.Core.Caching.Utilities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;

namespace Surging.Core.Caching.DependencyResolution
{
    /// <summary>
    /// IOC容器对象
    /// </summary>
    /// <remarks>
    /// 	<para>创建：范亮</para>
    /// 	<para>日期：2016/4/2</para>
    /// </remarks>
    public class ServiceResolver : IDependencyResolver
    {
        #region 字段

        private static readonly ServiceResolver _defaultInstance = new ServiceResolver();

        private readonly ConcurrentDictionary<Tuple<Type, string>, object> _initializers =
            new ConcurrentDictionary<Tuple<Type, string>, object>();

        #endregion 字段

        #region 公共方法

        /// <summary>
        /// 注册对象添加到IOC容器
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <remarks>
        /// 	<para>创建：范亮</para>
        /// 	<para>日期：2016/4/2</para>
        /// </remarks>
        public virtual void Register(string key, object value)
        {
            DebugCheck.NotNull(value);
            // DebugCheck.NotNull(key);

            _initializers.GetOrAdd(Tuple.Create(value.GetType(), key), value);
            var interFaces = value.GetType().GetTypeInfo().GetInterfaces();
            foreach (var interFace in interFaces)
            {
                _initializers.GetOrAdd(Tuple.Create(interFace, key), value);
            }
        }

        /// <summary>
        /// 返回当前IOC容器
        /// </summary>
        /// <remarks>
        /// 	<para>创建：范亮</para>
        /// 	<para>日期：2016/4/2</para>
        /// </remarks>
        public static ServiceResolver Current
        {
            get { return _defaultInstance; }
        }

        /// <summary>
        /// 通过KEY和TYPE获取实例对象
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="key">键</param>
        /// <returns>返回实例对象</returns>
        /// <remarks>
        /// 	<para>创建：范亮</para>
        /// 	<para>日期：2016/4/2</para>
        /// </remarks>
        public virtual object GetService(Type type, object key)
        {
            object result;
            _initializers.TryGetValue(Tuple.Create(type, key == null ? null : key.ToString()), out result);
            return result;
        }

        /// <summary>
        /// 通过KEY和TYPE获取实例对象集合
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="key">键</param>
        /// <returns>返回实例对象</returns>
        /// <remarks>
        /// 	<para>创建：范亮</para>
        /// 	<para>日期：2016/4/2</para>
        /// </remarks>
        public IEnumerable<object> GetServices(Type type, object key)
        {
            return this.GetServiceAsServices(type, key);
        }

        #endregion 公共方法
    }
}