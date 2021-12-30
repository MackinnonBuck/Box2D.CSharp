using System.Text.Json;
using System.Text.Json.Serialization;

namespace Testbed;

internal class Settings
{
    private static readonly string _fileName = "settings.ini";

    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        IncludeFields = true,
    };

    public int testIndex;

    public int windowWidth;

    public int windowHeight;

    public float hertz;

    public int velocityIterations;

    public int positionIterations;

    public bool drawShapes;

    public bool drawJoints;

    public bool drawAABBs;

    public bool drawContactPoints;

    public bool drawContactNormals;

    public bool drawContactImpulse;

    public bool drawFrictionImpulse;

    public bool drawCOMs;

    public bool drawStats;

    public bool drawProfile;

    public bool enableWarmStarting;

    public bool enableContinuous;

    public bool enableSubStepping;

    public bool enableSleep;

    [JsonIgnore]
    public bool pause;

    [JsonIgnore]
    public bool singleStep;

    public Settings()
    {
        Reset();
    }

    public void Reset()
    {
        testIndex = 0;
        windowWidth = 1600;
        windowHeight = 900;
        hertz = 60f;
        velocityIterations = 8;
        positionIterations = 3;
        drawShapes = true;
        drawJoints = true;
        drawAABBs = false;
        drawContactPoints = false;
        drawContactNormals = false;
        drawFrictionImpulse = false;
        drawCOMs = false;
        drawStats = false;
        drawProfile = false;
        enableWarmStarting = true;
        enableContinuous = true;
        enableSubStepping = false;
        enableSleep = true;
        pause = false;
        singleStep = false;
    }

    public void Save()
    {
        try
        {
            using var stream = new FileStream(_fileName, FileMode.Create);
            JsonSerializer.Serialize(stream, this, _jsonOptions);
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"Unable to save settings: {e.Message}");
        }
    }

    public static Settings Load()
    {
        try
        {
            using var stream = new FileStream(_fileName, FileMode.Open);
            return JsonSerializer.Deserialize<Settings>(stream, _jsonOptions) ?? new();
        }
        catch (FileNotFoundException)
        {
            // No-op: The file hasn't been created yet.
        }
        catch (Exception e)
        {
            Console.WriteLine($"Unable to load settings: {e.Message}");
        }

        return new();
    }
}
