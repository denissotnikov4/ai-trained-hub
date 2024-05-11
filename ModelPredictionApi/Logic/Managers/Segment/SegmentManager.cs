using Compunet.YoloV8;
using Compunet.YoloV8.Metadata;
using Compunet.YoloV8.Plotting;
using FileLib.Services.Interfaces;
using Logic.Helpers;
using Logic.Managers.Segment.Interfaces;
using Logic.Records.Segment.Params;
using Logic.Records.Segment.Results;
using SixLabors.ImageSharp;

namespace Logic.Managers.Segment;

/// <inheritdoc cref="ISegmentManager"/>
public class SegmentManager : ISegmentManager
{
    private readonly IFileService _fileService;

    public SegmentManager(IFileService fileService)
    {
        _fileService = fileService;
    }

    /// <inheritdoc cref="ISegmentManager.SegmentAsync"/>
    public async Task<SegmentResult> SegmentAsync(SegmentParam segmentParam)
    {
        var model = await _fileService.GetFileAsync(segmentParam.ModelId);
        var file = await _fileService.GetFileAsync(segmentParam.FileId);

        using var predictor = YoloV8Predictor.Create(model.FileContent);
        
        PredictionHelper.ThrowIfModelDoesNotSupportTask(predictor, YoloV8Task.Segment);
        
        var result = await predictor.SegmentAsync(file.FileContent);
        
        using var image = Image.Load(file.FileContent);
        using var plotted = await result.PlotImageAsync(image);

        var predictedFileName = "imageWithSegmentation.png";
        var plottedBytes = await PredictionHelper.ConvertImageToByteArrayAsync(plotted);
        var predictedFileId = await _fileService.SaveFileAsync(plottedBytes, predictedFileName);

        var segmentResult = new SegmentResult
        {
            PredictedFileId = predictedFileId.FileId
        };

        return segmentResult;
    }
}