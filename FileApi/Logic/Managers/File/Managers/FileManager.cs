using AutoMapper;
using Dal.Enums;
using Dal.Helpers;
using Dal.Models;
using Dal.Repositories.FileRepository.Interfaces;
using Logic.Managers.File.Managers.Interfaces;
using Logic.Models;
using Logic.Records.File.Params;
using Logic.Records.File.Results;
using Logic.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using FileNotFoundException = Core.Exceptions.FileNotFoundException;

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
    
    public async Task<CreateFileResult> UploadFileAsync(IFormFile file)
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
            AccessModifier = AccessModifier.Private
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

        if (fileDal is null)
        {
            throw new FileNotFoundException($"File with id {fileId} not found");
        }

        var fileModel = _mapper.Map<FileModel>(fileDal);
        
        var fileBytes = await _fileStorageService.GetFileBytesAsync(fileModel);
        
        var fileByIdResult = _mapper.Map<GetFileResult>(fileDal);
        fileByIdResult.FileBytes = fileBytes;
        
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

        if (fileDal is null)
        {
            throw new FileNotFoundException($"File with id {fileId} not found");
        }

        await _fileRepository.DeleteFileAsync(fileId);
        
        var fileModel = _mapper.Map<FileModel>(fileDal);
        await _fileStorageService.DeleteFileAsync(fileModel);
    }
}