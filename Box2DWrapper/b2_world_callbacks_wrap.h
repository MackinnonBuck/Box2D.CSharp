#pragma once

#include "api.h"

#include <box2d/b2_world.h>

/*
 * b2ContactListenerWrapper
 */
typedef void (*b2ContactListener_callback_BeginContact)(b2Contact* contact);
typedef void (*b2ContactListener_callback_EndContact)(b2Contact* contact);
typedef void (*b2ContactListener_callback_PreSolve)(b2Contact* contact, const b2Manifold* oldManifold);
typedef void (*b2ContactListener_callback_PostSolve)(b2Contact* contact, const b2ContactImpulse* impulse);

class b2ContactListenerWrapper : public b2ContactListener
{
public:
    b2ContactListenerWrapper(
        b2ContactListener_callback_BeginContact beginContact,
        b2ContactListener_callback_EndContact endContact,
        b2ContactListener_callback_PreSolve preSolve,
        b2ContactListener_callback_PostSolve postSolve);

    void BeginContact(b2Contact* contact) override;
    void EndContact(b2Contact* contact) override;
    void PreSolve(b2Contact* contact, const b2Manifold* oldManifold) override;
    void PostSolve(b2Contact* contact, const b2ContactImpulse* impulse) override;

private:
    b2ContactListener_callback_BeginContact m_beginContact;
    b2ContactListener_callback_EndContact m_endContact;
    b2ContactListener_callback_PreSolve m_preSolve;
    b2ContactListener_callback_PostSolve m_postSolve;
};

/*
 * b2QueryCallbackWrapper
 */
typedef bool (*b2QueryCallback_callback_ReportFixture)(b2Fixture* fixture);

class b2QueryCallbackWrapper : public b2QueryCallback
{
public:
    b2QueryCallbackWrapper(
        b2QueryCallback_callback_ReportFixture reportFixture);

    bool ReportFixture(b2Fixture* fixture) override;

private:
    b2QueryCallback_callback_ReportFixture m_reportFixture;
};

/*
 * b2RayCastCallbackWrapper
 */
typedef float (*b2RayCastCallback_callback_ReportFixture)(b2Fixture* fixture, const b2Vec2* point, const b2Vec2* normal, float fraction);

class b2RayCastCallbackWrapper : public b2RayCastCallback
{
public:
    b2RayCastCallbackWrapper(
        b2RayCastCallback_callback_ReportFixture reportFixture);

    float ReportFixture(b2Fixture* fixture, const b2Vec2& point, const b2Vec2& normal, float fraction) override;

private:
    b2RayCastCallback_callback_ReportFixture m_reportFixture;
};

extern "C"
{
    /*
     * b2ContactImpulse
     */
    BOX2D_API void b2ContactImpulse_get_impulses(b2ContactImpulse* obj, float** normalImpulses, float** tangentImpulses, int32* count);

    /*
     * b2ContactListenerWrapper
     */
    BOX2D_API b2ContactListenerWrapper* b2ContactListenerWrapper_new(
        b2ContactListener_callback_BeginContact beginContact,
        b2ContactListener_callback_EndContact endContact,
        b2ContactListener_callback_PreSolve preSolve,
        b2ContactListener_callback_PostSolve postSolve);
    BOX2D_API void b2ContactListenerWrapper_delete(b2ContactListenerWrapper* obj);

    /*
     * b2QueryCallbackWrapper
     */
    BOX2D_API b2QueryCallbackWrapper* b2QueryCallbackWrapper_new(b2QueryCallback_callback_ReportFixture reportFixture);
    BOX2D_API void b2QueryCallbackWrapper_delete(b2QueryCallbackWrapper* obj);

    /*
     * b2RayCastCallbackWrapper
     */
    BOX2D_API b2RayCastCallbackWrapper* b2RayCastCallbackWrapper_new(b2RayCastCallback_callback_ReportFixture reportFixture);
    BOX2D_API void b2RayCastCallbackWrapper_delete(b2RayCastCallbackWrapper* obj);
}
