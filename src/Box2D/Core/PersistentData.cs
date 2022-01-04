namespace Box2D.Core;

// This type is used when access checking is enabled.
// It provides a shared flag so struct-based instances
// can check if the native resource has been deleted,
// while also storing the user data object.
internal class PersistentData
{
    public bool IsValid { get; set; }

    public object? UserData { get; }

    public PersistentData(object? userData)
    {
        UserData = userData;
        IsValid = true;
    }
}
