using Compunet.YoloV8;
using Compunet.YoloV8.Metadata;
using Compunet.YoloV8.Plotting;
using FileLib.Services.Interfaces;
using Logic.Helpers;
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
        
        PredictionHelper.ThrowIfModelDoesNotSupportTask(predictor, YoloV8Task.Detect);
        
        var result = await predictor.DetectAsync(file.FileContent);
        
        using var image = Image.Load(file.FileContent);
        using var plotted = await result.PlotImageAsync(image);

        var predictedFileName = "imageWithDetect.png";
        var plottedBytes = await PredictionHelper.ConvertImageToByteArrayAsync(plotted);
        var predictedFileId = await _fileService.SaveFileAsync(plottedBytes, predictedFileName);

        var detectResult = new DetectResult
        {
            PredictedFileId = predictedFileId.FileId
        };

        return detectResult;
    }
}