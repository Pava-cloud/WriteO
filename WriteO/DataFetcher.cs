namespace WriteO;

public static class DataFetcher
{
    private static string[] allowedLangs = {
        "EN", "DE"
    };

    public static string Log()
    {
        string log = "";
        if (File.Exists(Files.Log))
        {
            StreamReader logGetter = new StreamReader(Files.Log);
            log = String.DecodeText(logGetter.ReadToEnd(), User.Key);
            logGetter.Close();
        }
        else File.Create(Files.Log).Dispose();
        return log;
    }

    public static (string?, bool) GetName()
    {
        string? name = "";
        bool newUser = false;
        if (File.Exists("name.txt"))
        {
            using (StreamReader nameGetter = new StreamReader("name.txt"))
            {
                name = nameGetter.ReadLine();
                Console.WriteLine(name);
            }
        }
        else
        {
            File.Create("name.txt").Dispose();
            using (StreamWriter nameWriter = new StreamWriter("name.txt"))
            {
                do
                {
                    Console.WriteLine(Translations.Get("NamePrompt"));
                    name = Console.ReadLine();
                } while (string.IsNullOrEmpty(name));
                nameWriter.Write(name);
            }
            newUser = true;
        }
        return (name, newUser);
    }

    public static string GetLang()
    {
        string langPath = "lang.txt";
        string lang = "";
        if (File.Exists(langPath))
        {
            using (StreamReader langGetter = new StreamReader(langPath))
            {
                lang = langGetter.ReadLine();
            }
        }

        while (string.IsNullOrEmpty(lang) || !LangIsAllowed(lang.ToUpper()))
        {
            if (File.Exists(langPath)) File.Delete(langPath);
            File.Create(langPath).Dispose();
            using (StreamWriter langWriter = new StreamWriter(langPath))
            {
                Console.Write($"Please enter your language (valid languages: {ListAllLang()}): ");
                lang = Console.ReadLine();
                if (lang != null && LangIsAllowed(lang.ToUpper()))
                {
                    langWriter.WriteLine(lang.ToUpper());
                }
            }
        }
        return lang.ToUpper();
    }

    public static bool LangIsAllowed(string input)
    {
        bool retval = false;
        foreach (string allowedLang in allowedLangs)
        {
            if (input == allowedLang) retval = true;
        }
        return retval;
    }

    public static string ListAllLang()
    {
        string retval = "";
        foreach (string allowedLang in allowedLangs)
        {
            retval += $"{allowedLang}, ";
        }
        if (retval.Length >= 2)
            retval = retval.Remove(retval.Length - 2);
        return retval;
    }
}
