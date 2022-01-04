using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Box2D.Core;

using static Config.Conditionals;

/// <summary>
/// Tracks <see cref="Box2DObject"/> instances. Used to determine
/// whether objects get disposed properly and track down memory leaks.
/// </summary>
public sealed class Box2DObjectTracker
{
#if BOX2D_OBJECT_TRACKING
    public static Box2DObjectTracker? Instance { get; } = new();
#else
    public static Box2DObjectTracker? Instance { get; } = null;
#endif

    private readonly object _referenceLock = new();
    private readonly Dictionary<IntPtr, WeakReference<Box2DObject>> _references = new();

    private int _finalizerCallCount;

    /// <summary>
    /// Gets the total number of finalizer calls. This can indicate how many objects
    /// were not properly disposed.
    /// </summary>
    public int TotalFinalizerCallCount => _finalizerCallCount;

    /// <summary>
    /// Gets the total number of <see cref="Box2DObject"/> references, including invalid references.
    /// </summary>
    public int TotalReferenceCount
    {
        get
        {
            lock (_referenceLock)
            {
                return _references.Count;
            }
        }
    }

    /// <summary>
    /// Gets a list of all valid referenced <see cref="Box2DObject"/> instances.
    /// </summary>
    public IList<Box2DObject> GetReferencedObjects()
    {
        lock (_referenceLock)
        {
            var result = new List<Box2DObject>(_references.Count);

            foreach (var value in _references.Values)
            {
                if (value.TryGetTarget(out var target))
                {
                    result.Add(target);
                }
            }

            return result;
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

    [Conditional(BOX2D_OBJECT_TRACKING)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void IncrementFinalizerCallCount()
        => Interlocked.Increment(ref Instance!._finalizerCallCount);

    private void AddRef(Box2DObject obj)
    {
        if (obj is null)
        {
            throw new ArgumentNullException(nameof(obj));
        }

        lock (_referenceLock)
        {
            if (_references.ContainsKey(obj.Native))
            {
                throw new InvalidOperationException("Attempted to add a reference to an object already being tracked.");
            }

            _references.Add(obj.Native, new(obj));
        }
    }

    private void RemoveRef(Box2DObject obj)
    {
        if (obj is null)
        {
            throw new ArgumentNullException(nameof(obj));
        }

        lock (_referenceLock)
        {
            if (!_references.ContainsKey(obj.Native))
            {
                throw new InvalidOperationException("Attempted to add a reference to an object already being tracked.");
            }

            _references.Remove(obj.Native);
        }
    }
}
