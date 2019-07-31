using AutoMapper;
using Hl.BasicData.Common.HlDictionary;
using Hl.BasicData.Domain;

namespace Hl.BasicData.IApplication.Dictionary.Dtos
{
    public class DictProfiles : Profile
    {
        public DictProfiles()
        {
            CreateMap<CreateDictInput, HlDictionary>();
            CreateMap<HlDictionary, HlDictionaryOutput>();
        }
    }
}
