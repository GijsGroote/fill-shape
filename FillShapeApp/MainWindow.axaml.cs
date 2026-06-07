using Avalonia.Controls;
using AppGrid = FillShapeApp.Grid;

namespace FillShapeApp;


public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        Grid grid = new(rows: 300, columns: 300);
        (int row, int col)[] corners = [
            (1,1),
            (1,28),
            (28,28),
            (28,1),
        ];
        grid.SetRectangleContour(corners);


        // // 1. Create and populate your grid
        // var grid = new AppGrid(30, 30);
        // grid.SetCircleContour((15, 15), 10);
        // grid.FillShapeRecursively((15, 15));
        //
        // assume that the screen is 1920 by 1080
        var AppWidth = 1200;
        var AppHeigth = 800;

        Console.WriteLine("grid.rows"+grid.Rows+" AppHeight: "+AppHeigth+" grid.coluns "+grid.Columns+" AppWith: "+AppWidth);

        Console.WriteLine("jajajajja"+Math.Min(AppHeigth/grid.Rows, AppWidth/grid.Columns));


        // 2. Create the control and assign the grid
        var gridControl = new GridControl
        {
            Grid = grid,
            CellSize = Math.Min(AppHeigth/grid.Rows, AppWidth/grid.Columns)
        };

        // 3. Put it in the window
        Content = gridControl;
    }
}
