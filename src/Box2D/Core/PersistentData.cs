namespace Box2D.Core;

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
