﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Surging.Core.CPlatform.Module
{
    public class ModuleProvider : IModuleProvider
    {
        private readonly List<AbstractModule> _modules;
        private readonly CPlatformContainer _serviceProvoider;
        private readonly ILogger<ModuleProvider> _logger;

        public ModuleProvider(List<AbstractModule> modules,
            ILogger<ModuleProvider> logger,
            CPlatformContainer serviceProvoider)
        {
            _modules = modules;
            _serviceProvoider = serviceProvoider;
            _logger = logger;
        }

        public void Initialize()
        {
            _modules.ForEach(p =>
            {
                try
                {
                    if (p.Enable)
                        p.Initialize(_serviceProvoider);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
            WriteLog();
        }

        public void WriteLog()
        {
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _modules.ForEach(p =>
                {
                    if (p.Enable)
                        _logger.LogDebug($"已初始化加载模块，类型：{p.TypeName}模块名：{p.ModuleName}。");
                });
            }
        }
    }
}