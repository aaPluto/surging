using AutoMapper;
using Hl.BasicData.Common.SystemConf;
using Hl.BasicData.Domain;

namespace Hl.BasicData.IApplication.Dtos
{
    public class SystemConfProfiles : Profile
    {
        public SystemConfProfiles()
        {
            CreateMap<SystemConf, GetSystemConfOutput>();
        }
    }
}
