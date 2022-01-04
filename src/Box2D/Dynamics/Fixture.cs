using Box2D.Collision;
using Box2D.Core;
using System;
using System.Numerics;

namespace Box2D.Dynamics;

using static Interop.NativeMethods;

/// <summary>
/// Used to attach a shape to a body for collision detection. A fixture
/// inherits its transform from its parent. Fixtures hold additional non-gemoetric data
/// such as friction, collision filters, etc.
/// Fixtures are created via <see cref="Body.CreateFixture(in FixtureDef)"/>.
/// </summary>
/// <remarks>
/// Warning: You cannot reuse fixtures.
/// </remarks>
public readonly struct Fixture : IEquatable<Fixture>
{
    internal readonly struct Reviver : IPersistentDataReviver
    {
        public string RevivedObjectName => nameof(Fixture);

        public IntPtr GetPersistentDataPointer(IntPtr native)
            => b2Fixture_GetUserData(native);
    }

    internal readonly struct Destroyer : INativeResourceDestroyer
    {
        private readonly IntPtr _bodyNative;

        public Destroyer(IntPtr bodyNative)
        {
            _bodyNative = bodyNative;
        }

        public void Destroy(IntPtr native)
        {
            b2Body_DestroyFixture(_bodyNative, native);
        }
    }

    private readonly NativeHandle<Reviver> _nativeHandle;

    internal IntPtr Native => _nativeHandle.Ptr;

    /// <summary>
    /// Gets whether this fixture instance is null.
    /// </summary>
    /// <remarks>
    /// This will not necessarily return <see langword="true"/> if the fixture has been implicitly destroyed.
    /// You can manually nullify <see cref="Fixture"/> instances by assigning them to <see langword="default"/>.
    /// </remarks>
    public bool IsNull => _nativeHandle.IsNull;

    /// <summary>
    /// Gets the user data of the fixture. Use this to store your application-specific data.
    /// </summary>
    public object? UserData => _nativeHandle.GetUserData();

    /// <summary>
    /// Gets the type of the child shape. You can use this to down cast to the concrete shape.
    /// </summary>
    public ShapeType Type => b2Fixture_GetType(Native);

    /// <summary>
    /// Gets the parent body of this fixture. This is <c>null</c> if the fixture is not attached.
    /// </summary>
    public Body Body => new(b2Fixture_GetBody(Native));

    /// <summary>
    /// Gets or sets the child shape. You can modify the child shape, however you should not
    /// change the number of vertices because this will crash some collision caching mechanisms.
    /// Manipulating the shape may lead to non-physical behavior.
    /// </summary>
    public Shape Shape => Shape.GetFromCacheOrCreate(b2Fixture_GetShape(Native), Type)!;

    /// <summary>
    /// Gets or sets if the fixture is a sensor.
    /// </summary>
    public bool IsSensor
    {
        get => b2Fixture_IsSensor(Native);
        set => b2Fixture_SetSensor(Native, value);
    }

    /// <summary>
    /// Gets the next fixture in the parent body's fixture list.
    /// </summary>
    public Fixture Next => new(b2Fixture_GetNext(Native));

    internal Fixture(Body body, in FixtureDef def)
    {
        if (def.Shape is null)
        {
            throw new InvalidOperationException($"Cannot create a {nameof(Fixture)} from a {nameof(FixtureDef)} without a {nameof(Shape)}.");
        }

        var persistentDataHandle = PersistentDataHandle.Create(def.UserData);
        var native = b2Body_CreateFixture(body.Native, def.Native, persistentDataHandle.Ptr);
        _nativeHandle = new(native, persistentDataHandle);
    }

    internal Fixture(Body body, Shape shape, float density)
    {
        if (shape is null)
        {
            throw new InvalidOperationException($"Cannot create a {nameof(Fixture)} without a {nameof(Shape)}.");
        }

        var managedHandle = PersistentDataHandle.Create(null);
        var native = b2Body_CreateFixture2(body.Native, shape.Native, density, managedHandle.Ptr);
        _nativeHandle = new(native, managedHandle);
    }

    internal Fixture(IntPtr native)
    {
        _nativeHandle = new(native);
    }

    internal void Invalidate()
    {
        // This will return null if the shape was never cached.
        var shape = Shape.GetFromCache(b2Fixture_GetShape(Native));

        // The shape is not user-owned, so disposing it is safe.
        shape?.Dispose();

        _nativeHandle.Invalidate();
    }

    internal void Destroy(IntPtr bodyNative)
        => _nativeHandle.Destroy(new Destroyer(bodyNative));

    /// <summary>
    /// Test a point for containment in this fixture.
    /// </summary>
    /// <param name="p">A point in world coordinates.</param>
    public bool TestPoint(Vector2 p)
        => b2Fixture_TestPoint(Native, ref p);

    public static bool operator ==(Fixture a, Fixture b)
        => a.Equals(b);

    public static bool operator !=(Fixture a, Fixture b)
        => !a.Equals(b);

    /// <inheritdoc/>
    public bool Equals(Fixture other)
        => _nativeHandle.Equals(other._nativeHandle);

    /// <inheritdoc/>
    public override bool Equals(object obj)
        => obj is Fixture fixture && Equals(fixture);

    /// <inheritdoc/>
    public override int GetHashCode()
        => _nativeHandle.GetHashCode();
}
