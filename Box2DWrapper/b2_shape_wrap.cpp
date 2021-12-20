#include "pch.h"
#include "verify.h"
#include "b2_shape_wrap.h"

int32 b2Shape_GetChildCount(b2Shape* obj)
{
    VERIFY_INSTANCE;
    return obj->GetChildCount();
}

bool b2Shape_TestPoint(b2Shape* obj, b2Transform* transform, b2Vec2* p)
{
    VERIFY_INSTANCE;
    return obj->TestPoint(*transform, *p);
}

bool b2Shape_RayCast(b2Shape* obj, b2RayCastOutput* output, b2RayCastInput* input, b2Transform* transform, int32 childIndex)
{
    VERIFY_INSTANCE;
    return obj->RayCast(output, *input, *transform, childIndex);
}

void b2Shape_ComputeAABB(b2Shape* obj, b2AABB* aabb, b2Transform* transform, int32 childIndex)
{
    VERIFY_INSTANCE;
    obj->ComputeAABB(aabb, *transform, childIndex);
}

void b2Shape_ComputeMass(b2Shape* obj, b2MassData* massData, float density)
{
    VERIFY_INSTANCE;
    obj->ComputeMass(massData, density);
}

float b2Shape_get_m_radius(b2Shape* obj)
{
    VERIFY_INSTANCE;
    return obj->m_radius;
}

void b2Shape_set_m_radius(b2Shape* obj, float value)
{
    VERIFY_INSTANCE;
    obj->m_radius = value;
}

void b2Shape_delete(b2Shape* obj)
{
    VERIFY_INSTANCE;
    delete obj;
}
