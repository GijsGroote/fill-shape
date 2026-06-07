using Avalonia;
using FillShapeApp;

class Program
{
    public static void Main(string[] args) =>
        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);

    public static AppBuilder BuildAvaloniaApp() =>
        AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace();
}


// using System;
// using System.Collections.Generic;
// using System.IO;
// using System.Linq;
// using Avalonia;

// using FillShapeApp;

// class Program
// {
//     public static void Main(string[] args) =>
//         BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);

//         Console.WriteLine("Well we are heare");


//         // arrange
//         Grid grid = new(rows: 30, columns: 30);
//         (int row, int col)[] corners = [
//             (1,1),
//             (1,28),
//             (28,28),
//             (28,1),
//         ];
//         grid.SetRectangleContour(corners);

//         GridControl gridControl = new GridControl
//         {
//             Grid = grid,
//             CellSize = 20
//         };

//         gridControl.InvalidateVisual();
//         // grid.PrintGrid();
//         // act
//         // grid.FillShapeRecursively((4, 4));

//         // // grid.SetCircleContour((10, 15), 9);
//         // // grid.SetStarContour(10, 10, outerRadius: 8, innerRadius: 3, points: 6);
//         // grid.SetLineShapeContour([
//         //     (1,1),  (1,28), (12,28), (11, 20), (3, 21), (5, 5),
//         //     (13,5),(14, 25), (25, 25), (27, 1), (1,1)]);

//         // grid.PrintGrid();

//     public static AppBuilder BuildAvaloniaApp() =>
//         AppBuilder.Configure<App>()
//             .UsePlatformDetect()
//             .LogToTrace();
// }
