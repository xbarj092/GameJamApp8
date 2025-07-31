using System;

[Serializable]
public class GridNode
{
    public int X;
    public int Y;

    public bool Valid;

    private Grid<GridNode> _grid;

    public GridNode(Grid<GridNode> grid, int x, int y)
    {
        _grid = grid;
        X = x;
        Y = y;
    }
}
