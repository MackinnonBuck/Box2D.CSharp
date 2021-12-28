#pragma once

#include "api.h"

#include <box2d/b2_draw.h>

typedef void (*b2Draw_callback_DrawPolygon)(const b2Vec2* vertices, int32 vertexCount, const b2Color* color);
typedef void (*b2Draw_callback_DrawSolidPolygon)(const b2Vec2* vertices, int32 vertexCount, const b2Color* color);
typedef void (*b2Draw_callback_DrawCircle)(const b2Vec2* center, float radius, const b2Color* color);
typedef void (*b2Draw_callback_DrawSolidCircle)(const b2Vec2* center, float radius, const b2Vec2* axis, const b2Color* color);
typedef void (*b2Draw_callback_DrawSegment)(const b2Vec2* p1, const b2Vec2* p2, const b2Color* color);
typedef void (*b2Draw_callback_DrawTransform)(const b2Transform* xf);
typedef void (*b2Draw_callback_DrawPoint)(const b2Vec2* p, float size, const b2Color* color);

class b2DrawWrapper : public b2Draw
{
public:
    b2DrawWrapper(
        b2Draw_callback_DrawPolygon drawPolygon,
        b2Draw_callback_DrawSolidPolygon drawSolidPolygon,
        b2Draw_callback_DrawCircle drawCircle,
        b2Draw_callback_DrawSolidCircle drawSolidCircle,
        b2Draw_callback_DrawSegment drawSegment,
        b2Draw_callback_DrawTransform drawTransform,
        b2Draw_callback_DrawPoint drawPoint);

	void DrawPolygon(const b2Vec2* vertices, int32 vertexCount, const b2Color& color) override;
	void DrawSolidPolygon(const b2Vec2* vertices, int32 vertexCount, const b2Color& color) override;
	void DrawCircle(const b2Vec2& center, float radius, const b2Color& color) override;
	void DrawSolidCircle(const b2Vec2& center, float radius, const b2Vec2& axis, const b2Color& color) override;
	void DrawSegment(const b2Vec2& p1, const b2Vec2& p2, const b2Color& color) override;
	void DrawTransform(const b2Transform& xf) override;
	void DrawPoint(const b2Vec2& p, float size, const b2Color& color) override; 
    
private:
    b2Draw_callback_DrawPolygon m_drawPolygon;
    b2Draw_callback_DrawSolidPolygon m_drawSolidPolygon;
    b2Draw_callback_DrawCircle m_drawCircle;
    b2Draw_callback_DrawSolidCircle m_drawSolidCircle;
    b2Draw_callback_DrawSegment m_drawSegment;
    b2Draw_callback_DrawTransform m_drawTransform;
    b2Draw_callback_DrawPoint m_drawPoint;
};

extern "C"
{
    BOX2D_API b2DrawWrapper* b2DrawWrapper_new(
        b2Draw_callback_DrawPolygon drawPolygon,
        b2Draw_callback_DrawSolidPolygon drawSolidPolygon,
        b2Draw_callback_DrawCircle drawCircle,
        b2Draw_callback_DrawSolidCircle drawSolidCircle,
        b2Draw_callback_DrawSegment drawSegment,
        b2Draw_callback_DrawTransform drawTransform,
        b2Draw_callback_DrawPoint drawPoint);
    BOX2D_API uint32 b2DrawWrapper_GetFlags(b2DrawWrapper* obj);
    BOX2D_API void b2DrawWrapper_SetFlags(b2DrawWrapper* obj, uint32 flags);
    BOX2D_API void b2DrawWrapper_delete(b2DrawWrapper* obj);
}
