using static System.Console;

namespace WriteO;

class Program
{
    static void Main(string[] args)
    {
		WriteLine("Test");
		ClearAll();
        WriteLine("Welcome to WriteO - The successor to WriteC");
		(string userName, bool newUser) = DataFetcher.GetName();
		if (newUser) 
		{
			string path;
            WriteLine("It seems as if you were a new user. Please select a path for the server.");
			do
			{
				path = ReadLine();
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
        }
        User.InitName(userName);
		//User.InitLang(DataFetcher.GetLang());
		int mode = 0;
		while(mode != 4)
		{
		    ClearAll();
		    WriteLine("What action do you want to perform?\n1.....Messages\n2.....FileSystem\n3.....WIP\n4.....Exit\n");
#pragma warning disable
		    if(int.TryParse(ReadLine().Remove(1), out mode))
			{
				Mode.Select(mode);
			}
		}
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
}
