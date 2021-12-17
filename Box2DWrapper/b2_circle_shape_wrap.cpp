#include "pch.h"
#include "verify.h"
#include "b2_circle_shape_wrap.h"

b2CircleShape* b2CircleShape_new()
{
    return new b2CircleShape;
}

void b2CircleShape_GetP(b2CircleShape* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    *value = obj->m_p;
}
