using Spectre.Console;
using static Spectre.Console.AnsiConsole;
//using static System.Console;

namespace WriteO;

class Program
{
    static void Main(string[] args)
    {
        String.WriteCenteredMarkupText(Translations.Get("BuildSuccess"), System.Console.WindowHeight / 2);
		ClearAll();
        String.WriteCenteredText(Translations.Get("Welcome"), System.Console.WindowHeight / 2);
        #region Fetching
        (string userName, bool newUser) = DataFetcher.GetName();
		if (newUser) 
		{
			string path;
            System.Console.WriteLine(Translations.Get("NewUserPath"));
			do
			{
				path = System.Console.ReadLine();
			} while(!Directory.Exists(path));
			if (Environment.OSVersion.Platform == PlatformID.Unix)
			{
				Files.Log = path + (path[^1] == '/' ? "log.txt" : "/log.txt");
				Files.FS = path + (path[^1] == '/' ? "files/" : "/files/");
			}
			else if (Environment.OSVersion.Platform == PlatformID.Win32NT)
			{
				Files.Log = path + (path[^1] == '\\' ? "log.txt" : "\\log.txt");
				Files.FS = path + (path[^1] == '\\' ? "files\\" : "\\files\\");
			}
			using (StreamWriter streamWriter = new StreamWriter(Files.FilePath))
			{
				streamWriter.WriteLine(Files.Log + '\n' +  Files.FS);
			}
        }
		else
		{
			using (StreamReader streamReader = new StreamReader(Files.FilePath))
			{
				Files.Log = streamReader.ReadLine();
				Files.FS = streamReader.ReadLine();
			}
		}
		User.InitName(userName);
        User.InitLang(DataFetcher.GetLang());
        #endregion Fetching
        int mode = 0;
		while(mode != 4)
		{ // TODO: Update for mouse support
		    ClearAll();
                    var choice = Prompt(
                                    new SelectionPrompt<string>()
                                    .Title(Translations.Get("ActionPrompt"))
                                    .WrapAround()
                                    //.HighlightStyle()
                                    .AddChoices(Translations.Get("Messages"), Translations.Get("FileServer"), Translations.Get("Settings"), Translations.Get("Exit"))); // Same as return values
                    if (choice == Translations.Get("Messages"))
                    {
                        mode = 1;
                    }
                    else if (choice == Translations.Get("FileServer"))
                    {
                        mode = 2;
                    }
                    else if (choice == Translations.Get("Settings"))
                    {
                        mode = 3;
                    }
                    else if (choice == Translations.Get("Exit"))
                    {
                        mode = 4;
                    }
        	Mode.Select(mode);
			
		}
                ClearAll();
                Clear();
	}
	public static void ClearAll()
	{
        // Source - https://stackoverflow.com/a
        // Posted by Alex
        // Retrieved 2026-01-16, License - CC BY-SA 4.0

        Clear();
        WriteLine("\x1b[3J");
    }
}
public static class Files
{
	public static string Log { get; set; }
	public static string Usr { get; } = "usr.json";
	public static string FS { get; set; }
	public static string FilePath { get; set; } = "FilePath.txt";
}
