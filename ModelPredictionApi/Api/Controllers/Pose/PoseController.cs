using System.Net;
using Api.Controllers.Pose.Dto.Requests;
using Api.Controllers.Pose.Dto.Responses;
using AutoMapper;
using Logic.Managers.Pose.Interfaces;
using Logic.Records.Pose.Params;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Pose;

/// <summary>
/// Контроллер для задач типа Pose
/// </summary>
[ApiController]
[Route("pose")]
public class PoseController : ControllerBase
{
    private readonly IPoseManager _poseManager;
    private readonly IMapper _mapper;

    public PoseController(IPoseManager poseManager, IMapper mapper)
    {
        _poseManager = poseManager;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Экшен для задач Pose
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(PoseResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> PoseAsync([FromBody] PoseRequest poseRequest)
    {
        var poseParam = _mapper.Map<PoseParam>(poseRequest);
        var poseResult = await _poseManager.PoseAsync(poseParam);
        var response = _mapper.Map<PoseResponse>(poseResult);
        return Ok(response);
    }
}