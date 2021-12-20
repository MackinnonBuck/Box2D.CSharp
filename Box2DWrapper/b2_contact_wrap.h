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
    BOX2D_API b2Fixture* b2Contact_GetFixtureA(b2Contact* obj);
    BOX2D_API int b2Contact_GetChildIndexA(b2Contact* obj);
    BOX2D_API b2Fixture* b2Contact_GetFixtureB(b2Contact* obj);
    BOX2D_API int b2Contact_GetChildIndexB(b2Contact* obj);
    BOX2D_API float b2Contact_GetFriction(b2Contact* obj);
    BOX2D_API void b2Contact_SetFriction(b2Contact* obj, float friction);
    BOX2D_API void b2Contact_ResetFriction(b2Contact* obj);
    BOX2D_API float b2Contact_GetRestitution(b2Contact* obj);
    BOX2D_API void b2Contact_SetRestitution(b2Contact* obj, float restitution);
    BOX2D_API void b2Contact_ResetRestitution(b2Contact* obj);
    BOX2D_API float b2Contact_GetRestitutionThreshold(b2Contact* obj);
    BOX2D_API void b2Contact_SetRestitutionThreshold(b2Contact* obj, float threshold);
    BOX2D_API void b2Contact_ResetRestitutionThreshold(b2Contact* obj);
    BOX2D_API float b2Contact_GetTangentSpeed(b2Contact* obj);
    BOX2D_API void b2Contact_SetTangentSpeed(b2Contact* obj, float speed);
    BOX2D_API void b2Contact_Evaluate(b2Contact* obj, b2Manifold* manifold, b2Transform* xfA, b2Transform* xfB);
}
