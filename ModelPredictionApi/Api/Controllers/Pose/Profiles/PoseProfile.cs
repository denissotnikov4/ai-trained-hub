using Api.Controllers.Pose.Dto.Requests;
using Api.Controllers.Pose.Dto.Responses;
using AutoMapper;
using Logic.Records.Pose.Params;
using Logic.Records.Pose.Results;

namespace Api.Controllers.Pose.Profiles;

/// <summary>
/// Маппинг для Pose в Api
/// </summary>
public class PoseProfile : Profile
{
    public PoseProfile()
    {
        CreateMap<PoseRequest, PoseParam>()
            .ForMember(x => x.FileId, y => y.MapFrom(z => z.FileId))
            .ForMember(x => x.ModelId, y => y.MapFrom(z => z.ModelId));
        
        CreateMap<PoseResult, PoseResponse>()
            .ForMember(x => x.PredictedFileId, y => y.MapFrom(z => z.PredictedFileId));
    }
}