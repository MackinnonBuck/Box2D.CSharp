#include "pch.h"
#include "verify.h"
#include "b2_collision_wrap.h"

/*
 * Top-level functions
 */

void b2GetPointStates_wrap(b2PointState* state1, b2PointState* state2, b2Manifold* manifold1, b2Manifold* manifold2)
{
    b2GetPointStates(state1, state2, manifold1, manifold2);
}

/*
 * b2Manifold
 */

b2ManifoldPoint* b2Manifold_get_points(b2Manifold* obj, int32* pointCount)
{
    b2ManifoldPoint* result = obj->points;
    *pointCount = obj->pointCount;

    return result;
}

void b2Manifold_get_localNormal(b2Manifold* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    *value = obj->localNormal;
}

void b2Manifold_set_localNormal(b2Manifold* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    obj->localNormal = *value;
}

void b2Manifold_get_localPoint(b2Manifold* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    *value = obj->localPoint;
}

void b2Manifold_set_localPoint(b2Manifold* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    obj->localPoint = *value;
}

b2Manifold::Type b2Manifold_get_type(b2Manifold* obj)
{
    VERIFY_INSTANCE;
    return obj->type;
}

void b2Manifold_set_type(b2Manifold* obj, b2Manifold::Type value)
{
    VERIFY_INSTANCE;
    obj->type = value;
}

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
