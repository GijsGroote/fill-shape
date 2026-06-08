// class that holds the grid, initializes the shape outline, has a draw function and also contains the algorithms to fill the shape
using System;
using System.ComponentModel.Design;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;


namespace FillShapeApp;


public class Grid
{

    private readonly int[,] _cells;
    private (int, int) _initial_start_point;

    private List<(int, int)> _ordered_filled_cells = new();
    public IReadOnlyList<(int, int)> OrderedFilledCells => _ordered_filled_cells;


    public int Rows    { get; }
    public int Columns { get; }

    public (int row, int col) InitialStartPoint
    {
        get => _initial_start_point;
        set
        {
            ValidateBounds(value.row, value.col);
            _initial_start_point = value;
        }
    }

    public Grid(int rows, int columns)
    {
        if (rows <= 0 || columns <= 0)
            throw new ArgumentException("Grid dimensions must be positive.");

        Rows    = rows;
        Columns = columns;
        _cells  = new int[rows, columns];
    }

    // Indexer — lets you use grid[row, col] syntax
    public int this[int row, int col]
    {
        get
        {
            ValidateBounds(row, col);
            return _cells[row, col];
        }
        set
        {
            ValidateBounds(row, col);
            _cells[row, col] = value;
        }
    }

    private void ValidateBounds(int row, int col)
    {
        if (row < 0 || row >= Rows)
        {
            throw new IndexOutOfRangeException(
                $"Row {row} is out of range. Valid range: 0–{Rows - 1}.");
        }

        if (col < 0 || col >= Columns)
        {
            throw new IndexOutOfRangeException(
                $"Column {col} is out of range. Valid range: 0–{Columns - 1}.");
        }
    }

    public void AddFilledCell(int row, int col)
    {
        this.ValidateBounds(row, col);

        _ordered_filled_cells.Add((row, col));
    }

    public void PrintGrid()
    {
        // top border
        Console.Write("┌");
        Console.Write(new string('─', (Columns * 2) - 1));
        Console.WriteLine("┐");

        // rows
        for (int i = 0; i < Rows; i++)
        {
            Console.Write("│");
            for (int j = 0; j < Columns; j++)
            {
                Console.Write(_cells[i, j] == 1 ? "X" : ".");
                if (j < Columns - 1) Console.Write(" ");
            }
            Console.WriteLine("│");
        }

        // bottom border
        Console.Write("└");
        Console.Write(new string('─', Columns * 2 - 1));
        Console.WriteLine("┘");
    }

    public void FillShapeSingleQueue((int row, int col) StartingPoint)
    {
        // assume that a shape contour is draw in the grid
        // assume that the starting point in within the shape contour
        
        // make 4 queue's one to check each direction (N, E, S, W) 
        // make a single queue 

        // while there are (x,y) coordinates in one of the queue's, 
        // pop the point    
        // if already a 1, continue
        // if still a 0, make it a 1, then check all surrounding points which are 0
        // add points to 

    }
    
    public void FillShapeDirectionalQueues((int row, int col) StartingPoint)
    {
        // assume that a shape contour is draw in the grid
        // assume that the starting point in within the shape contour
        
        // make 4 queue's one to check each direction (N, E, S, W) 
        // make a single queue 

        // while there are (x,y) coordinates in one of the queue's, 
        // pop the point  
        // if already a 1, continue
        // if still a 0, make it a 1, then check all surrounding points which are 0
        // add points to directional queue
    }

    public void FillShapeRecursively((int row, int col) StartPoint = default)
    {
        // assume that the starting point in within the shape contour
        var (row, col) = StartPoint == default ? _initial_start_point : StartPoint;

        // if StartingPoint is 1, return
        if (this[row, col] == 1) {
            return;
        }

        // else make Stating point 1
        this[row,col] = 1;
        this.AddFilledCell(row,col);

        // call function FillShapeRecursively recursively for every neighboring point which is 0.
        foreach ((int d_row, int d_col) in new[]{(1, 0), (-1, 0), (0, 1), (0, -1)})
        {
            if (this[row + d_row, col + d_col] == 0)
                this.FillShapeRecursively((row + d_row, col + d_col));
        }
    }

    private double Distance((int row, int col) a, (int row, int col) b)
    {
        int dRow = b.row - a.row;
        int dCol = b.col - a.col;
        return Math.Sqrt(dRow * dRow + dCol * dCol);
    }

