#pragma once

#include "api.h"

#include <box2d/b2_collision.h>

extern "C"
{
    /*
     * Top-level functions
     */
    BOX2D_API void b2GetPointStates_wrap(b2PointState* state1, b2PointState* state2, b2Manifold* manifold1, b2Manifold* manifold2);

    /*
     * b2Manifold
     */
    BOX2D_API b2ManifoldPoint* b2Manifold_get_points(b2Manifold* obj, int32* pointCount);
    BOX2D_API void b2Manifold_get_localNormal(b2Manifold* obj, b2Vec2* value);
    BOX2D_API void b2Manifold_set_localNormal(b2Manifold* obj, b2Vec2* value);
    BOX2D_API void b2Manifold_get_localPoint(b2Manifold* obj, b2Vec2* value);
    BOX2D_API void b2Manifold_set_localPoint(b2Manifold* obj, b2Vec2* value);
    BOX2D_API b2Manifold::Type b2Manifold_get_type(b2Manifold* obj);
    BOX2D_API void b2Manifold_set_type(b2Manifold* obj, b2Manifold::Type value);

    /*
     * b2WorldManifold
     */
    BOX2D_API b2WorldManifold* b2WorldManifold_new(b2Vec2** points, float** separations);
    BOX2D_API void b2WorldManifold_Initialize(b2WorldManifold* obj, b2Manifold* manifold, b2Transform* xfA, float radiusA, b2Transform* xfB, float radiusB);
    BOX2D_API void b2WorldManifold_get_normal(b2WorldManifold* obj, b2Vec2* value);
    BOX2D_API void b2WorldManifold_set_normal(b2WorldManifold* obj, b2Vec2* value);
    BOX2D_API void b2WorldManifold_delete(b2WorldManifold* obj);
}
