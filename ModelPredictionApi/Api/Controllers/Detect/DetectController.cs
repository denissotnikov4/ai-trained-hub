using System.Net;
using Api.Controllers.Detect.Dto.Requests;
using Api.Controllers.Detect.Dto.Responses;
using AutoMapper;
using Logic.Managers.Detect.Interfaces;
using Logic.Records.Detect.Params;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Detect;

/// <summary>
/// Контроллер для задач обнаружения
/// </summary>
[ApiController]
[Route("detect")]
public class DetectController : ControllerBase
{
    private readonly IDetectManager _detectManager;
    private readonly IMapper _mapper;

    public DetectController(IDetectManager detectManager, IMapper mapper)
    {
        _detectManager = detectManager;
        _mapper = mapper;
    }

    /// <summary>
    /// Экшен для задачи обнаружения объекта
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(DetectResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DetectAsync([FromBody] DetectRequest detectRequest)
    {
        var detectParam = _mapper.Map<DetectParam>(detectRequest);
        var detectResult = await _detectManager.DetectAsync(detectParam);
        var response = _mapper.Map<DetectResponse>(detectResult);
        return Ok(response);
    }
}