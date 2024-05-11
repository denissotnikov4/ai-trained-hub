using Api.Controllers.Segment.Dto.Requests;
using Api.Controllers.Segment.Dto.Responses;
using AutoMapper;
using Logic.Records.Segment.Params;
using Logic.Records.Segment.Results;

namespace Api.Controllers.Segment.Profiles;

/// <summary>
/// Маппинг для сегментирования в Api
/// </summary>
public class SegmentProfile : Profile
{
    /// <inheritdoc />
    public SegmentProfile()
    {
        CreateMap<SegmentRequest, SegmentParam>()
            .ForMember(x => x.FileId, y => y.MapFrom(z => z.FileId))
            .ForMember(x => x.ModelId, y => y.MapFrom(z => z.ModelId));
        
        CreateMap<SegmentResult, SegmentResponse>()
            .ForMember(x => x.PredictedFileId, y => y.MapFrom(z => z.PredictedFileId));
    }
}