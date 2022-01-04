#include "pch.h"
#include "verify.h"
#include "b2_circle_shape_wrap.h"

#include <memory>

b2CircleShape* b2CircleShape_new()
{
    return new b2CircleShape;
}

void b2CircleShape_get_m_p(b2CircleShape* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    *value = obj->m_p;
}

void b2CircleShape_set_m_p(b2CircleShape* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    obj->m_p = *value;
}

void b2CircleShape_reset(b2CircleShape* obj)
{
    VERIFY_INSTANCE;
    obj->~b2CircleShape();
    new (obj) b2CircleShape();
}
