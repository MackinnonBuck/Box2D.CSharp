using System.Reflection;
using Testbed.Drawing;

namespace Testbed;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
internal class TestEntryAttribute : Attribute
{
    public string Category { get; }

    public string Name { get; }

    public TestEntryAttribute(string category, string name)
    {
        Category = category;
        Name = name;
    }
}

internal class TestEntry : IComparable<TestEntry>
{
    private readonly ConstructorInfo _testConstructor;

    public string Category { get; }

    public string Name { get; }

    public TestEntry(Type testType)
    {
        if (!typeof(Test).IsAssignableFrom(testType))
        {
            throw new ArgumentException($"The type '{testType.Name}' is not assignable to type '{typeof(Test).Name}'.");
        }
        
        var ctor = testType.GetConstructor(Type.EmptyTypes);

        if (ctor is null)
        {
            throw new ArgumentException($"The type '{testType.Name}' does not have a default constructor.", nameof(testType));
        }

        var attributes = testType.GetCustomAttributes<TestEntryAttribute>();

        if (attributes is null)
        {
            throw new ArgumentException($"The type '{testType.Name}' is missing a '{nameof(TestEntryAttribute)}' attribute.");
        }

        var attribute = attributes.Single();

        _testConstructor = ctor;
        Category = attribute.Category;
        Name = attribute.Name;
    }

    public Test CreateTest(DebugDraw debugDraw, Settings settings, Camera camera)
    {
        var test = (Test)_testConstructor.Invoke(null);
        test.Initialize(debugDraw, settings, camera);

        return test;
    }

    public int CompareTo(TestEntry? other)
    {
        if (other is null)
        {
            return 1;
        }

        var result = string.Compare(Category, other.Category);

        if (result == 0)
        {
            result = string.Compare(Name, other.Name);
        }

        return result;
    }
}