    private void SetLineContour((int row, int col)[] corners)
    {
        if (corners.Length < 2)
            throw new ArgumentException("Need at least 2 corners to draw a line.");

        for (int i = 0; i < corners.Length - 1; i++)
        {
            DrawLine(corners[i], corners[i + 1]);
        }
    }

    private void DrawLine((int row, int col) start, (int row, int col) end)
    {
        int row0 = start.row, col0 = start.col;
        int row1 = end.row,   col1 = end.col;

        int dRow = Math.Abs(row1 - row0);
        int dCol = Math.Abs(col1 - col0);

        int stepRow = row0 < row1 ? 1 : -1;
        int stepCol = col0 < col1 ? 1 : -1;

        int err = dCol - dRow;

        while (true)
        {
            this[row0, col0] = 1;

            if (row0 == row1 && col0 == col1) break;

            int e2 = 2 * err;

            if (e2 > -dRow) { err -= dRow; col0 += stepCol; }
            if (e2 <  dCol) { err += dCol; row0 += stepRow; }
        }
    }

    public void SetRectangleContour((int row, int col)[] corners)
    {
        if (corners.Length != 4)
        {
            throw new ArgumentException($"expected 4 corners points, got: {corners.Length}");
        }

            // check parallel sides are of equal length
            foreach ((double side0, double side1) in new (double, double)[] {
            (this.Distance(corners[0], corners[1]), this.Distance(corners[2], corners[3])),
            (this.Distance(corners[1], corners[2]), this.Distance(corners[3], corners[0])),
        })
        {
            if (Math.Abs(side0 - side1) > 0.0001)
                throw new ArgumentException($"rectangle sides are not equal, found {side0} and {side1}");
        }

        // draw rectangle contours
        this.SetLineContour(corners);
        this.SetLineContour([corners[0], corners[3]]);
    }

    private void SetCirclePoints((int row, int col) center, int row, int col)
    {
        this[center.row + row, center.col + col] = 1;
        this[center.row - row, center.col + col] = 1;
        this[center.row + row, center.col - col] = 1;
        this[center.row - row, center.col - col] = 1;
        this[center.row + col, center.col + row] = 1;
        this[center.row - col, center.col + row] = 1;
        this[center.row + col, center.col - row] = 1;
        this[center.row - col, center.col - row] = 1;
    }

    public void SetCircleContour((int row, int col) center, int radius)
    {
        if (radius <= 0)
            throw new ArgumentException("Radius must be positive.");

        int temp_row = 0;
        int temp_col = radius;
        int diameter = 3 - (2 * radius);

        while (temp_row <= temp_col)
        {
            // plots all 8 symmetric points
            SetCirclePoints(center, temp_row, temp_col);

            if (diameter < 0)
            {
                diameter += (4 * temp_row) + 6;
            }
            else
            {
                diameter += (4 * (temp_row - temp_col)) + 10;
                temp_col--;
            }
            temp_row++;
        }
    }

    public void SetStarContour(int centerRow, int centerCol, int outerRadius, int innerRadius, int points = 5)
    {
        if (points < 3)
            throw new ArgumentException("A star needs at least 3 points.");
        if (innerRadius >= outerRadius)
            throw new ArgumentException("Inner radius must be smaller than outer radius.");

        (int row, int col)[] vertices = new (int row, int col)[points * 2];

        for (int i = 0; i < points * 2; i++)
        {
            double radius = i % 2 == 0 ? outerRadius : innerRadius;
            double angle = (Math.PI * i / points) - (Math.PI / 2);

            vertices[i] = (
                centerRow + (int)Math.Round(radius * Math.Sin(angle)),
                centerCol + (int)Math.Round(radius * Math.Cos(angle))
            );
        }

        // draw each arm and mirror it exactly to the opposite side
        for (int i = 0; i < points * 2; i++)
        {
            var from = vertices[i];
            var to   = vertices[(i + 1) % (points * 2)];

            // draw original line segment
            SetLineContour([from, to]);

            // mirror every drawn point through center
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    if (_cells[row, col] == 1)
                    {
                        int mirrorRow = (2 * centerRow) - row;
                        int mirrorCol = (2 * centerCol) - col;
                        if (mirrorRow >= 0 && mirrorRow < Rows &&
                            mirrorCol >= 0 && mirrorCol < Columns)
                        {
                            _cells[mirrorRow, mirrorCol] = 1;
                        }
                    }
                }
            }
        }
    }

    public void SetLineShapeContour((int row, int col)[] corners)
    {
        // check if the shape is valid.

        this.SetLineContour(corners);
    }
}
