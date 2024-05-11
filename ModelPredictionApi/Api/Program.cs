namespace Api;

/// <summary>
/// Точка входа в приложение
/// </summary>
public class Program
{
    /// <summary>
    /// Точка входа в приложение. Создает и запускает хост приложения, выполняет миграцию базы данных и запускает приложение
    /// </summary>
    /// <param name="args">Аргументы командной строки</param>
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    /// <summary>
    /// Создает конфигурацию хоста приложения с использованием стандартных настроек
    /// </summary>
    /// <param name="args">Аргументы командной строки</param>
    /// <returns>Конфигуратор хоста приложения</returns>
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
