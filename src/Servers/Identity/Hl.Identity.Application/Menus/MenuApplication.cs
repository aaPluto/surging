using System.Collections.Generic;
using System.Threading.Tasks;
using Hl.Core.Commons.Dtos;
using Hl.Core.ServiceApi;
using Hl.Core.Validates;
using Hl.Identity.Domain.Authorization.Menus;
using Hl.Identity.Domain.Authorization.Permissions;
using Hl.Identity.IApplication.Menus;
using Hl.Identity.IApplication.Menus.Dtos;
using Surging.Core.AutoMapper;
using Surging.Core.CPlatform.Exceptions;
using Surging.Core.CPlatform.Ioc;
using Surging.Core.Dapper.Repositories;
using Surging.Core.ProxyGenerator;

namespace Hl.Identity.Application.Menus
{
    [ModuleName(ApiConsts.Identity.ServiceKey, Version = "v1")]
    public class MenuApplication : ProxyServiceBase, IMenuApplication
    {
        private readonly IMenuManager _menuManager;
        private readonly IDapperRepository<Menu, long> _menuRepository;
        private readonly IDapperRepository<Permission, long> _permissionRepository;
        private readonly IDapperRepository<Function, long> _functionRepository;
        private readonly IFunctionManager _functionManager;
        public MenuApplication(IMenuManager menuManager,
            IDapperRepository<Menu, long> menuRepository,
            IDapperRepository<Permission, long> permissionRepository,
            IDapperRepository<Function, long> functionRepository,
            IFunctionManager functionManager)
        {
            _menuManager = menuManager;
            _menuRepository = menuRepository;
            _permissionRepository = permissionRepository;
            _functionRepository = functionRepository;
            _functionManager = functionManager;
        }

        public async Task<CreateFunctionOutput> CreateFunction(CreateFunctionInput input)
        {
            input.CheckDataAnnotations().CheckValidResult();
            var funcCode = input.WebApi.Replace("/", "."); //(input.WebApi.Replace("/", ".") + "." + input.Method).ToLower();
            var existFunc = await _functionRepository.SingleOrDefaultAsync(p => p.Code == funcCode);
            if (existFunc != null)
            {
                throw new BusinessException($"系统中已经存在{input.WebApi}-{input.Method}的接口信息");
            }
            if (input.ParentId != 0)
            {
                var parentFunc = await _functionRepository.SingleOrDefaultAsync(p => p.Id == input.ParentId);
                if (parentFunc == null)
                {
                    throw new BusinessException($"系统中已经不存在id为{input.ParentId}的父功能信息");
                }
            }
            var function = input.MapTo<Function>();
            var funcId = await _functionRepository.InsertOrUpdateAndGetIdAsync(function);
            return new CreateFunctionOutput() {
                FuncId = funcId,
                Tips = $"新增{input.WebApi}-{input.Method}的接口信息成功"
            };
        }

        public async Task<string> CreateMenu(CreateMenuInput input)
        {
            input.CheckDataAnnotations().CheckValidResult();
            var exsitMenu = await _menuRepository.SingleOrDefaultAsync(p => p.Code == input.Code);
            if (exsitMenu != null)
            {
                throw new BusinessException($"系统中已经存在Code为{input.Code}的菜单信息");
            }
            var exsitPermission = await _permissionRepository.SingleOrDefaultAsync(p => p.Code == input.Code);
            if (exsitPermission != null)
            {
                throw new BusinessException($"系统中已经存在Code为{input.Code}的权限信息");
            }
            var menu = input.MapTo<Menu>();
            var permission = new Permission()
            {
                Code = input.Code,
                Name = input.Name,
                Memo = input.Memo,
                Mold = PermissionMold.Menu
            };
            await _menuManager.CreateMenu(menu,permission);
            return "新增菜单成功";
        }

        public async Task<string> DeleteMenu(DeleteByIdInput input)
        {
            await _menuManager.DeleteMenu(input.Id);
            return "删除菜单成功";
        }

        public async Task<string> UpdateFunction(UpdateFunctionInput input)
        {
            input.CheckDataAnnotations().CheckValidResult();
            var function = await _functionRepository.SingleOrDefaultAsync(p => p.Id == input.Id);
            if (function == null)
            {
                throw new BusinessException($"不存在Id为{input.Id}的功能信息");
            }
            function = input.MapTo(function);
            await _functionRepository.UpdateAsync(function);
            return "更新功能操作成功";
        }

        public async Task<string> UpdateMenu(UpdateMenuInput input)
        {
            input.CheckDataAnnotations().CheckValidResult();
            var menu = await _menuRepository.SingleOrDefaultAsync(p => p.Id == input.Id);
            if (menu == null)
            {
                throw new BusinessException($"不存在Id为{input.Id}的菜单信息");
            }
            menu = input.MapTo(menu);
            var permission = await _permissionRepository.GetAsync(menu.PermissionId);
            permission.Memo = input.Memo;
            await _menuManager.UpdateMenu(menu, permission);
            return "更新菜单成功";
        }

        public async Task<string> CreateOperation(CreateOperationInput input)
        {
            var operation = await _permissionRepository.FirstOrDefaultAsync(p => p.Code == input.Code);
            if (operation != null)
            {
                throw new BusinessException($"系统中已经存在Code为{input.Code}的操作");
            }
            foreach (var funcId in input.FunctionIds)
            {
                var funcInfo = await _functionRepository.SingleOrDefaultAsync(p => p.Id == funcId);
                if (funcInfo == null)
                {
                    throw new BusinessException($"系统中不存在{funcId}的功能");
                }
            }
            operation = new Permission() {
                Code = input.Code,
                Name = input.Name,
                Memo = input.Memo,
                Mold = PermissionMold.Operate,
            };
            await _functionManager.CreateOperation(operation, input.FunctionIds);
            return $"新增{input.Name}操作成功";
        }

        public async Task<string> UpdateOperation(UpdateOperationInput input)
        {
            var operation = await _permissionRepository.FirstOrDefaultAsync(p => p.Id == input.Id);
            if (operation == null)
            {
                throw new BusinessException($"系统中不存在Id为{input.Id}的操作");
            }
            operation.Name = input.Name;
            operation.Memo = input.Memo;
            await _functionManager.UpdateOperation(operation, input.FunctionIds);
            return $"修改{input.Name}操作成功";
        }

        public async Task<ICollection<QueryFunctionOutput>> QueryFunctions(string keyworld)
        {
            var functions = await _functionRepository.GetAllAsync(p => p.WebApi.Contains(keyworld) || p.Code.Contains(keyworld));
            return functions.MapTo<ICollection<QueryFunctionOutput>>();
        }

        public async Task<QueryFunctionOutput> QueryFunction(string keyworld)
        {
            var function = await _functionRepository.FirstOrDefaultAsync(p => p.WebApi.Equals(keyworld)  || p.Code.Equals(keyworld));
            if (function == null)
            {
                throw new UserFriendlyException($"为查询到{keyworld}的功能记录");
            }
            return function.MapTo<QueryFunctionOutput>();
        }

        public async Task<string> DeleteFunction(DeleteByIdInput input)
        {
            await _functionRepository.DeleteAsync(p => p.Id == input.Id);
            return "删除功能操作成功";
        }
    }
}
