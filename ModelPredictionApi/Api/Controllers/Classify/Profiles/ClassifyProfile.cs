using Api.Controllers.Classify.Dto.Requests;
using Api.Controllers.Classify.Dto.Responses;
using AutoMapper;
using Logic.Records.Classify.Params;
using Logic.Records.Classify.Results;

namespace Api.Controllers.Classify.Profiles;

/// <summary>
/// Маппинг для классификация в Api
/// </summary>
public class ClassifyProfile : Profile
{
    public ClassifyProfile()
    {
        CreateMap<ClassifyRequest, ClassifyParam>()
            .ForMember(x => x.ModelId, y => y.MapFrom(z => z.ModelId))
            .ForMember(x => x.FileId, y => y.MapFrom(z => z.FileId));

        CreateMap<ClassifyResult, ClassifyResponse>()
            .ForMember(x => x.PredictedFileId, y => y.MapFrom(z => z.PredictedFileId));
    }
}