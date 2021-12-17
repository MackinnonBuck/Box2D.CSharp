#pragma once

#include "api.h"

#include <box2d/b2_polygon_shape.h>

extern "C"
{
    BOX2D_API b2PolygonShape* b2PolygonShape_new();
    BOX2D_API int32 b2PolygonShape_GetChildCount(b2PolygonShape* obj);
    BOX2D_API void b2PolygonShape_GetCentroid(b2PolygonShape* obj, b2Vec2* value);
    BOX2D_API void b2PolygonShape_Set(b2PolygonShape* obj, b2Vec2* points, int32 count);
    BOX2D_API void b2PolygonShape_SetAsBox(b2PolygonShape* obj, float hx, float hy);
    BOX2D_API void b2PolygonShape_SetAsBox2(b2PolygonShape* obj, float hx, float hy, b2Vec2 center, float angle);
    BOX2D_API void b2PolygonShape_ComputeAABB(b2PolygonShape* obj, b2AABB* aabb, b2Transform transform, int32 childIndex);
    BOX2D_API void b2PolygonShape_ComputeMass(b2PolygonShape* obj, b2MassData* massData, float density);
    BOX2D_API bool b2PolygonShape_RayCast(b2PolygonShape* obj, b2RayCastOutput* output, b2RayCastInput* input, b2Transform transform, int32 childIndex);
    BOX2D_API bool b2PolygonShape_TestPoint(b2PolygonShape* obj, b2Transform transform, b2Vec2 p);
    BOX2D_API void b2PolygonShape_delete(b2PolygonShape* obj);
}
