#include "pch.h"
#include "verify.h"
#include "b2_collision_wrap.h"

/*
 * b2WorldManifold
 */

b2WorldManifold* b2WorldManifold_new(b2Vec2** points, float** separations)
{
    b2WorldManifold* result = new b2WorldManifold;
    *points = result->points;
    *separations = result->separations;

    return result;
}

void b2WorldManifold_Initialize(b2WorldManifold* obj, b2Manifold* manifold, b2Transform* xfA, float radiusA, b2Transform* xfB, float radiusB)
{
    VERIFY_INSTANCE;
    obj->Initialize(manifold, *xfA, radiusA, *xfB, radiusB);
}

void b2WorldManifold_get_normal(b2WorldManifold* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    *value = obj->normal;
}

void b2WorldManifold_set_normal(b2WorldManifold* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    obj->normal = *value;
}

void b2WorldManifold_delete(b2WorldManifold* obj)
{
    VERIFY_INSTANCE;
    delete obj;
}
