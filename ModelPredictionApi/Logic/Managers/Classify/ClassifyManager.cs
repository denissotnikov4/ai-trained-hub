using Compunet.YoloV8;
using Compunet.YoloV8.Metadata;
using Compunet.YoloV8.Plotting;
using FileLib.Services.Interfaces;
using Logic.Helpers;
using Logic.Managers.Classify.Interfaces;
using Logic.Records.Classify.Params;
using Logic.Records.Classify.Results;
using SixLabors.ImageSharp;

namespace Logic.Managers.Classify;

/// <inheritdoc cref="IClassifyManager"/>
public class ClassifyManager : IClassifyManager
{
    private readonly IFileService _fileService;

    public ClassifyManager(IFileService fileService)
    {
        _fileService = fileService;
    }

    /// <inheritdoc cref="IClassifyManager.ClassifyAsync"/>
    public async Task<ClassifyResult> ClassifyAsync(ClassifyParam classifyParam)
    {
        var model = await _fileService.GetFileAsync(classifyParam.ModelId);
        var file = await _fileService.GetFileAsync(classifyParam.FileId);
        
        using var predictor = YoloV8Predictor.Create(model.FileContent);
        
        PredictionHelper.ThrowIfModelDoesNotSupportTask(predictor, YoloV8Task.Classify);
        
        var result = await predictor.ClassifyAsync(file.FileContent);
        
        using var image = Image.Load(file.FileContent);
        using var plotted = await result.PlotImageAsync(image);

        var predictedFileName = "imageWithClassify.png";
        var plottedBytes = await PredictionHelper.ConvertImageToByteArrayAsync(plotted);
        var predictedFileId = await _fileService.SaveFileAsync(plottedBytes, predictedFileName);

        var classifyResult = new ClassifyResult
        {
            PredictedFileId = predictedFileId.FileId
        };

        return classifyResult;
    }
}