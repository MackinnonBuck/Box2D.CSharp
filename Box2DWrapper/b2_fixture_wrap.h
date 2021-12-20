#pragma once

#include "api.h"

#include <box2d/b2_fixture.h>

extern "C"
{
    BOX2D_API b2Shape* b2Fixture_GetShape(b2Fixture* obj);
    BOX2D_API b2Fixture* b2Fixture_GetNext(b2Fixture* obj);
    BOX2D_API uintptr_t b2Fixture_GetUserData(b2Fixture* obj);
}
