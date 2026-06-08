using Avalonia.Controls;
using Avalonia.Interactivity;
using AppGrid = FillShapeApp.Grid;

namespace FillShapeApp;


public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        Grid grid = new(rows: 30, columns: 35);
        grid.SetRectangleContour([ (1,1), (1,28), (28,28), (28,1) ]);


        // // 1. Create and populate your grid
        // var grid = new AppGrid(30, 30);
        // grid.SetCircleContour((15, 15), 10);
        grid.InitialStartPoint = (15, 15);
        grid.FillShapeRecursively();

        //
        // assume that the screen is 1920 by 1080
        //

        // var GridWidth = this.Width;
        // var GridHeight = this.Heigth - ButtonPanel.Height;
        var buttonPanelTotalHeight = ButtonPanel.Height + 
                                     ButtonPanel.Margin.Top +
                                     ButtonPanel.Margin.Bottom;

        var CellSize = (int)Math.Floor( Math.Min(
                                            (this.Height - buttonPanelTotalHeight) / grid.Rows,
                                            this.Width / grid.Columns));

        if (CellSize == 0)
        {
            throw new NotImplementedException("CellSize is smaller than a single pixel.");
            
        }


        // 2. Create the control and assign the grid
        var gridControl = new GridControl
        {
            Grid = grid,
            CellSize = CellSize,
        };

        // 3. Put it in the window
        MainGrid.Grid = grid;
        MainGrid.CellSize = CellSize;
        MainGrid.InvalidateVisual();
    }

    private void OnButtonMinManyStepsClick(object? sender, RoutedEventArgs e)
    {
        MainGrid.TakeSteps(-10);
    }
    private void OnButtonMinSmallStepClick(object? sender, RoutedEventArgs e) 
    {
        MainGrid.TakeSteps(-1);
    }
    private void OnButtonPlayClick(object? sender, RoutedEventArgs e) 
    {
    	Console.WriteLine("Play/Pause");
    }
    private void OnButtonPlusSmallStepClick(object? sender, RoutedEventArgs e) 
    {
        MainGrid.TakeSteps(1);
    }
    private void OnButtonPlusManyStepsClick(object? sender, RoutedEventArgs e) 
    {
        MainGrid.TakeSteps(10);
    }
}
