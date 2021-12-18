using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Box2D.Core;

public sealed class Box2DObjectTracker
{
    private readonly HashSet<Box2DObject> _box2DObjects = new();

    public IReadOnlyCollection<Box2DObject> Objects => _box2DObjects;

    private Box2DObjectTracker()
    {
    }

#if BOX2D_OBJECT_TRACKING
    public static Box2DObjectTracker? Instance { get; } = new();

    internal static void Add(Box2DObject box2DObject)
        => Instance!.AddRef(box2DObject);

    internal static void Remove(Box2DObject box2DObject)
        => Instance!.RemoveRef(box2DObject);

    private void AddRef(Box2DObject box2DObject)
    {
        if (box2DObject is null)
        {
            throw new ArgumentNullException(nameof(box2DObject));
        }

        if (!_box2DObjects.Add(box2DObject))
        {
            throw new InvalidOperationException("Attempted to add a reference to an object already being tracked.");
        }
    }

    private void RemoveRef(Box2DObject box2DObject)
    {
        if (box2DObject is null)
        {
            throw new ArgumentNullException(nameof(box2DObject));
        }

        if (!_box2DObjects.Remove(box2DObject))
        {
            throw new InvalidOperationException("Attempted to remove a reference to an object not being tracked.");
        }
    }

#else
    public static Box2DObjectTracker? Instance { get; } = null;

    [Conditional("BOX2D_OBJECT_TRACKING")]
    internal static void Add(Box2DObject box2DObject)
    {
    }

    [Conditional("BOX2D_OBJECT_TRACKING")]
    internal static void Remove(Box2DObject box2DObject)
    {
    }
#endif
}
