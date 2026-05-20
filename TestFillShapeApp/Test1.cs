using FillShapeApp;

namespace TestFillShapeApp;

[TestClass]
public sealed class TestBlock
{
    [TestMethod]
    public void SimpleTest()
    {

        // arrange
        Grid grid = new(rows: 30, columns: 30);
        (int row, int col)[] corners = [
            (1,1),
            (1,28),
            (28,28),
            (28,1),
        ];
        grid.SetRectangleContour(corners);

        // act
        grid.FillShapeRecursively((4, 4));

        // Assert
        //
        Assert.AreEqual(1, grid[2, 6], $"Expected 1 at ({2},{6}) but got {grid[2, 6]}");
        // Check all cells inside the rectangle are 1
        // for (int row = 1; row <= 7; row++)
        // {
        //     for (int col = 5; col <= 12; col++)
        //     {
        //         Assert.AreEqual(1, grid[row, col]), 
        //             $"Expected 1 at ({row},{col}) but got {grid[row, col]}");
        //     }
        // }

    }
}
