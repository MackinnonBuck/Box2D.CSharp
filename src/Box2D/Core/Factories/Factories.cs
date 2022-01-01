using Box2D.Dynamics;
using System.Runtime.CompilerServices;

namespace Box2D.Core.Factories;

public class BodyDefFactory : IBox2DPoolableObjectFactory<BodyDef>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    BodyDef IBox2DPoolableObjectFactory<BodyDef>.Create()
        => new();
}

public class FixtureDefFactory : IBox2DPoolableObjectFactory<FixtureDef>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    FixtureDef IBox2DPoolableObjectFactory<FixtureDef>.Create()
        => new();
}
