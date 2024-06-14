using System.Net;
using Api.Controllers.Classify.Dto.Requests;
using Api.Controllers.Classify.Dto.Responses;
using AutoMapper;
using Logic.Managers.Classify.Interfaces;
using Logic.Records.Classify.Params;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Classify;

/// <summary>
/// Контроллер для задач классификации
/// </summary>
[ApiController]
[Route("classify")]
public class ClassifyController : ControllerBase
{
    private readonly IClassifyManager _classifyManager;
    private readonly IMapper _mapper;

    public ClassifyController(IClassifyManager classifyManager, IMapper mapper)
    {
        _classifyManager = classifyManager;
        _mapper = mapper;
    }

    /// <summary>
    /// Экшен для задачи классификации 
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ClassifyResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> ClassifyAsync([FromBody] ClassifyRequest classifyRequest)
    {
        var classifyParam = _mapper.Map<ClassifyParam>(classifyRequest);
        var classifyResult = await _classifyManager.ClassifyAsync(classifyParam);
        var response = _mapper.Map<ClassifyResponse>(classifyResult);
        return Ok(response);
    }
}