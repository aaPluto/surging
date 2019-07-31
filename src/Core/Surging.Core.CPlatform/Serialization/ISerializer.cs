﻿using System;

namespace Surging.Core.CPlatform.Serialization
{
    /// <summary>
    /// 一个抽象的序列化器。
    /// </summary>
    /// <typeparam name="T">序列化内容类型。</typeparam>
    public interface ISerializer<T>
    {
        /// <summary>
        /// 序列化。
        /// </summary>
        /// <param name="instance">需要序列化的对象。</param>
        /// <param name="camelCase">是否是camel格式。</param>
        /// <param name="indented">是否美化缩进。</param>
        /// <returns>序列化之后的结果。</returns>
        T Serialize(object instance, bool camelCase = true, bool indented = false);

        /// <summary>
        /// 反序列化。
        /// </summary>
        /// <param name="content">序列化的内容。</param>
        /// <param name="type">对象类型。</param>
        /// <returns>一个对象实例。</returns>
        object Deserialize(T content, Type type, bool camelCase = true, bool indented = false);
    }
}