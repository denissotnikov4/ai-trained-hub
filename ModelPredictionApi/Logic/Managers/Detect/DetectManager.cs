using Compunet.YoloV8;
using Compunet.YoloV8.Plotting;
using FileLib.Services.Interfaces;
using HttpLogic.Models;
using Logic.Managers.Detect.Interfaces;
using Logic.Records.Detect.Params;
using Logic.Records.Detect.Results;
using SixLabors.ImageSharp;

namespace Logic.Managers.Detect;

/// <inheritdoc cref="IDetectManager"/>
public class DetectManager : IDetectManager
{
    private readonly IFileService _fileService;

    public DetectManager(IFileService fileService)
    {
        _fileService = fileService;
    }

    /// <inheritdoc cref="IDetectManager.DetectAsync"/>
    public async Task<DetectResult> DetectAsync(DetectParam detectParam)
    {
        var model = await _fileService.GetFileAsync(detectParam.ModelId);
        var file = await _fileService.GetFileAsync(detectParam.FileId);

        using var predictor = YoloV8Predictor.Create(model.FileContent);

        var result = await predictor.DetectAsync(file.FileContent);
        using var image = Image.Load(file.FileContent);
        using var plotted = await result.PlotImageAsync(image);

        var plottedBytes = await SaveImageToByteArrayAsync(plotted);

        var predictedFileId = await _fileService.SaveFileAsync(plottedBytes, "plotted.png");

        var detectResult = new DetectResult
        {
            PredictedFileId = predictedFileId.FileId
        };

        return detectResult;
    }
    
    /// <summary>
    /// Сохраняет изображение в массив байтов
    /// </summary>
    private async Task<byte[]> SaveImageToByteArrayAsync(Image plotted)
    {
        var tempFilePath = Path.GetTempFileName();

        await using (var fileStream = File.Create(tempFilePath))
        {
            await plotted.SaveAsync(fileStream, new SixLabors.ImageSharp.Formats.Png.PngEncoder());
        }

        var plottedBytes = await File.ReadAllBytesAsync(tempFilePath);
        
        File.Delete(tempFilePath); 

        return plottedBytes;
    }
}