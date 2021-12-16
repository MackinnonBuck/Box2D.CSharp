#include "pch.h"
#include "verify.h"
#include "b2_polygon_shape_wrap.h"

b2PolygonShape* b2PolygonShape_new()
{
    return new b2PolygonShape;
}

int32 b2PolygonShape_GetChildCount(b2PolygonShape* obj)
{
    VERIFY_INSTANCE;
    return obj->GetChildCount();
}

void b2PolygonShape_SetAsBox(b2PolygonShape* obj, float hx, float hy)
{
    VERIFY_INSTANCE;
    obj->SetAsBox(hx, hy);
}

void b2PolygonShape_SetAsBox2(b2PolygonShape* obj, float hx, float hy, b2Vec2 center, float angle)
{
    VERIFY_INSTANCE;
    obj->SetAsBox(hx, hy, center, angle);
}

void b2PolygonShape_ComputeAABB(b2PolygonShape* obj, b2AABB* aabb, b2Transform transform, int32 childIndex)
{
    VERIFY_INSTANCE;
    obj->ComputeAABB(aabb, transform, childIndex);
}

void b2PolygonShape_ComputeMass(b2PolygonShape* obj, b2MassData* massData, float density)
{
    VERIFY_INSTANCE;
    obj->ComputeMass(massData, density);
}

bool b2PolygonShape_RayCast(b2PolygonShape* obj, b2RayCastOutput* output, b2RayCastInput* input, b2Transform transform, int32 childIndex)
{
    VERIFY_INSTANCE;
    return obj->RayCast(output, *input, transform, childIndex);
}

bool b2PolygonShape_TestPoint(b2PolygonShape* obj, b2Transform transform, b2Vec2 p)
{
    VERIFY_INSTANCE;
    return obj->TestPoint(transform, p);
}

void b2PolygonShape_delete(b2PolygonShape* obj)
{
    VERIFY_INSTANCE;
    delete obj;
}
