#include "pch.h"
#include "verify.h"
#include "b2_draw_wrap.h"

b2DrawWrapper::b2DrawWrapper(
    b2Draw_callback_DrawPolygon drawPolygon,
    b2Draw_callback_DrawSolidPolygon drawSolidPolygon,
    b2Draw_callback_DrawCircle drawCircle,
    b2Draw_callback_DrawSolidCircle drawSolidCircle,
    b2Draw_callback_DrawSegment drawSegment,
    b2Draw_callback_DrawTransform drawTransform,
    b2Draw_callback_DrawPoint drawPoint)
    : m_drawPolygon(drawPolygon)
    , m_drawSolidPolygon(drawSolidPolygon)
    , m_drawCircle(drawCircle)
    , m_drawSolidCircle(drawSolidCircle)
    , m_drawSegment(drawSegment)
    , m_drawTransform(drawTransform)
    , m_drawPoint(drawPoint)
{
}

void b2DrawWrapper::DrawPolygon(const b2Vec2* vertices, int32 vertexCount, const b2Color& color)
{
    m_drawPolygon(vertices, vertexCount, &color);
}

void b2DrawWrapper::DrawSolidPolygon(const b2Vec2* vertices, int32 vertexCount, const b2Color& color)
{
    m_drawSolidPolygon(vertices, vertexCount, &color);
}

void b2DrawWrapper::DrawCircle(const b2Vec2& center, float radius, const b2Color& color)
{
    m_drawCircle(&center, radius, &color);
}

void b2DrawWrapper::DrawSolidCircle(const b2Vec2& center, float radius, const b2Vec2& axis, const b2Color& color)
{
    m_drawSolidCircle(&center, radius, &axis, &color);
}

void b2DrawWrapper::DrawSegment(const b2Vec2& p1, const b2Vec2& p2, const b2Color& color)
{
    m_drawSegment(&p1, &p2, &color);
}

void b2DrawWrapper::DrawTransform(const b2Transform& xf)
{
    m_drawTransform(&xf);
}

void b2DrawWrapper::DrawPoint(const b2Vec2& p, float size, const b2Color& color)
{
    m_drawPoint(&p, size, &color);
}

b2DrawWrapper* b2DrawWrapper_new(
    b2Draw_callback_DrawPolygon drawPolygon,
    b2Draw_callback_DrawSolidPolygon drawSolidPolygon,
    b2Draw_callback_DrawCircle drawCircle,
    b2Draw_callback_DrawSolidCircle drawSolidCircle,
    b2Draw_callback_DrawSegment drawSegment,
    b2Draw_callback_DrawTransform drawTransform,
    b2Draw_callback_DrawPoint drawPoint)
{
    return new b2DrawWrapper(
        drawPolygon,
        drawSolidPolygon,
        drawCircle,
        drawSolidCircle,
        drawSegment,
        drawTransform,
        drawPoint);
}

uint32 b2DrawWrapper_GetFlags(b2DrawWrapper* obj)
{
    VERIFY_INSTANCE;
    return obj->GetFlags();
}

void b2DrawWrapper_SetFlags(b2DrawWrapper* obj, uint32 flags)
{
    VERIFY_INSTANCE;
    obj->SetFlags(flags);
}

void b2DrawWrapper_delete(b2DrawWrapper* obj)
{
    VERIFY_INSTANCE;
    delete obj;
}
