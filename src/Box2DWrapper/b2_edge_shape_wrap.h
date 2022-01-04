#pragma once

#include "api.h"

#include <box2d/b2_edge_shape.h>

extern "C"
{
    /*
     * b2EdgeShape
     */
    BOX2D_API b2EdgeShape* b2EdgeShape_new();
    BOX2D_API void b2EdgeShape_SetOneSided(b2EdgeShape* obj, b2Vec2* v0, b2Vec2* v1, b2Vec2* v2, b2Vec2* v3);
    BOX2D_API void b2EdgeShape_SetTwoSided(b2EdgeShape* obj, b2Vec2* v1, b2Vec2* v2);
    BOX2D_API void b2EdgeShape_get_m_vertex0(b2EdgeShape* obj, b2Vec2* value);
    BOX2D_API void b2EdgeShape_get_m_vertex1(b2EdgeShape* obj, b2Vec2* value);
    BOX2D_API void b2EdgeShape_get_m_vertex2(b2EdgeShape* obj, b2Vec2* value);
    BOX2D_API void b2EdgeShape_get_m_vertex3(b2EdgeShape* obj, b2Vec2* value);
    BOX2D_API bool b2EdgeShape_get_m_oneSided(b2EdgeShape* obj);
    BOX2D_API void b2EdgeShape_reset(b2EdgeShape* obj);
}
