using Api.Controllers.Detect.Dto.Requests;
using Api.Controllers.Detect.Dto.Responses;
using AutoMapper;
using Logic.Records.Detect.Params;
using Logic.Records.Detect.Results;

namespace Api.Controllers.Detect.Profiles;

/// <summary>
/// Маппинг для детекта в Api
/// </summary>
public class DetectProfile : Profile
{
    /// <inheritdoc />
    public DetectProfile()
    {
        CreateMap<DetectRequest, DetectParam>()
            .ForMember(x => x.FileId, y => y.MapFrom(z => z.FileId))
            .ForMember(x => x.ModelId, y => y.MapFrom(z => z.ModelId));

        CreateMap<DetectResult, DetectResponse>()
            .ForMember(x => x.PredictedFileId, y => y.MapFrom(z => z.PredictedFileId));
    }
}