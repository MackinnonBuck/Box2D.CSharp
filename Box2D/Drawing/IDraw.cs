using Box2D.Collections;
using Box2D.Math;
using System.Numerics;

namespace Box2D.Drawing;

/// <summary>
/// Implement and register this interface with a <see cref="Dynamics.World"/> to provide debug
/// drawing of physics entities in your game.
/// </summary>
public interface IDraw
{
    /// <summary>
    /// Draw a closed polygon provided in CCW order.
    /// </summary>
    void DrawPolygon(in ArrayRef<Vector2> vertices, Color color);

    /// <summary>
    /// Draw a solid closed polygon provided in CCW order.
    /// </summary>
    void DrawSolidPolygon(in ArrayRef<Vector2> vertices, Color color);

    /// <summary>
    /// Draw a circle.
    /// </summary>
    void DrawCircle(Vector2 center, float radius, Color color);

    /// <summary>
    /// Draw a solid circle.
    /// </summary>
    void DrawSolidCircle(Vector2 center, float radius, Vector2 axis, Color color);

    /// <summary>
    /// Draw a line segment.
    /// </summary>
    void DrawSegment(Vector2 p1, Vector2 p2, Color color);

    /// <summary>
    /// Draw a transform. Choose your own length scale.
    /// </summary>
    void DrawTransform(Transform xf);

    /// <summary>
    /// Draw a point.
    /// </summary>
    void DrawPoint(Vector2 p, float size, Color color);
}
