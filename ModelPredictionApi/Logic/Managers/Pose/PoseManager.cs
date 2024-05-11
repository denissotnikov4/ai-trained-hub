using Compunet.YoloV8;
using Compunet.YoloV8.Data;
using Compunet.YoloV8.Metadata;
using FileLib.Services.Interfaces;
using Logic.Helpers;
using Logic.Managers.Pose.Interfaces;
using Logic.Records.Pose.Params;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using PoseResult = Logic.Records.Pose.Results.PoseResult;

namespace Logic.Managers.Pose;

/// <inheritdoc cref="IPoseManager"/>
public class PoseManager : IPoseManager
{
    private readonly IFileService _fileService;

    public PoseManager(IFileService fileService)
    {
        _fileService = fileService;
    }

    /// <inheritdoc cref="IPoseManager.PoseAsync"/>
    public async Task<PoseResult> PoseAsync(PoseParam poseParam)
    {
        var model = await _fileService.GetFileAsync(poseParam.ModelId);
        var file = await _fileService.GetFileAsync(poseParam.FileId);
        
        using var predictor = YoloV8Predictor.Create(model.FileContent);
        
        PredictionHelper.ThrowIfModelDoesNotSupportTask(predictor, YoloV8Task.Pose);
        
        var result = await predictor.PoseAsync(file.FileContent);
        
        using var image = Image.Load(file.FileContent);
        PlotKeyPointsOnImage(result, image);

        var predictedFileName = "imageWithPose.png";
        var plottedByteList = await PredictionHelper.ConvertImageToByteArrayAsync(image);
        var predictedFileId = await _fileService.SaveFileAsync(plottedByteList, predictedFileName);

        var poseResult = new PoseResult
        {
            PredictedFileId = predictedFileId.FileId
        };

        return poseResult;
    }
    
    /// <summary>
    /// Отрисовать кейпоинты на изображении
    /// </summary>
    /// <param name="poseResult">Результат работы модели</param>
    /// <param name="image">Изображение на которое будут нанесены кейпоинты</param>
    private static void PlotKeyPointsOnImage(Compunet.YoloV8.Data.PoseResult poseResult, Image image)
    {
        var resultBoxes = poseResult.Boxes;
        
        foreach (var resultBox in resultBoxes)
        {
            var keypointList = resultBox.Keypoints;
            
            for (var index = 0; index < keypointList.Length; index++)
            {
                var keypoint = keypointList[index];
                if (keypoint.Point.X == 0 || keypoint.Point.Y == 0)
                {
                    continue;
                }
                
                DrawKeyPoint(keypoint, image);

                DrawIndexesNearKeyPoints(keypoint, image, index);
            }
        }
    }

    /// <summary>
    /// Отрисовка кейпоинта на изображении
    /// </summary>
    /// <param name="keypoint"></param>
    /// <param name="image"></param>
    private static void DrawKeyPoint(Keypoint keypoint, Image image)
    {
        var graphics = new DrawingOptions();
        
        var ellipsePolygon = new EllipsePolygon(keypoint.Point.X, keypoint.Point.Y, 10, 10);
        
        image.Mutate(x => x.Fill(graphics, Color.Red, ellipsePolygon));
    }

    /// <summary>
    /// Отрисовка индексов рядом с кейпонтами
    /// </summary>
    /// <param name="keypoint"></param>
    /// <param name="image"></param>
    /// <param name="index"></param>
    private static void DrawIndexesNearKeyPoints(Keypoint keypoint, Image image, int index)
    {
        var fontName = "Arial";
        var fontSize = 18;
        
        var font = SystemFonts.CreateFont(fontName, fontSize, FontStyle.Bold); 
        
        image.Mutate(x => x.DrawText(
            text: (index + 1).ToString(),
            font: font,
            color: Color.Red,
            location: new PointF(keypoint.Point.X + 5, keypoint.Point.Y + 5)
        ));
    }
}