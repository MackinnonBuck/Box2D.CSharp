#pragma once

#include "api.h"

#include <box2d/b2_shape.h>

extern "C"
{
    BOX2D_API int32 b2Shape_GetChildCount(b2Shape* obj);
    BOX2D_API bool b2Shape_TestPoint(b2Shape* obj, b2Transform* transform, b2Vec2* p);
    BOX2D_API bool b2Shape_RayCast(b2Shape* obj, b2RayCastOutput* output, b2RayCastInput* input, b2Transform* transform, int32 childIndex);
    BOX2D_API void b2Shape_ComputeAABB(b2Shape* obj, b2AABB* aabb, b2Transform* transform, int32 childIndex);
    BOX2D_API void b2Shape_ComputeMass(b2Shape* obj, b2MassData* massData, float density);
    BOX2D_API float b2Shape_get_m_radius(b2Shape* obj);
    BOX2D_API void b2Shape_set_m_radius(b2Shape* obj, float value);
    BOX2D_API void b2Shape_delete(b2Shape* obj);
}
