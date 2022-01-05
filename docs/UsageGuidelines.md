# Usage Guidelines

This document contains important guidelines for using Box2D.CSharp.

## Disposing Box2D Resources

Resource management patterns in Box2D.CSharp aligns closely with that of the official C++ Box2D implementation. Here are some general principles to follow when using our API:

### 1. Dispose objects that you own

If you create a Box2D object instance and its type implements `IDisposable`, make sure you either:
  * call `Dispose()` when you're done using the object, or...
  * use a `using` statement to automatically dispose the object when it goes out of scope.

For example, to destroy a `World` (including all its bodies, joints, etc.), call its `Dispose()` method.

"Definition" objects (`BodyDef`, `FixtureDef`, and `JointDef`) should be disposed so the underlying pooling mechanism can reuse disposed instances rather than always allocating new instances.

**NOTE:** `Shape` instances get copied when attached to `Fixture` instances. Remember to dispose any `Shape` instances you create, but don't feel obligated to dispose `Fixture.Shape`, since it will get implicitly destroyed when its parent `Fixture` is destroyed.

### 2. Be wary of implicit destruction

Fixtures automatically get destroyed when their parent body gets destoyed, and joints get destroyed when either of their connected bodies gets destroyed. This is known as implicit destruction. Be careful not to access implicitly-destroyed objects. If Box2D.CSharp is compiled in its `Debug` configuration, checks will be included to throw exceptions with helpful messages when objects are accessed after disposal (either implicit or explicit), so it is encouraged to use the `Debug` configuration during development. The `Release` configuration automatically removes these checks for improved performance.

You can be notified when a joint or fixture is implicitly destroyed by implementing the `IDestructionListener` interface and registering your  listener using `World.SetDestructionListener()`.

For more information, see the [Box2D docs](https://github.com/erincatto/box2d/blob/main/docs/loose_ends.md#implicit-destruction) on implicit destruction.

## Memory efficiency

Box2D.CSharp was created with both performance and memory-efficiency in mind. Real-time applications sensitive to frame-time variation (like many games and game servers) suffer when the garbage collector pauses for long periods of time. Thus, the framework allocates very little heap memory under-the-hood in order to minimize GC pressure.

That said, there are patterns that users of Box2D.CSharp can follow to further minimize heap allocation:

### 1. Reuse `"*Def"` instances when possible.

`BodyDef`, `FixtureDef`, and `JointDef` are all `class` types for performance reasons, so they consequently get heap-allocated. If you are creating bodies, fixtures, or joints in a tight loop, it can be more efficient to create the `"*Def"` instance outside the loop, mutate it within the loop, and dispose it after the loop. For example:

```csharp
// Create a BodyDef limited to the current scope.
using var bodyDef = BodyDef.Create();
bodyDef.Type = Bodytype.Dynamic,

for (var i = 0; i < 1000; i++)
{
    // Mutate bodyDef for each body.
    bodyDef.Position = new(i * 3f, 0f);
    var body = _world.CreateBody(bodyDef);
    // ...
}
```

**NOTE:** If you are creating bodies or fixtures with that don't require much configuration, you can also use the shortcut overloads of `World.CreateBody()` and `Body.CreateFixture()` that don't require a `"*Def"` parameter.

### 2. Utilize `Span`-based APIs

Box2D.CSharp exposes `Span`-based APIs in a handful of places that can be used in combination with C#'s high-performance features like `stackalloc` to pass collections of data without allocating additional heap memory. For example, the vertices of a `PolygonShape` can be defined as follows:

```csharp
// Create the PolygonShape.
using var shape = PolygonShape.Create();

// Allocate the shape's vertices on the stack.
Span<Vector2> vertices = stackalloc Vector2[]
{
    new(-1.5f, -0.5f),
    new(1.5f, -0.5f),
    new(1.5f, 0f),
    new(0f, 0.9f),
    new(-1.15f, 0.9f),
    new(-1.5f, 0.2f),
};

// Update the shape's vertices.
// These will be copied into an unmanaged buffer, so
// it's fine that 'vertices' is going out of scope.
shape.Set(vertices);
```
