namespace WriteO;
class Program
{

    static void Main()
    {
		Console.Title = "WriteO - Starting";
        String.WriteCenteredMarkupText("✓ Build completed successfully", "[green]", Console.WindowHeight / 2);
		ClearAll();
        String.WriteCenteredText("Welcome to WriteO - The successor to WriteC", Console.WindowHeight / 4);
		String.WriteCenteredText("Initializing...", Console.WindowHeight / 4 + 1);
        #region Fetching
        (string userName, bool newUser) = DataFetcher.GetName()!;
		if (newUser) 
		{
			string path;
            Console.WriteLine("Please select a path for the server.");
			do
			{
				do { } while(string.IsNullOrEmpty(path = Console.ReadLine()!));
			} while(!Directory.Exists(path));
			if (Environment.OSVersion.Platform == PlatformID.Unix)
			{
				Files.Log = path + (path[^1] == '/' ? "log.ocht" : "/log.ocht");
				Files.FS = path + (path[^1] == '/' ? "files/" : "/files/");
			}
			else if (Environment.OSVersion.Platform == PlatformID.Win32NT)
			{
				Files.Log = path + (path[^1] == '\\' ? "log.ocht" : "\\log.ocht");
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
				Files.Log = streamReader.ReadLine()!;
				Files.FS = streamReader.ReadLine()!;
			}
		}
		User.InitName(userName);
        //User.InitLang(DataFetcher.GetLang());
        #endregion Fetching

		Console.Title = "WriteO";
		Mode.Select();
		ResetAll();
	}
	public static void ResetAll()
	{
		Console.CursorVisible = false;
		Console.ResetColor();
		ClearAll();
		Console.Clear();
		Console.CursorVisible = true;
	}
	public static void ClearAll()
	{
        // Source - https://stackoverflow.com/a
        // Posted by Alex
        // Retrieved 2026-01-16, License - CC BY-SA 4.0

        Console.Clear();
        Console.WriteLine("\x1b[3J");
    }
}
public static class Files
{
	public static string Log { get; set; } = "";
	public static string Usr { get; } = "usr.json";
	public static string FS { get; set; } = "";
	public static string FilePath { get; set; } = "FilePath.txt";
}
public class newRandom
{
	private int s;
	public newRandom()
	{
		s = (int)DateTime.Now.Ticks % int.MaxValue;
	}
	public newRandom(int seed)
    {
        s = seed;
    }
	public int Next(bool includeNegatives = false)
	{
		if (!includeNegatives)
			s = (s * 47231 + 8209) % 236150;
		else
			s = (s * -47231 + 8209) % 236150;
		return s;
	}
	public int Next(int mod, bool includeNegatives = false)
	{
		if (!includeNegatives)
			s = (s * 47231 + 8209) % mod;
		else
			s = (s * -47231 + 8209) % mod;
		return s;
	}
}
