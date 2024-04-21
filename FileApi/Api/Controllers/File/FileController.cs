using System.Net;
using Api.Controllers.Dto.Response;
using Api.Controllers.File.Dto.Request;
using AutoMapper;
using Dal.Helpers;
using Dal.Repositories.FileRepository.Interfaces;
using HttpLogic.Models;
using Logic.Managers.File.Managers.Interfaces;
using Logic.Records.File.Params;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.File;

/// <summary>
/// Контроллер для работы с файлами
/// </summary>
[ApiController]
[Route("file")]
public class FileController : ControllerBase
{
    private readonly IFileManager _fileManager;
    private readonly IMapper _mapper;
    
    public FileController(IFileManager fileManager, IMapper mapper)
    {
        _fileManager = fileManager;
        _mapper = mapper;
    }

    /// <summary>
    /// Получить файл по id
    /// </summary>
    [HttpGet("download")]
    [ProducesResponseType(typeof(FileContentResult), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetFileById([FromQuery] GetFileRequest getFileRequest)
    {
        var file = await _fileManager.GetFileByIdAsync(getFileRequest.FileId);
        return File(file.FileBytes, FileHelper.GetMimeType(file.FileExtension), file.OriginalFileName);
    }
    
    /// <summary>
    /// Загрузить файл
    /// </summary>
    [HttpPost("upload")]
    [RequestSizeLimit(100_000_000)]
    [ProducesResponseType(typeof(CreateFileResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UploadFileAsync(IFormFile file)
    {
        var createFileResult = await _fileManager.UploadFileAsync(file);
        var response = new CreateFileResponse { FileId = createFileResult.FileId };
        return Ok(response);
    }
    
    /// <summary>
    /// Получить файл по id для межсервисного взаимодействия
    /// </summary>
    [HttpGet("download-private")]
    [ProducesResponseType(typeof(FileHttpResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetFileByIdPrivateAsync([FromQuery] GetFileRequest getFileRequest)
    {
        var file = await _fileManager.GetFileByIdAsync(getFileRequest.FileId);

        var fileHttpResponse = new FileHttpResponse
        {
            FileNameWithExtension = $"{file.FileExtension}.{file.OriginalFileName}",
            FileContent = file.FileBytes
        };
        
        return Ok(fileHttpResponse);
    }

    /// <summary>
    /// Изменить файл
    /// </summary>
    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateFileAsync([FromForm] UpdateFileRequest updateFileRequest,
                                                     IFormFile file)
    {
        var updateFileParam = _mapper.Map<UpdateFileParam>(updateFileRequest);
        await _fileManager.UpdateFileAsync(updateFileParam, file);
        return Ok();
    }

    /// <summary>
    /// Удалить файл
    /// </summary>
    [HttpDelete]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteFileAsync([FromBody] DeleteFileRequest deleteFileRequest)
    {
        await _fileManager.DeleteFileAsync(deleteFileRequest.FileId);
        return Ok();
    }
}