using FillShapeApp;

using AppGrid = FillShapeApp.Grid;  // alias to avoid conflict

public static class HorizontalVSVerticalBenchmarkTest
{
    public static void Run()
    {
        // warmup, to take care of compilation overhead.
        var warmup = new AppGrid(10, 10);
        warmup.SetRectangleContour([ (0,0), (0,9), (9,9), (9,0) ]);
        warmup.InitialStartCell = (5, 5);
        warmup.FillShapeRecursively(direction: AppGrid.ExploreDirection.Horizontal);
        warmup = new AppGrid(10, 10);
        warmup.SetRectangleContour([ (0,0), (0,9), (9,9), (9,0) ]);
        warmup.InitialStartCell = (5, 5);
        warmup.FillShapeRecursively(direction: AppGrid.ExploreDirection.Vertical);

        // RecursiveAlgorithmBenchmark();
        // IterativelyAlgorithmBenchmark();
        IterativelyAlgorithmBenchmarkLarge();

    }

    private static void IterativelyAlgorithmBenchmark()
    {
        AppGrid FillHorizontallyGrid = new Grid(10000, 10000);
        FillHorizontallyGrid.SetRectangleContour([ (5,5), (5,9500), (9500,9500), (9500,5) ]);
        FillHorizontallyGrid.InitialStartCell = (25, 25);

        AppGrid FillVerticallyGrid = new Grid(10000, 10000);
        FillVerticallyGrid.SetRectangleContour([ (5,5), (5,9500), (9500,9500), (9500,5) ]);
        FillVerticallyGrid.InitialStartCell = (25, 25);

        var sw = System.Diagnostics.Stopwatch.StartNew();
        FillHorizontallyGrid.FillShapeIteratively(direction: AppGrid.ExploreDirection.Horizontal);
        sw.Stop();
        Console.WriteLine("Filled shape iteratively horizontally in: " + sw.Elapsed.TotalSeconds + " sec");

        sw.Restart();
        FillVerticallyGrid.FillShapeIteratively(direction: AppGrid.ExploreDirection.Vertical);
        sw.Stop();
        Console.WriteLine("Filled grid iteratively vertically in: " + sw.Elapsed.TotalSeconds + " sec");
    }

    private static void IterativelyAlgorithmBenchmarkLarge()
    {
        AppGrid FillHorizontallyGrid = new Grid(15000, 15000);
        FillHorizontallyGrid.SetRectangleContour([ (5,5), (5,14500), (14500,14500), (14500,5) ]);
        FillHorizontallyGrid.InitialStartCell = (25, 25);

        AppGrid FillVerticallyGrid = new Grid(15000, 15000);
        FillVerticallyGrid.SetRectangleContour([ (5,5), (5,14500), (14500,14500), (14500,5) ]);
        FillVerticallyGrid.InitialStartCell = (25, 25);

        var sw = System.Diagnostics.Stopwatch.StartNew();
        FillHorizontallyGrid.FillShapeIteratively(direction: AppGrid.ExploreDirection.Horizontal);
        sw.Stop();
        Console.WriteLine("Filled shape iteratively horizontally in: " + sw.Elapsed.TotalSeconds + " sec");

        sw.Restart();
        FillVerticallyGrid.FillShapeIteratively(direction: AppGrid.ExploreDirection.Vertical);
        sw.Stop();
        Console.WriteLine("Filled grid iteratively vertically in: " + sw.Elapsed.TotalSeconds + " sec");
    }


    private static void RecursiveAlgorithmBenchmark()
    {
        // CONCLUSION: STACK OVERFLOW IS ENCOUNTERED ON GRIDS LARGER THAN ~200 BY 200

        AppGrid FillHorizontallyGrid = new Grid(200, 200);
        FillHorizontallyGrid.SetRectangleContour([ (5,5), (5,195), (195,195), (195,5) ]);
        FillHorizontallyGrid.InitialStartCell = (25, 25);

        AppGrid FillVerticallyGrid = new Grid(200, 200);
        FillVerticallyGrid.SetRectangleContour([ (5,5), (5,195), (195,195), (195,5) ]);
        FillVerticallyGrid.InitialStartCell = (25, 25);

        var sw = System.Diagnostics.Stopwatch.StartNew();
        FillHorizontallyGrid.FillShapeRecursively(direction: AppGrid.ExploreDirection.Horizontal);
        sw.Stop();
        Console.WriteLine("Filled grid recursively horizontally in: " + sw.Elapsed.TotalSeconds + " sec");

        sw.Restart();
        FillVerticallyGrid.FillShapeRecursively(direction: AppGrid.ExploreDirection.Vertical);
        sw.Stop();
        Console.WriteLine("Filled grid recursively vertically in: " + sw.Elapsed.TotalSeconds + " sec");
    }
}
