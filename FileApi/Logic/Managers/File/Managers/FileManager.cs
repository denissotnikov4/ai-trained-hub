using AutoMapper;
using Dal.Helpers;
using Dal.Models;
using Dal.Repositories.FileRepository.Interfaces;
using Logic.Managers.File.Managers.Interfaces;
using Logic.Models;
using Logic.Records.File.Params;
using Logic.Records.File.Results;
using Logic.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Logic.Managers.File.Managers;

/// <inheritdoc cref="IFileManager"/>
internal class FileManager : IFileManager
{
    private IFileRepository _fileRepository;
    private IFileStorageService _fileStorageService; 
    private IMapper _mapper;

    public FileManager(IFileRepository fileRepository, IFileStorageService fileStorageService, IMapper mapper)
    {
        _fileRepository = fileRepository;
        _fileStorageService = fileStorageService;
        _mapper = mapper;
    }
    
    /// <inheritdoc cref="IFileManager.UploadFileAsync"/>
    public async Task<CreateFileResult> UploadFileAsync(CreateFileParam createFileParam, IFormFile file)
    {
        var fileName = FileHelper.GetFileNameFromFileNameWithExtension(file.FileName);
        var fileExtension = FileHelper.GetExtensionFile(file.FileName);
        
        var fileDal = new FileDal
        {
            OriginalFileName = fileName,
            HandledFileName = fileName.Replace(" ", ""),
            FileExtension = fileExtension,
            CreatedTime = DateTime.Now,
            FileSize = file.Length,
            AccessModifier = createFileParam.AccessModifier
        };

        var fileId = await _fileRepository.SaveFileAsync(fileDal);
        
        await _fileStorageService.SaveFileAsync(file, fileId);

        var createFileResult = new CreateFileResult { FileId = fileId };

        return createFileResult;
    }

    /// <inheritdoc cref="IFileManager.GetFileByIdAsync"/>
    public async Task<GetFileResult> GetFileByIdAsync(Guid fileId)
    {
        var fileDal = await _fileRepository.GetFileByIdAsync(fileId);

        var fileModel = _mapper.Map<FileModel>(fileDal);
        var fileStream = await _fileStorageService.GetFileDataAsync(fileModel);
        
        var fileByIdResult = _mapper.Map<GetFileResult>(fileDal);
        fileByIdResult.FileStream = fileStream;
        
        return fileByIdResult;
    }

    /// <inheritdoc cref="IFileManager.UpdateFileAsync"/>
    public async Task UpdateFileAsync(UpdateFileParam updateFileParam, IFormFile file)
    {
        var fileName = FileHelper.GetFileNameFromFileNameWithExtension(file.FileName);
        var fileExtension = FileHelper.GetExtensionFile(file.FileName);
        
        var fileDal = new FileDal
        {
            Id = updateFileParam.FileId,
            OriginalFileName = fileName,
            HandledFileName = fileName.Replace(" ", ""),
            FileExtension = fileExtension,
            FileSize = file.Length,
            AccessModifier = updateFileParam.AccessModifier
        };
        
        await _fileRepository.UpdateFileAsync(fileDal);

        await _fileStorageService.SaveFileAsync(file, updateFileParam.FileId);
    }

    /// <inheritdoc cref="IFileManager.DeleteFileAsync"/>
    public async Task DeleteFileAsync(Guid fileId)
    {
        var fileDal = await _fileRepository.GetFileByIdAsync(fileId);
        
        await _fileRepository.DeleteFileAsync(fileId);
        
        var fileModel = _mapper.Map<FileModel>(fileDal);
        await _fileStorageService.DeleteFileAsync(fileModel);
    }
}