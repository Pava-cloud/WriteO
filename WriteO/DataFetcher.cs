namespace WriteO;

public static class DataFetcher
{
    private static string[] allowedLangs = {
        "EN"
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
        if(File.Exists("name.txt"))
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
            StreamWriter nameWriter = new StreamWriter("name.txt");
            do
            {
                Console.WriteLine("Please enter your name:");
                name = Console.ReadLine();
            } while (string.IsNullOrEmpty(name));
            nameWriter.Write(name);
            nameWriter.Close();
            newUser = true;
        }
        return (name, newUser);
    }
    public static string GetLang()
    {
        throw new NotImplementedException("Multi-Language Support not yet implemented.\nIt will get implemented as soon as possible.");
        string langPath = "lang.txt";
        string lang = "NotEmpty";
        do
        {
            if (File.Exists(langPath))
            {
                using (StreamReader langGetter = new StreamReader(langPath))
                {
                    lang = langGetter.ReadLine();
                    //lang = Parse(langGetter.ReadToEnd(), );
                }
            }
            else
            {
                File.Create(langPath).Dispose();
                using (StreamWriter langWriter = new StreamWriter(langPath))
                {
                    Console.Write($"Please enter your language (valid languages: {ListAllLang()})");
                    langWriter.WriteLine(lang);
                }
            }
        } while (string.IsNullOrEmpty(lang));
        return lang;
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
        retval = retval.Remove(retval.Length - 2);
        return retval;
    }
}
