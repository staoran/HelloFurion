using System;
using System.IO;
using Furion.UnifyResult;
using Furion.Logging.Extensions;
using Microsoft.Extensions.DependencyInjection;

Serve.Run(x => x.AddFileLogging(options =>
{
    options.FileNameRule = fileName =>
    {
        var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
                              ?? Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")
                              ?? "Unknown";
        var isDevelopment =
            String.Equals(environmentName, "Development", StringComparison.OrdinalIgnoreCase);

        Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") +
                          $" - {(isDevelopment ? "Development" : environmentName)} - " +
                          fileName);
        return string.Format(fileName, DateTime.Now);
    };
    options.WithStackFrame = true;
    options.WithTraceId = true;
    options.HandleWriteError = writeError => writeError.UseRollbackFileName(
        $"{Path.GetFileNameWithoutExtension(writeError.CurrentFileName)}-oops{Path.GetExtension(writeError.CurrentFileName)}");
}));

[DynamicApiController]
public class HelloService
{

    public void Say()
    {
        "输出日志".LogInformation();
    }


    public void SayEmptySucceeded(int id)
    {
    }
}