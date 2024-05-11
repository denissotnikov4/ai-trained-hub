using System.Net;
using Api.Controllers.Segment.Dto.Requests;
using Api.Controllers.Segment.Dto.Responses;
using AutoMapper;
using Logic.Managers.Segment.Interfaces;
using Logic.Records.Segment.Params;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Segment;

/// <summary>
/// Контроллер для задач сегментирования
/// </summary>
[ApiController]
[Route("segment")]
public class SegmentController : ControllerBase
{
    private readonly ISegmentManager _segmentManager;
    private readonly IMapper _mapper;
    
    public SegmentController(ISegmentManager segmentManager, IMapper mapper)
    {
        _segmentManager = segmentManager;
        _mapper = mapper;
    }

    /// <summary>
    /// Экшен для задачи сегментирования
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(SegmentResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> SegmentAsync([FromBody] SegmentRequest segmentRequest)
    {
        var segmentParam = _mapper.Map<SegmentParam>(segmentRequest);
        var segmentResult = await _segmentManager.SegmentAsync(segmentParam);
        var response = _mapper.Map<SegmentResponse>(segmentResult);
        return Ok(response);
    }
}