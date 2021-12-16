#include "pch.h"
#include "verify.h"
#include "b2_body_wrap.h"

void b2Body_GetPosition(b2Body* obj, b2Vec2* v)
{
    VERIFY_INSTANCE;
    *v = obj->GetPosition();
}

void b2Body_SetLinearVelocity(b2Body* obj, const b2Vec2* v)
{
    VERIFY_INSTANCE;
    obj->SetLinearVelocity(*v);
}

b2Body* b2Body_GetNext(b2Body* obj)
{
    VERIFY_INSTANCE;
    return obj->GetNext();
}

uintptr_t b2Body_GetUserData(b2Body* obj)
{
    VERIFY_INSTANCE;
    return obj->GetUserData().pointer;
}
