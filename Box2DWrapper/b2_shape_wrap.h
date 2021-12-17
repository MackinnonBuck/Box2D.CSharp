#pragma once

#include "api.h"

#include <box2d/b2_shape.h>

extern "C"
{
    BOX2D_API float b2Shape_GetRadius(b2Shape* obj);
}
