using Box2D.Collections;
using Box2D.Math;

namespace Box2D.Drawing;

public interface IDraw
{
    void DrawPolygon(in ArrayRef<Vec2> vertices, Color color);
    void DrawSolidPolygon(in ArrayRef<Vec2> vertices, Color color);
    void DrawCircle(Vec2 center, float radius, Color color);
    void DrawSolidCircle(Vec2 center, float radius, Vec2 axis, Color color);
    void DrawSegment(Vec2 p1, Vec2 p2, Color color);
    void DrawTransform(Transform xf);
    void DrawPoint(Vec2 p, float size, Color color);
}
