using Surging.Core.CPlatform.Ioc;


namespace Hl.Identity.Domain.Authorization.Users
{
    public interface IPasswordHelper : ITransientDependency
    {
        string EncryptPassword(string userName, string plainPassword);
    }
}
