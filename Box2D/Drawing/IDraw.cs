using Box2D.Collections;
using Box2D.Math;
using System.Numerics;

namespace Box2D.Drawing;

public interface IDraw
{
    void DrawPolygon(in ArrayRef<Vector2> vertices, Color color);
    void DrawSolidPolygon(in ArrayRef<Vector2> vertices, Color color);
    void DrawCircle(Vector2 center, float radius, Color color);
    void DrawSolidCircle(Vector2 center, float radius, Vector2 axis, Color color);
    void DrawSegment(Vector2 p1, Vector2 p2, Color color);
    void DrawTransform(Transform xf);
    void DrawPoint(Vector2 p, float size, Color color);
}
