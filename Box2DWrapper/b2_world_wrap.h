#pragma once

#include "api.h"

#include <box2d/b2_world.h>

extern "C"
{
    BOX2D_API b2World* b2World_new(const b2Vec2* gravity);
    BOX2D_API b2Body* b2World_CreateBody(b2World* obj, const b2BodyDef* def);
    BOX2D_API void b2World_DestroyBody(b2World* obj, b2Body* body);
    BOX2D_API b2Body* b2World_GetBodyList(b2World* obj);
    BOX2D_API void b2World_delete(b2World* obj);
}
