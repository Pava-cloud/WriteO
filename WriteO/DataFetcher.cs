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
            BinaryReader logGetter = new(File.Open(Files.Log, System.IO.FileMode.Open));
            try
            {
                log = String.DecodeText(logGetter.ReadString(), User.Key);

            }
            catch (EndOfStreamException)
            {
                log = "";
            }

            logGetter.Close();
        }
        else File.Create(Files.Log).Dispose();
        return log;
    }
    public static (string, bool) GetName()
    {
        string name = "";
        bool newUser = false;
        if (File.Exists(Files.NameFile))
        {
            using (StreamReader nameGetter = new StreamReader(Files.NameFile))
            {
                name = nameGetter.ReadLine()!;
                Console.WriteLine(name);
            }
        }
        else
        {
            File.Create(Files.NameFile).Dispose();
            StreamWriter nameWriter = new StreamWriter(Files.NameFile);
            do
            {
                Console.WriteLine(Lang.GetText(Keys.nameEnterText));
                name = Console.ReadLine()!;
            } while (string.IsNullOrWhiteSpace(name));
            nameWriter.Write(name);
            nameWriter.Close();
            newUser = true;
        }
        return (name, newUser);
    }
}
