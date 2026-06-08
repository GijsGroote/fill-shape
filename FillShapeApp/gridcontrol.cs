using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

using AppGrid = FillShapeApp.Grid;  // alias to avoid conflict

namespace FillShapeApp;


public class GridControl : Control
{
    // grid property
    private AppGrid? _grid;
    private int _filled_cells_idx = 0;

    public AppGrid? Grid
    {
        get => _grid;
        set
        {
            _grid = value;
            InvalidateVisual(); // Avalonia function to redraw the grid
        }
    }

    public double CellSize { get; set; } = 20;

public override void Render(DrawingContext context)
    {
        base.Render(context);

        if (_grid == null)
            return;

        // Render the grid
        for (int row = 0; row < _grid.Rows; row++)
        {
            for (int col = 0; col < _grid.Columns; col++)
            {
                var rect = new Rect(
                    col * CellSize,
                    row * CellSize,
                    CellSize,
                    CellSize);

                var brush =
                    _grid[row, col] == 1
                    ? Brushes.Black
                    : Brushes.White;

                context.FillRectangle(brush, rect);

                context.DrawRectangle(
                    null,
                    new Pen(Brushes.Gray, 1),
                    rect);
            }
        }

        // Starting point
        var startRect = new Rect(
            _grid.InitialStartPoint.col * CellSize,
            _grid.InitialStartPoint.row * CellSize,
            CellSize,
            CellSize);

        context.DrawEllipse(
            Brushes.Red,
            null,
            startRect.Center,
            CellSize / 3.0,
            CellSize / 3.0);


    }
}
