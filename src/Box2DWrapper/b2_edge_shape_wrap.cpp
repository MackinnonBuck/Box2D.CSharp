#include "pch.h"
#include "verify.h"
#include "b2_edge_shape_wrap.h"

#include <memory>

/*
 * b2EdgeShape
 */

b2EdgeShape* b2EdgeShape_new()
{
    return new b2EdgeShape;
}

void b2EdgeShape_SetOneSided(b2EdgeShape* obj, b2Vec2* v0, b2Vec2* v1, b2Vec2* v2, b2Vec2* v3)
{
    VERIFY_INSTANCE;
    obj->SetOneSided(*v0, *v1, *v2, *v3);
}

void b2EdgeShape_SetTwoSided(b2EdgeShape* obj, b2Vec2* v1, b2Vec2* v2)
{
    VERIFY_INSTANCE;
    obj->SetTwoSided(*v1, *v2);
}

void b2EdgeShape_get_m_vertex0(b2EdgeShape* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    *value = obj->m_vertex0;
}

void b2EdgeShape_get_m_vertex1(b2EdgeShape* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    *value = obj->m_vertex1;
}

void b2EdgeShape_get_m_vertex2(b2EdgeShape* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    *value = obj->m_vertex2;
}

void b2EdgeShape_get_m_vertex3(b2EdgeShape* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    *value = obj->m_vertex3;
}

bool b2EdgeShape_get_m_oneSided(b2EdgeShape* obj)
{
    VERIFY_INSTANCE;
    return obj->m_oneSided;
}

void b2EdgeShape_reset(b2EdgeShape* obj)
{
    VERIFY_INSTANCE;
    obj->~b2EdgeShape();
    new (obj) b2EdgeShape();
}
