using Surging.Core.CPlatform.Cache;
using Surging.Core.CPlatform.Exceptions;
using Surging.Core.CPlatform.Serialization;
using Surging.Core.CPlatform.Utilities;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CPlatformAppConfig = Surging.Core.CPlatform.AppConfig;

namespace Surging.Core.Caching
{
    public static class ICacheProviderProviderExtensions
    {
        private static ISerializer<string> _serializer = ServiceLocator.GetService<ISerializer<string>>();

        public static async Task<T> GetAsyn<T>(this ICacheProvider cacheProvider, string key, Func<Task<T>> factory, long? storeTime = null) where T : class
        {
            T returnValue;
            try
            {
                var resultJson = cacheProvider.Get<string>(key);
                if (string.IsNullOrEmpty(resultJson) || resultJson == "\"[]\"")
                {

                    returnValue = await factory();
                    cacheProvider.Update(key, _serializer.Serialize(returnValue), storeTime);
                }
                else
                {
                    returnValue = _serializer.Deserialize(resultJson, typeof(T)) as T;
                }
                return returnValue;
            }
            catch(Exception ex)
            {
                if (ex is DataAccessException)
                {
                    throw ex;
                }
                returnValue = await factory();
                return returnValue;
               
            }
        }

        public static void Update(this ICacheProvider cacheProvider, string key, string jsonValue, long? storeTime = null)
        {
            if (storeTime.HasValue)
            {
                cacheProvider.Remove(key);
                cacheProvider.Add(key, jsonValue, storeTime.Value);
            }
            else
            {
                cacheProvider.Remove(key);
                cacheProvider.Add(key, jsonValue);
            }
        }

        public static void Update<T>(this ICacheProvider cacheProvider, string key, T value, long? storeTime = null)
        {
            var jsonValue = _serializer.Serialize(value);
            cacheProvider.Update(key, jsonValue, storeTime);
        }

        public static void RemoveWithMatch(this ICacheProvider cacheProvider, string key)
        {
            var matchKey = "_*_" + Regex.Replace(key, @"{\d*}", "*");
            cacheProvider.RemoveWithPrefix(matchKey);
        }

        public static async Task RemoveWithMatchAsync(this ICacheProvider cacheProvider, string key)
        {
            await Task.Run(() => {
                var matchKey = "_*_" + Regex.Replace(key, @"{\d*}", "*");
                cacheProvider.RemoveWithPrefix(matchKey);
            });
        }

        public static async Task RemoveWithMatchAsync(this ICacheProvider cacheProvider, string key, Func<Task> action)
        {
            await action();
            var matchKey = CPlatformAppConfig.CacheSectionOptions.CacheSectionName + Regex.Replace(key, @"{\d*}", "*");
            cacheProvider.RemoveWithPrefix(matchKey);
        }

        public static void RemoveWithMatch(this ICacheProvider cacheProvider, params string[] keys)
        {
            foreach (var key in keys)
            {
                var matchKey = CPlatformAppConfig.CacheSectionOptions.CacheSectionName + Regex.Replace(key, @"{\d*}", "*");
                cacheProvider.RemoveWithPrefix(matchKey);
            }

        }
        public static async Task RemoveWithMatchAsync(this ICacheProvider cacheProvider, params string[] keys)
        {
            foreach (var key in keys)
            {
                var matchKey = CPlatformAppConfig.CacheSectionOptions.CacheSectionName + Regex.Replace(key, @"{\d*}", "*");
                await Task.Run(() =>
                {
                    cacheProvider.RemoveWithPrefix(matchKey);
                });
            }

        }
        public static async Task RemoveWithMatchAsync(this ICacheProvider cacheProvider, string[] keys, Func<Task> action)
        {
            await action();
            await cacheProvider.RemoveWithMatchAsync(keys);

        }

        public static void Remove(this ICacheProvider cacheProvider, params string[] keys)
        {
            foreach(var key in keys)
            {
                cacheProvider.Remove(key);
            }
        }
    }
}
