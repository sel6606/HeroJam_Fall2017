using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    North,
    East,
    South,
    West
}

/// <summary>
/// Class that holds various directional information
/// </summary>
public static class ForestDirections
{


    //Directional vectors
    private static IntVector2[] vectors =
    {
        new IntVector2(0, 1),
        new IntVector2(1, 0),
        new IntVector2(0, -1),
        new IntVector2(-1, 0)
    };

    //The opposites of each direction enum
    private static Direction[] opposites =
    {
        Direction.South,
        Direction.West,
        Direction.North,
        Direction.East
    };

    //The rotations for each direction enum
    private static Quaternion[] rotations =
    {
        Quaternion.identity,
        Quaternion.Euler(0f,90f,0f),
        Quaternion.Euler(0f,180f,0f),
        Quaternion.Euler(0f,270f,0f)
    };

    public const int Count = 4;

    /// <summary>
    /// Returns a random Direction value
    /// </summary>
    /// <returns>The direction</returns>
    public static Direction RandomValue()
    {
        return (Direction)Random.Range(0, Count);
    }

    /// <summary>
    /// Converts a direction into a IntVector2
    /// </summary>
    /// <param name="direction">The direction</param>
    /// <returns>The IntVector2</returns>
    public static IntVector2 ToIntVector2(Direction direction)
    {
        return vectors[(int)direction];
    }

    /// <summary>
    /// Returns the direction opposite of the given direction
    /// </summary>
    /// <param name="direction">The direction to reverse</param>
    /// <returns>The opposite direction</returns>
    public static Direction GetOpposite(Direction direction)
    {
        return opposites[(int)direction];
    }

    /// <summary>
    /// Returns the correspoding rotation for the given direction
    /// </summary>
    /// <param name="dir">The direction</param>
    /// <returns>The rotation</returns>
    public static Quaternion ToRotation(Direction dir)
    {
        return rotations[(int)dir];
    }

}
