using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace WriteO;

public enum Languages
{
    en,
    de,
    fr,
    es,
    ja,
    ru,
    zh,
}

public static class Lang
{
    private static readonly Dictionary<Languages, Dictionary<Keys, string>> _translations = new();

    public static string GetText(Keys key)
    {
        if (_translations.TryGetValue(User.Lang, out var langDict) &&
            langDict.TryGetValue(key, out var value))
        {
            return value;
        }

        // Fallback to English
        if (_translations.TryGetValue(Languages.en, out var enDict) &&
            enDict.TryGetValue(key, out value))
        {
            return value;
        }

        return $"Missing Translation: {key}";
    }

    public static void Init()
    {
        string languageFolder = Environment.GetEnvironmentVariable("WriteOPath")!;
        if (Environment.OSVersion.Platform == PlatformID.Unix)
        {
            languageFolder += "/Languages";
        }
        else if (Environment.OSVersion.Platform == PlatformID.Win32NT)
        {
            languageFolder += "\\Languages";
        }
        _translations.Clear();

        if (!Directory.Exists(languageFolder))
            throw new DirectoryNotFoundException(languageFolder);

        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();

        foreach (var file in Directory.GetFiles(languageFolder, "*.yaml"))
        {
            var langName = Path.GetFileNameWithoutExtension(file);

            if (!Enum.TryParse<Languages>(langName, true, out var language))
                continue;

            var yaml = File.ReadAllText(file);

            var rawData = deserializer.Deserialize<Dictionary<string, string>>(yaml);

            var parsed = new Dictionary<Keys, string>();

            foreach (var pair in rawData)
            {
                if (Enum.TryParse<Keys>(pair.Key, out var key))
                {
                    parsed[key] = pair.Value;
                }
            }

            _translations[language] = parsed;
        }
    }
}

public enum Keys
{
    serverSelectText,
    alreadyBlacklistedText,
    keyToContinueText,
    commandNotFoundText,
    blacklistTitle,
    menuMessages,
    menuFiles,
    menuSettings,
    menuExit,
    gotBannedText,
    invalidEncoding,
    nameEnterText,
    settingsNameText,
    settiingsServerLocText,
    settingsKeyText,
    settingsLanguageText,
    keyEnterText,
    languageEnterText,
    fileUploadText,
    fileDownLoadText,

}
