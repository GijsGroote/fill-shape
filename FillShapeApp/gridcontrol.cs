using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

using AppGrid = FillShapeApp.Grid;  // alias to avoid conflict

namespace FillShapeApp;


public class GridControl : Control
{
    // grid property
    private AppGrid? _grid;
    private int _max_filled_cells_idx;

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
    public double MaxFilledCellsIdx { get; set; } = 0;

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
                var color = _grid[row, col] == 1
                    ? Brushes.White
                    : Brushes.Black;

                DrawColoredRectangle(row, col, color, context);
            }
        }

        for (int filled_cell_idx = 0; filled_cell_idx < _max_filled_cells_idx; filled_cell_idx++)
        {
            (int row, int col) = _grid.OrderedFilledCells[filled_cell_idx];
            DrawColoredRectangle(row, col, Brushes.Orange, context);
            
        }

        // Draw the shape's contour
        foreach ((int row, int col) in _grid.ContourCells)
        {
            DrawColoredRectangle(row, col, Brushes.Green, context);
        }


        // Starting point
        var startRect = new Rect(
            _grid.InitialStartCell.col * CellSize,
            _grid.InitialStartCell.row * CellSize,
            CellSize,
            CellSize);

        context.DrawEllipse(
            Brushes.Red,
            null,
            startRect.Center,
            CellSize / 3.0,
            CellSize / 3.0);
    }
    private void DrawColoredRectangle(int row, int col, IBrush color, DrawingContext context)
    {
        var rect = new Rect(
            col * CellSize,
            row * CellSize,
            CellSize,
            CellSize);

        context.FillRectangle(color, rect);

        context.DrawRectangle(
            null,
            new Pen(Brushes.Gray, 1),
            rect);
    }
    public void TakeSteps(int steps = 1)
    {
        _max_filled_cells_idx = Math.Clamp(_max_filled_cells_idx + steps, 0, _grid.OrderedFilledCells.Count);
        InvalidateVisual();
    }
}
