// struct is value type whereas class is reference type
using System;

// IEquatable to implements Equals method
public struct GridPosition : IEquatable<GridPosition>
{
    public int x;
    public int z;

    public GridPosition(int x, int z)
    {
        this.x = x;
        this.z = z;
    }

    public override bool Equals(object obj)
    {
        return obj is GridPosition position &&
               x == position.x &&
               z == position.z;
    }

    public bool Equals(GridPosition other)
    {
        return this == other;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(x, z);
    }

    public override string ToString()
    {
        return $"x: {x}, z: {z}";
    }

    // used in Unit.cs to do newGridPosition != gridPosition
    public static bool operator ==(GridPosition a, GridPosition b) => a.x == b.x && a.z == b.z;
    public static bool operator !=(GridPosition a, GridPosition b) => !(a == b);

    // used in MoveAction.cs to do unitGridPosition + offsetGridPosition;
    public static GridPosition operator +(GridPosition a, GridPosition b)
    {
        return new GridPosition(a.x + b.x, a.z + b.z);
    }
    public static GridPosition operator -(GridPosition a, GridPosition b)
    {
        return new GridPosition(a.x - b.x, a.z - b.z);
    }
}