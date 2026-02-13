namespace WriteO;

public static class User
{
    public static bool IsAdmin { get; set; }
    
    private static string name;

    public static string Name
    {
        get { return name; }
        private set
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentException("Name was null, Name cannot be empty");
            else if (value.Length > 4)
                name = value;
            else throw new ArgumentException($"Name was shorter than expected\nExpected at least 5 characters, got: {value.Length}");
        }
    }
    private static string lang;

    public static string Lang
    {
        get { return lang; }
        private set
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentException("Lang was null, Lang cannot be empty");
            else if (DataFetcher.LangIsAllowed(value)) lang = value;
            else throw new InvalidDataException($"value {value} is not allowed as Lang\nallowed values for Lang: {DataFetcher.ListAllLang()}");
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

#pragma warning disable CS0114
    public static string ToString()
    {
        return Name;
    }
    public static void changeLang(string newLang)
    {
        Lang = newLang;
    }
    public static void InitName(string name)
    {
        Name = name;
    }
    public static void InitLang(string lang)
    {
        if (Lang == default || string.IsNullOrWhiteSpace(Lang)) Lang = lang;
    }
}
