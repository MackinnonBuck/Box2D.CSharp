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
public sealed class Fixture : Box2DSubObject, IBox2DList<Fixture>
{
    internal static FixtureFromIntPtr FromIntPtr { get; } = new();

    internal class FixtureFromIntPtr : IGetFromIntPtr<Fixture>
    {
        public IntPtr GetManagedHandle(IntPtr obj)
            => b2Fixture_GetUserData(obj);
    }

    bool IBox2DList<Fixture>.IsNull => false;

    private Shape? _shape;

    /// <summary>
    /// Gets the type of the child shape. You can use this to down cast to the concrete shape.
    /// </summary>
    public ShapeType Type { get; }

    /// <summary>
    /// Gets the parent body of this fixture. This is <c>null</c> if the fixture is not attached.
    /// </summary>
    public Body Body { get; }

    /// <summary>
    /// Gets or sets the user data of the fixture. Use this to store your application-specific data.
    /// </summary>
    public object? UserData { get; set; }

    /// <summary>
    /// Gets or sets the child shape. You can modify the child shape, however you should not
    /// change the number of vertices because this will crash some collision caching mechanisms.
    /// Manipulating the shape may lead to non-physical behavior.
    /// </summary>
    public Shape Shape
    {
        get
        {
            // We can't simply cache the shape instance provided by the FixtureDef
            // because each fixture allocates its own copy of the specified shape.

            if (_shape is null || !_shape.IsValid)
            {
                var shapeNative = b2Fixture_GetShape(Native);
                _shape = Shape.FromIntPtr.Create(shapeNative, Type)!;
            }

            return _shape;
        }
    }

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
    public Fixture? Next => FromIntPtr.Get(b2Fixture_GetNext(Native));

    internal Fixture(Body body, in FixtureDef def)
    {
        if (def.Shape is null)
        {
            throw new InvalidOperationException($"Cannot create a {nameof(Fixture)} from a {nameof(FixtureDef)} without a {nameof(Shape)}.");
        }

        Type = def.Shape.Type;
        Body = body;
        UserData = def.UserData;

        var native = b2Body_CreateFixture(body.Native, def.Native, Handle);
        Initialize(native);
    }

    internal Fixture(Body body, Shape shape, float density)
    {
        Type = shape.Type;
        Body = body;

        var native = b2Body_CreateFixture2(body.Native, shape.Native, density, Handle);
        Initialize(native);
    }

    /// <summary>
    /// Test a point for containment in this fixture.
    /// </summary>
    /// <param name="p">A point in world coordinates.</param>
    public bool TestPoint(Vector2 p)
        => b2Fixture_TestPoint(Native, ref p);

    internal override void Invalidate()
    {
        // This shape is not user-owned, so disposing it is safe.
        _shape?.Dispose();

        base.Invalidate();
    }
}
