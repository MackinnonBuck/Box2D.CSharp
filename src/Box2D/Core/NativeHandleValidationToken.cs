namespace Box2D.Core;

internal class NativeHandleValidationToken
{
    public bool IsValid { get; set; }

    public object? UserData { get; }

    public NativeHandleValidationToken(object? userData)
    {
        UserData = userData;
        IsValid = true;
    }
}
