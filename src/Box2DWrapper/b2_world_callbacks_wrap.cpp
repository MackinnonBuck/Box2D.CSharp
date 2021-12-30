#include "pch.h"
#include "verify.h"
#include "b2_world_callbacks_wrap.h"

/*
 * b2DestructionListenerWrapper
 */

b2DestructionListenerWrapper::b2DestructionListenerWrapper(
    b2DestructionListener_callback_SayGoodbye_b2Joint sayGoodbyeJoint,
    b2DestructionListener_callback_SayGoodbye_b2Fixture sayGoodbyeFixture)
    : m_sayGoodbyeJoint(sayGoodbyeJoint)
    , m_sayGoodbyeFixture(sayGoodbyeFixture)
{
}

void b2DestructionListenerWrapper::SayGoodbye(b2Joint* joint)
{
    m_sayGoodbyeJoint(joint);
}

void b2DestructionListenerWrapper::SayGoodbye(b2Fixture* fixture)
{
    m_sayGoodbyeFixture(fixture);
}

/*
 * b2ContactListenerWrapper
 */

b2ContactListenerWrapper::b2ContactListenerWrapper(
    b2ContactListener_callback_BeginContact beginContact,
    b2ContactListener_callback_EndContact endContact,
    b2ContactListener_callback_PreSolve preSolve,
    b2ContactListener_callback_PostSolve postSolve)
    : m_beginContact(beginContact)
    , m_endContact(endContact)
    , m_preSolve(preSolve)
    , m_postSolve(postSolve)
{
}

void b2ContactListenerWrapper::BeginContact(b2Contact* contact)
{
    m_beginContact(contact);
}

void b2ContactListenerWrapper::EndContact(b2Contact* contact)
{
    m_endContact(contact);
}

void b2ContactListenerWrapper::PreSolve(b2Contact* contact, const b2Manifold* oldManifold)
{
    m_preSolve(contact, oldManifold);
}

void b2ContactListenerWrapper::PostSolve(b2Contact* contact, const b2ContactImpulse* impulse)
{
    m_postSolve(contact, impulse);
}

/*
 * b2QueryCallbackWrapper
 */

b2QueryCallbackWrapper::b2QueryCallbackWrapper(
    b2QueryCallback_callback_ReportFixture reportFixture)
    : m_reportFixture(reportFixture)
{
}

bool b2QueryCallbackWrapper::ReportFixture(b2Fixture* fixture)
{
    return m_reportFixture(fixture);
}

/*
 * b2RayCastCallbackWrapper
 */

b2RayCastCallbackWrapper::b2RayCastCallbackWrapper(
    b2RayCastCallback_callback_ReportFixture reportFixture)
    : m_reportFixture(reportFixture)
{
}

float b2RayCastCallbackWrapper::ReportFixture(b2Fixture* fixture, const b2Vec2& point, const b2Vec2& normal, float fraction)
{
    return m_reportFixture(fixture, &point, &normal, fraction);
}

/*
 * b2DestructionListenerWrapper
 */

b2DestructionListenerWrapper* b2DestructionListenerWrapper_new(
    b2DestructionListener_callback_SayGoodbye_b2Joint sayGoodbyeJoint,
    b2DestructionListener_callback_SayGoodbye_b2Fixture sayGoodbyeFixture)
{
    return new b2DestructionListenerWrapper(sayGoodbyeJoint, sayGoodbyeFixture);
}

void b2DestructionListenerWrapper_delete(b2DestructionListenerWrapper* obj)
{
    VERIFY_INSTANCE;
    delete obj;
}

/*
 * b2ContactImpulse
 */

void b2ContactImpulse_get_impulses(b2ContactImpulse* obj, float** normalImpulses, float** tangentImpulses, int32* count)
{
    VERIFY_INSTANCE;
    *normalImpulses = obj->normalImpulses;
    *tangentImpulses = obj->tangentImpulses;
    *count = obj->count;
}

/*
 * b2ContactListenerWrapper
 */

b2ContactListenerWrapper* b2ContactListenerWrapper_new(
    b2ContactListener_callback_BeginContact beginContact,
    b2ContactListener_callback_EndContact endContact,
    b2ContactListener_callback_PreSolve preSolve,
    b2ContactListener_callback_PostSolve postSolve)
{
    return new b2ContactListenerWrapper(beginContact, endContact, preSolve, postSolve);
}

void b2ContactListenerWrapper_delete(b2ContactListenerWrapper* obj)
{
    VERIFY_INSTANCE;
    delete obj;
}

/*
 * b2QueryCallbackWrapper
 */

b2QueryCallbackWrapper* b2QueryCallbackWrapper_new(b2QueryCallback_callback_ReportFixture reportFixture)
{
    return new b2QueryCallbackWrapper(reportFixture);
}

void b2QueryCallbackWrapper_delete(b2QueryCallbackWrapper* obj)
{
    VERIFY_INSTANCE;
    delete obj;
}

/*
 * b2RayCastCallbackWrapper
 */

b2RayCastCallbackWrapper* b2RayCastCallbackWrapper_new(b2RayCastCallback_callback_ReportFixture reportFixture)
{
    return new b2RayCastCallbackWrapper(reportFixture);
}

void b2RayCastCallbackWrapper_delete(b2RayCastCallbackWrapper* obj)
{
    VERIFY_INSTANCE;
    delete obj;
}
