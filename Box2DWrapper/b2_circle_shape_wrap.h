#pragma once

#include "api.h"

#include <box2d/b2_circle_shape.h>

extern "C"
{
    BOX2D_API b2CircleShape* b2CircleShape_new();
    BOX2D_API void b2CircleShape_get_m_p(b2CircleShape* obj, b2Vec2* value);
}
