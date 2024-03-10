using System.Net;
using Api.Controllers.Dto.Response;
using Api.Controllers.File.Dto.Request;
using Api.Controllers.File.Dto.Response;
using AutoMapper;
using Dal.Helpers;
using Dal.Repositories.FileRepository.Interfaces;
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
    private IFileManager _fileManager;
    private IMapper _mapper;
    
    public FileController(IFileRepository repository, IFileManager fileManager, IMapper mapper)
    {
        _fileManager = fileManager;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Загрузить файл
    /// </summary>
    [HttpPost("upload")]
    [ProducesResponseType(typeof(CreateFileResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UploadFileAsync([FromForm] CreateFileRequest createFileRequest, 
                                                     IFormFile file)
    {
        var createFileParam = _mapper.Map<CreateFileParam>(createFileRequest);
        var createFileResult = await _fileManager.UploadFileAsync(createFileParam, file);
        var response = new CreateFileResponse { FileId = createFileResult.FileId };
        return Ok(response);
    }

    /// <summary>
    /// Получить файл по id
    /// </summary>
    [HttpGet("download")]
    [ProducesResponseType(typeof(FileContentResult), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetFileById([FromQuery] GetFileRequest getFileRequest)
    {
        var file = await _fileManager.GetFileByIdAsync(getFileRequest.FileId);
        return File(file.FileStream, FileHelper.GetMimeType(file.FileExtension), file.OriginalFileName);
    }

    /// <summary>
    /// Изменить файл
    /// </summary>
    [HttpPatch]
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