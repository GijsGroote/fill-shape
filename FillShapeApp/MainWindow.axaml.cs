using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using AppGrid = FillShapeApp.Grid;

namespace FillShapeApp;


public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        Grid grid = new AppGrid(rows: 50, columns: 60);

        // Choose: Rectangle, Star, Circle, LineShape
        String ContourType = "LineShape"; 

        switch(ContourType) 
        {
          case "Rectangle":
            grid.SetRectangleContour([ (5,1), (5,38), (38,38), (38,1) ]);
            grid.InitialStartCell = (25, 25);
            break;

          case "Star":
            grid.SetStarContour(centerRow: 23, centerCol: 24, outerRadius: 20, innerRadius: 10);
            grid.InitialStartCell = (15, 25);
            break;

          case "Circle":
            grid.SetCircleContour((22, 25), 20);
            grid.InitialStartCell = (15, 25);
            break;

          case "LineShape":
            grid.SetLineShapeContour([ (1,1),(46,1),(46,46),(14,46),(14,26),(22,26),
                    (22,40),(39,40),(39,12),(12,12),(8,22), (8,40), (1,40),(1,1)]);
            grid.InitialStartCell = (5, 25);
            break;

          default:
            throw new ArgumentException("Unknown contour type: "+ContourType);
        }

        grid.FillShapeIterativelyTracked(direction: AppGrid.ExploreDirection.Vertical);
        // grid.FillShapeRecursivelyTracked(direction: AppGrid.ExploreDirection.Vertical);
        // grid.FillShapeTrackedTracked(direction: ExploreDirection.Horizontal);

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
        MainGrid.PlayPauseAnimation();
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
