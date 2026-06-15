using Avalonia;
using FillShapeApp;
// using HorizontalVSVerticalBenchmarkTest;

public partial class Program
{
    public static void Main(string[] args)
    {
        // start horizontal vs vertical benchmark test using:
        // dotnet run -- --benchmark
        if (args.Length > 0 && args[0] == "--benchmark")
        {
            HorizontalVSVerticalBenchmarkTest.Run();
            return;
        }
        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }

    public static AppBuilder BuildAvaloniaApp() =>
        AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace();
}
