#pragma once

#include "api.h"

#include <box2d/b2_circle_shape.h>

extern "C"
{
    BOX2D_API b2CircleShape* b2CircleShape_new();
    BOX2D_API void b2CircleShape_get_m_p(b2CircleShape* obj, b2Vec2* value);
    BOX2D_API void b2CircleShape_set_m_p(b2CircleShape* obj, b2Vec2* value);
    BOX2D_API void b2CircleShape_reset(b2CircleShape* obj);
}
