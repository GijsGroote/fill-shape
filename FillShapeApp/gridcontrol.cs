using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

using AppGrid = FillShapeApp.Grid;  // alias to avoid conflict

namespace FillShapeApp;


public class GridControl : Control
{
    // grid property
    private AppGrid? _grid;

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
    }
}
