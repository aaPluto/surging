using System.Collections.Generic;
using System.Threading.Tasks;
using Hl.BasicData.Common.SystemConf;
using Hl.Core.ServiceApi;
using Hl.Core.Utils;
using Hl.Identity.Domain.Authorization.UserGroups;
using Surging.Core.CPlatform.Exceptions;
using Surging.Core.Dapper.Manager;
using Surging.Core.Dapper.Repositories;
using Surging.Core.ProxyGenerator;

namespace Hl.Identity.Domain.Authorization.Users
{
    public class UserManager : ManagerBase, IUserManager
    {
        private readonly IDapperRepository<UserInfo,long> _userRepository;
        private readonly IDapperRepository<UserRole, long> _userRoleRepository;
        private readonly IDapperRepository<UserGroupRelation, long> _userGroupRelationRepository;

        private readonly IServiceProxyProvider _serviceProxyProvider;
        private readonly IPasswordHelper _passwordHelper;

        public UserManager(IDapperRepository<UserInfo, long> userRepository,
            IDapperRepository<UserRole, long> userRoleRepository,
            IDapperRepository<UserGroupRelation, long> userGroupRelationRepository,
            IServiceProxyProvider serviceProxyProvider,
            IPasswordHelper passwordHelper)
        {
            _userRepository = userRepository;
            _userGroupRelationRepository = userGroupRelationRepository;
            _userRoleRepository = userRoleRepository;
            _serviceProxyProvider = serviceProxyProvider;
            _passwordHelper = passwordHelper;
        }

        public async Task CreateUserInfo(UserInfo userInfo)
        {
            var rpcParams = new Dictionary<string, object>() { { "confName", IdentityConstants.SysConfPwdModeName } };
            var pwdConfig = await _serviceProxyProvider.Invoke<GetSystemConfOutput>(rpcParams, ApiConsts.BasicData.GetSysConfApi);
            if (pwdConfig == null)
            {
                throw new BusinessException("获取用户加密模式失败,请先完成系统初始化");
            }
            var generatePwdMode = ConvertHelper.ParseEnum<GeneratePwdMode>(pwdConfig.ConfigValue);
            var plainPwd = string.Empty;
            if (generatePwdMode == GeneratePwdMode.Fixed)
            {
                rpcParams = new Dictionary<string, object>() { { "confName", IdentityConstants.SysConfFieldModeName } };
                var fixedPwdConf = await _serviceProxyProvider.Invoke<GetSystemConfOutput>(rpcParams, ApiConsts.BasicData.GetSysConfApi);
                if (pwdConfig == null)
                {
                    throw new BusinessException("未配置员工用户默认密码");
                }
                plainPwd = fixedPwdConf.ConfigValue;
            }
            else
            {
                plainPwd = PasswordGenerator.GetRandomPwd(IdentityConstants.RandomLen);
                // :todo email send pwd
            }
            userInfo.Password = _passwordHelper.EncryptPassword(userInfo.UserName, plainPwd);
            await _userRepository.InsertAsync(userInfo);
        }

        public async Task DeleteByUserId(long userId)
        {
            await UnitOfWorkAsync(async (conn, trans) => {
                await _userRepository.DeleteAsync(p => p.Id == userId, conn, trans);
                await _userRoleRepository.DeleteAsync(p => p.UserId == userId, conn, trans);
                await _userGroupRelationRepository.DeleteAsync(p => p.UserId == userId, conn, trans);
            }, Connection);
        }

        public async Task<UserInfo> GetUserInfoByUserId(long userId)
        {
            return await _userRepository.GetAsync(userId);
        }
    }
}
