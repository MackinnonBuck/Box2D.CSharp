#include "pch.h"
#include "verify.h"
#include "b2_shape_wrap.h"

float b2Shape_GetRadius(b2Shape* obj)
{
    VERIFY_INSTANCE;
    return obj->m_radius;
}
