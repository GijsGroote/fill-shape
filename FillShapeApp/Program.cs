using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace FillShapeApp;

static class Program
{
    static void Main(string[] args)
    {

        Console.WriteLine("Well we are heare");


        // arrange
        Grid grid = new(rows: 30, columns: 30);
        (int row, int col)[] corners = [
            (1,1),
            (1,28),
            (28,28),
            (28,1),
        ];
        grid.SetRectangleContour(corners);

        grid.PrintGrid();
        // act
        grid.FillShapeRecursively((4, 4));

        // // grid.SetCircleContour((10, 15), 9);
        // // grid.SetStarContour(10, 10, outerRadius: 8, innerRadius: 3, points: 6);
        // grid.SetLineShapeContour([
        //     (1,1),  (1,28), (12,28), (11, 20), (3, 21), (5, 5),
        //     (13,5),(14, 25), (25, 25), (27, 1), (1,1)]);

        grid.PrintGrid();
    }
}
