namespace WriteO;

public static class User
{
    public static bool IsAdmin { get; set; }
    private static string name = "";

    public static string Name
    {
        get { return name; }
        set
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentException("Name was null, Name cannot be empty");
            else if (value.Length > 4)
                name = value;
            else throw new ArgumentException($"Name was shorter than expected\nExpected at least 5 characters, got: {value.Length}");
        }
    }
    private static Languages lang = Languages.en;

    public static Languages Lang
    {
        get { return lang; }
        private set
        {
            lang = value;
        }
    }
    private static int key = 10;

    public static int Key
    {
        get { return key; }
        set
        {
            if (value < 3 || value > 'z')
                throw new ArgumentOutOfRangeException($"Key was out of valid range.\nExpected a value between 3 and {(int)'z'}; Got: {value}");
            else key = value;
        }
    }

    public static void setLang(Languages newLang)
    {
        Lang = newLang;
    }
    public static void InitName(string? name)
    {
        Name = name!;
    }
}
