#include "pch.h"
#include "verify.h"
#include "b2_world_wrap.h"

b2World* b2World_new(const b2Vec2* gravity)
{
    return new b2World(*gravity);
}

b2Body* b2World_CreateBody(b2World* obj, const b2BodyDef* def)
{
    VERIFY_INSTANCE;
    return obj->CreateBody(def);
}

void b2World_DestroyBody(b2World* obj, b2Body* body)
{
    VERIFY_INSTANCE;
    obj->DestroyBody(body);
}

void b2World_Step(b2World* obj, float timeStep, int32 velocityIterations, int32 positionIterations)
{
    VERIFY_INSTANCE;
    obj->Step(timeStep, velocityIterations, positionIterations);
}

void b2World_ClearForces(b2World* obj)
{
    VERIFY_INSTANCE;
    obj->ClearForces();
}

b2Body* b2World_GetBodyList(b2World* obj)
{
    VERIFY_INSTANCE;
    return obj->GetBodyList();
}

void b2World_delete(b2World* obj)
{
    VERIFY_INSTANCE;
    delete obj;
}
