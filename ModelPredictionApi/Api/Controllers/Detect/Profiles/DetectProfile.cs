using Api.Controllers.Detect.Dto.Requests;
using AutoMapper;
using Logic.Records.Detect.Params;

namespace Api.Controllers.Detect.Profiles;

public class DetectProfile : Profile
{
    public DetectProfile()
    {
        CreateMap<DetectRequest, DetectParam>()
            .ForMember(x => x.FileId, y => y.MapFrom(z => z.FileId))
            .ForMember(x => x.ModelId, y => y.MapFrom(z => z.ModelId));
    }
}