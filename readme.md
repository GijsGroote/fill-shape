# Fill Shape Application

A contour of a predefined shape is drawn on a grid consisting of a specified number of rows and columns.

The available contours of shapes are:

* Rectangle
* Circle
* Star
* LineShape (provide points of a simple closed contour)

The goal is to fill the area enclosed by the contour, starting from a given point inside the shape and continuing until the contour boundary is reached.

## Algorithms

Two flood-fill algorithms are implemented:

1. **Recursive**
2. **Iterative**

Both algorithms can be configured to prioritize exploration in either the horizontal or vertical direction.

## Question

**Is prioritizing horizontal exploration faster than prioritizing vertical exploration?**

## Visualization

For small grids (fewer than ~100 rows and ~100 columns), the grid can be visualized.

```bash
cd FillShapeApp
dotnet run
```

## Benchmarking

A benchmark (`HorVSVerBenchmark.cs`) is included to compare the performance of horizontal and vertical exploration strategies on large shapes.

To run the benchmark:

```bash
cd FillShapeApp
dotnet run --benchmark
```

## Results

### Recursive vs. Iterative

The recursive implementation eventually causes a stack overflow when the grid size approaches approximately **200 × 200** cells. For larger grids, the iterative implementation is therefore required.

### Horizontal vs. Vertical Exploration

Benchmark results indicate a small but consistent performance difference. Prioritizing horizontal exploration appears to be slightly faster than prioritizing vertical exploration.

For a rectangle covering almost the entire **10,000 × 10,000** grid:

```text
~/Documents/fill-shape/FillShapeApp$ dotnet run --benchmark
Filled shape iteratively (horizontal priority) in: 14.7705438 sec
Filled shape iteratively (vertical priority)   in: 18.2770467 sec

~/Documents/fill-shape/FillShapeApp$ dotnet run --benchmark
Filled shape iteratively (horizontal priority) in: 15.2436845 sec
Filled shape iteratively (vertical priority)   in: 18.8212812 sec

~/Documents/fill-shape/FillShapeApp$ dotnet run --benchmark
Filled shape iteratively (horizontal priority) in: 16.1466806 sec
Filled shape iteratively (vertical priority)   in: 18.4264620 sec

~/Documents/fill-shape/FillShapeApp$ dotnet run --benchmark
Filled shape iteratively (horizontal priority) in: 14.7856516 sec
Filled shape iteratively (vertical priority)   in: 16.5868381 sec
```

In these tests, horizontal exploration consistently outperformed vertical exploration.

A likely explanation is that the grid is stored in row-major order. Horizontal traversal accesses adjacent memory locations more frequently, resulting in better CPU cache utilization and fewer cache misses.

## Larger Test

Next, a benchmark was performed using a rectangle covering almost the entire **15 000 × 15 000** grid, output:
```text
Filled shape iteratively horizontally in: 36.7993731 sec
Filled grid iteratively vertically in: 42.461687 sec
```

Next, a benchmark was performed using a rectangle covering almost the entire **50 000 × 50 000** grid, output:

```text
Out of memory.
```

Ah, found a limit there.

For now it will be concluded that searching horizontal has is faster compared to searching vertical. But why this difference exists is up for debate. 

