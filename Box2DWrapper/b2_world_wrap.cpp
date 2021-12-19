#include "pch.h"
#include "verify.h"
#include "b2_world_wrap.h"

b2World* b2World_new(b2Vec2* gravity)
{
    return new b2World(*gravity);
}

void b2World_SetContactListener(b2World* obj, b2ContactListener* listener)
{
    VERIFY_INSTANCE;
    obj->SetContactListener(listener);
}

b2Body* b2World_CreateBody(b2World* obj, b2BodyDef* def)
{
    VERIFY_INSTANCE;
    return obj->CreateBody(def);
}

void b2World_DestroyBody(b2World* obj, b2Body* body)
{
    VERIFY_INSTANCE;
    obj->DestroyBody(body);
}

b2Joint* b2World_CreateJoint(b2World* obj, b2JointDef* def)
{
    VERIFY_INSTANCE;
    return obj->CreateJoint(def);
}

void b2World_DestroyJoint(b2World* obj, b2Joint* joint)
{
    VERIFY_INSTANCE;
    obj->DestroyJoint(joint);
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

b2Joint* b2World_GetJointList(b2World* obj)
{
    VERIFY_INSTANCE;
    return obj->GetJointList();
}

void b2World_delete(b2World* obj)
{
    VERIFY_INSTANCE;
    delete obj;
}
