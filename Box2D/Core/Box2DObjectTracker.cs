using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Box2D;

using static Conditionals;

public sealed class Box2DObjectTracker
{
#if BOX2D_OBJECT_TRACKING
    public static Box2DObjectTracker? Instance { get; } = new();
#else
    public static Box2DObjectTracker? Instance { get; } = null;
#endif

    private readonly object _box2DObjectsLock = new();
    private readonly HashSet<Box2DObject> _box2DObjects = new();

    public int ObjectCount
    {
        get
        {
            lock (_box2DObjectsLock)
            {
                return _box2DObjects.Count;
            }
        }
    }

    public IList<Box2DObject> GetObjects()
    {
        lock (_box2DObjectsLock)
        {
            return _box2DObjects.ToList();
        }
    }

    private Box2DObjectTracker()
    {
    }

    [Conditional(BOX2D_OBJECT_TRACKING)]
    internal static void Add(Box2DObject box2DObject)
        => Instance!.AddRef(box2DObject);

    [Conditional(BOX2D_OBJECT_TRACKING)]
    internal static void Remove(Box2DObject box2DObject)
        => Instance!.RemoveRef(box2DObject);

    private void AddRef(Box2DObject box2DObject)
    {
        if (box2DObject is null)
        {
            throw new ArgumentNullException(nameof(box2DObject));
        }

        lock (_box2DObjectsLock)
        {
            if (!_box2DObjects.Add(box2DObject))
            {
                throw new InvalidOperationException("Attempted to add a reference to an object already being tracked.");
            }
        }
    }

    private void RemoveRef(Box2DObject box2DObject)
    {
        if (box2DObject is null)
        {
            throw new ArgumentNullException(nameof(box2DObject));
        }

        lock (_box2DObjectsLock)
        {
            if (!_box2DObjects.Remove(box2DObject))
            {
                throw new InvalidOperationException("Attempted to remove a reference to an object not being tracked.");
            }
        }
    }
}
