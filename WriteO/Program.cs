using Spectre.Console;
using static Spectre.Console.AnsiConsole;
//using static System.Console;

namespace WriteO;

class Program
{
    static void Main(string[] args)
    {
        String.WriteCenteredMarkupText("[green]✓ Build completed successfully[/]", System.Console.WindowHeight / 2);
		ClearAll();
        String.WriteCenteredText("Welcome to WriteO - The successor to WriteC\nInitializing...", System.Console.WindowHeight / 2);
        #region Fetching
        (string userName, bool newUser) = DataFetcher.GetName();
		if (newUser) 
		{
			string path;
            System.Console.WriteLine("It seems as if you were a new user. Please select a path for the server.");
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
        //User.InitLang(DataFetcher.GetLang());
        #endregion Fetching
        int mode = 0;
		while(mode != 4)
		{ // TODO: Update for mouse support
		    ClearAll();
                    var choice = Prompt(
                                    new SelectionPrompt<string>()
                                    .Title("Which Action do you want to perform?")
                                    .WrapAround()
                                    //.HighlightStyle()
                                    .AddChoices("Messages", "File Server", "Settings", "Exit")); // Same as return values
                    switch (choice) 
                    {
                            case "Messages":
                                    mode = 1;
                                    break;
                            case "File Server":
                                    mode = 2;
                                    break;
                            case "Settings":
                                    mode = 3;
                                    break;
                            case "Exit":
                                    mode = 4;
                                    break;
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
