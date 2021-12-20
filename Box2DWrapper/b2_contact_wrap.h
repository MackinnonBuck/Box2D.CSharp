#pragma once

#include "api.h"

#include <box2d/b2_contact.h>

extern "C"
{
    BOX2D_API b2Manifold* b2Contact_GetManifold(b2Contact* obj);
    BOX2D_API void b2Contact_GetWorldManifold(b2Contact* obj, b2WorldManifold* worldManifold);
    BOX2D_API bool b2Contact_IsTouching(b2Contact* obj);
    BOX2D_API bool b2Contact_IsEnabled(b2Contact* obj);
    BOX2D_API void b2Contact_SetEnabled(b2Contact* obj, bool flag);
    BOX2D_API b2Contact* b2Contact_GetNext(b2Contact* obj);
}
