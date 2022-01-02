#pragma once

#include "api.h"

#include <box2d/b2_polygon_shape.h>

extern "C"
{
    BOX2D_API b2PolygonShape* b2PolygonShape_new();
    BOX2D_API void b2PolygonShape_Set(b2PolygonShape* obj, b2Vec2* points, int32 count);
    BOX2D_API void b2PolygonShape_SetAsBox(b2PolygonShape* obj, float hx, float hy);
    BOX2D_API void b2PolygonShape_SetAsBox2(b2PolygonShape* obj, float hx, float hy, b2Vec2* center, float angle);
    BOX2D_API void b2PolygonShape_get_m_centroid(b2PolygonShape* obj, b2Vec2* value);
    BOX2D_API void b2PolygonShape_reset(b2PolygonShape* obj);
}
