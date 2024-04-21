﻿using System.Net;
using Api.Controllers.Detect.Dto;
using Api.Controllers.Detect.Dto.Requests;
using Api.Controllers.Detect.Dto.Responses;
using AutoMapper;
using Logic.Managers.Detect.Interfaces;
using Logic.Records.Detect.Params;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Detect;

/// <summary>
/// Контроллер для задач обнаружения
/// </summary>
[ApiController]
[Route("detect")]
public class DetectController : ControllerBase
{
    private IDetectManager _detectManager;
    private IMapper _mapper;

    public DetectController(IDetectManager detectManager, IMapper mapper)
    {
        _detectManager = detectManager;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(typeof(DetectResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DetectAsync([FromBody] DetectRequest detectRequest)
    {
        var detectParam = _mapper.Map<DetectParam>(detectRequest);
        var detectResult = await _detectManager.DetectAsync(detectParam);
        var response = new DetectResponse { PredictedFileId = detectResult.PredictedFileId };
        return Ok(response);
    }
}