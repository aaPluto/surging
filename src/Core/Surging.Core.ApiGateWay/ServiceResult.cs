﻿namespace Surging.Core.ApiGateWay
{
    /// <summary>
    /// 自定义结果对象
    /// </summary>
    /// <typeparam name="T">需要返回的类型</typeparam>

    public class ServiceResult<T> : ServiceResult
    {
        /// <summary>
        /// 数据集
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 生成自定义服务数据集
        /// </summary>
        /// <param name="successd">状态值（true:成功 false：失败）</param>
        /// <param name="message">返回到客户端的消息</param>
        /// <param name="entity">返回到客户端的数据集</param>
        /// <returns>返回信息结果集</returns>
        public static ServiceResult<T> Create(bool successd, string message, T data)
        {
            return new ServiceResult<T>()
            {
                IsSucceed = successd,
                Message = message,
                Data = data
            };
        }

        /// <summary>
        /// 生成自定义服务数据集
        /// </summary>
        /// <param name="successd">状态值（true:成功 false:失败）</param>
        /// <param name="entity">返回到客户端的数据集</param>
        /// <returns>返回信息结果集</returns>
        public static ServiceResult<T> Create(bool successd, T data)
        {
            return new ServiceResult<T>()
            {
                IsSucceed = successd,
                Data = data
            };
        }
    }

    public class ServiceResult
    {
        /// <summary>
        /// 生成错误信息
        /// </summary>
        /// <param name="message">返回客户端的消息</param>
        /// <returns>返回服务数据集</returns>
        public static ServiceResult Error(string message)
        {
            return new ServiceResult() { Message = message, IsSucceed = false };
        }

        /// <summary>
        /// 生成服务器数据集
        /// </summary>
        /// <param name="success">状态值（true:成功 false：失败）</param>
        /// <param name="successMessage">返回客户端的消息</param>
        /// <param name="errorMessage">错误信息</param>
        /// <returns>返回服务数据集</returns>
        public static ServiceResult Create(bool success, string successMessage = "", string errorMessage = "")
        {
            return new ServiceResult() { Message = success ? successMessage : errorMessage, IsSucceed = success };
        }

        /// <summary>
        /// 构造服务数据集
        /// </summary>
        public ServiceResult()
        {
            IsSucceed = false;
            Message = string.Empty;
        }

        /// <summary>
        /// 状态值
        /// </summary>

        public bool IsSucceed { get; set; }

        /// <summary>
        ///返回客户端的消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 状态码
        /// </summary>
        public int StatusCode { get; set; }
    }
}