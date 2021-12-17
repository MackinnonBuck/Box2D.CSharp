#include "pch.h"
#include "verify.h"
#include "b2_fixture_wrap.h"

b2Shape* b2Fixture_GetShape(b2Fixture* obj)
{
    VERIFY_INSTANCE;
    return obj->GetShape();
}

b2Fixture* b2Fixture_GetNext(b2Fixture* obj)
{
    VERIFY_INSTANCE;
    return obj->GetNext();
}

uintptr_t b2Fixture_GetUserData(b2Fixture* obj)
{
    VERIFY_INSTANCE;
    return obj->GetUserData().pointer;
}
