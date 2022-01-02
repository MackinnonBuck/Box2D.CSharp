#include "pch.h"
#include "verify.h"
#include "b2_polygon_shape_wrap.h"

#include <memory>

b2PolygonShape* b2PolygonShape_new()
{
    return new b2PolygonShape;
}

void b2PolygonShape_Set(b2PolygonShape* obj, b2Vec2* points, int32 count)
{
    VERIFY_INSTANCE;
    obj->Set(points, count);
}

void b2PolygonShape_SetAsBox(b2PolygonShape* obj, float hx, float hy)
{
    VERIFY_INSTANCE;
    obj->SetAsBox(hx, hy);
}

void b2PolygonShape_SetAsBox2(b2PolygonShape* obj, float hx, float hy, b2Vec2* center, float angle)
{
    VERIFY_INSTANCE;
    obj->SetAsBox(hx, hy, *center, angle);
}

void b2PolygonShape_get_m_centroid(b2PolygonShape* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    *value = obj->m_centroid;
}

void b2PolygonShape_reset(b2PolygonShape* obj)
{
    VERIFY_INSTANCE;
    obj->~b2PolygonShape();
    new (obj) b2PolygonShape();
}
