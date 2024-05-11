using Compunet.YoloV8;
using Compunet.YoloV8.Metadata;
using Core.Exceptions;
using SixLabors.ImageSharp;

namespace Logic.Helpers;

/// <summary>
/// Helper для предиктов
/// </summary>
public static class PredictionHelper
{
    /// <summary>
    /// Конвертирует изображение в массив байтов
    /// </summary>
    /// <param name="plotted">Изображение с предиктом</param>
    /// <returns>Массив байтов изображения</returns>
    public static async Task<byte[]> ConvertImageToByteArrayAsync(Image plotted)
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
    
    /// <summary>
    /// Кидаем ошибку, если модель не поддерживает тип задачи
    /// </summary>
    /// <param name="predictor">Модель</param>
    /// <param name="task">Тип задачи</param>
    /// <exception cref="ModelDoesNotSupportTaskException"></exception>
    public static void ThrowIfModelDoesNotSupportTask(YoloV8Predictor predictor, YoloV8Task task)
    {
        if (predictor.Metadata.Task != task)
        {
            throw new ModelDoesNotSupportTaskException();
        }
    }
}