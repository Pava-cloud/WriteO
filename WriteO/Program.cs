using Spectre.Console;
namespace WriteO;

class Program
{
    /// <summary>
    /// Used for displaying extra details to the user
    /// </summary>
    public static bool verbose = false;

    static void Main(string[] args)
    {
        bool message = HandleArgs(args);
        Console.Title = verbose ? "WriteO - Starting. (Building)" : "WriteO - Starting.";
        StringExtras.WriteCenteredMarkupText("✓ Build completed successfully", "[green]", Console.WindowHeight / 2);
        ClearAll();
        Console.Title = verbose ? "WriteO - Starting.. (Initializing)" : "WriteO - Starting..";
        StringExtras.WriteCenteredText("Welcome to WriteO - The successor to WriteC", Console.WindowHeight / 4);
        StringExtras.WriteCenteredText("Initializing...", Console.WindowHeight / 4 + 1);
        if (Files.FilePath == string.Empty) FilePathInit();
        Console.Title = verbose ? "WriteO - Starting... (Fetching Data)" : "WriteO - Starting...";
        Fetch();
        Console.Title = "WriteO";
        if (message) Mode.Message();
        else Mode.Select();
        ResetAll();
    }
    private static bool HandleArgs(string[] args)
    {
        if (args.Length != 0)
        {
            for (int i = 0; i < args.Length; i++)
            {
                string arg = args[i];
                if (arg == "-m" || arg == "--message")
                    return true;
                if (arg == "-v" || arg == "--verbose")
                    verbose = true;
                if (arg == "-h" || arg == "--help")
                {
                    WriteHelp();
                }
                if (arg == "-c" || arg == "--config")
                {
                    if (args[i + 1] == "-h" || args[i + 1] == "--help")
                    {
                        Console.WriteLine("\nSyntax: -c <PATH>");
                        Environment.Exit(0);
                    }
                    else
                        Files.FilePath = args[i + 1];
                }
            }
        }
        return false;
    }
    /// <summary>
    /// Message returned when starting with -h
    /// </summary>
    private static void WriteHelp()
    {
        FilePathInit();
        AnsiConsole.MarkupLine(@"[LightGoldenrod1]USAGE:[/]");
        Console.WriteLine("\tWriteO [OPTIONS]");
        AnsiConsole.MarkupLine("[LightGoldenrod1]OPTIONS:[/]");
        Console.WriteLine("\t-v, --verbose\t\tShows extra information about startup");
        Console.WriteLine($"\t-c, --config\t\tUses the given Config path instead of the default one ({Path.GetDirectoryName(Files.FilePath)})");
        Console.WriteLine("\t-m, --message\t\tStarts without the main page in a command-only mode");
        Environment.Exit(0);
    }

    private static void FilePathInit()
    {
        switch (Environment.OSVersion.Platform)
        {
            case PlatformID.Unix:
                Files.FilePath = Environment.GetEnvironmentVariable("HOME") + "/.config/WriteO/config.txt";
                break;
            case PlatformID.Win32NT:
                Files.FilePath = Environment.GetEnvironmentVariable("APPDATA") + "\\WriteO\\config.txt";
                break;
            default:
                Console.Error.WriteLine("Your OS is currently unsupported." +
                    " Please open an issue (https://www.codeberg.org/PavaLP1/WriteO/issues)" +
                    (verbose ? "/Pull request (https://www.codeberg.org/PavaLP1/WriteO/pulls)" : "") +
                    " for implementation. Please add info on how your OS handles file paths.");
                Environment.Exit(1);
                break;
        }
        Environment.SetEnvironmentVariable("WriteOPath", Path.GetDirectoryName(Files.FilePath));
        string dirName = Path.GetDirectoryName(Files.FilePath)!;
        if (!Path.Exists(dirName))
        {
            Directory.CreateDirectory(dirName);
        }
        if (!File.Exists(Files.FilePath))
        {
            File.Create(Files.FilePath).Dispose();
        }
        Files.NameFile = Files.JoinPath(Environment.GetEnvironmentVariable("WriteOPath")!, Files.NameFile);
    }
    private static void Fetch()
    {
        Lang.Init();
        (string userName, bool newUser) = DataFetcher.GetName()!;
        if (newUser)
        {
            string path;
            Console.WriteLine(Lang.GetText(Keys.serverSelectText));
            do
            {
                do { } while (string.IsNullOrEmpty(path = Console.ReadLine()!));
            } while (!Directory.Exists(path));
            Files.Log = Files.JoinPath(path, "log.ocht");
            Files.FS = Files.JoinPath(path, "files" + Files.Sep);
            Files.BlackList = Files.JoinPath(path, "blacklist.ocht");
            using (StreamWriter streamWriter = new StreamWriter(Files.FilePath))
            {
                streamWriter.WriteLine(Files.Log + '\n' + Files.FS + '\n' + Files.BlackList);
            }
        }
        else
        {
            using (StreamReader streamReader = new StreamReader(Files.FilePath))
            {
                Files.Log = streamReader.ReadLine()!;
                Files.FS = streamReader.ReadLine()!;
                Files.BlackList = streamReader.ReadLine()!;
            }
        }
        User.InitName(userName);
        //User.InitLang(DataFetcher.GetLang());
    }
    public static void ResetAll()
    {
        Console.CursorVisible = false;
        Console.ResetColor();
        ClearAll();
        Console.Clear();
        Console.CursorVisible = true;
        Environment.SetEnvironmentVariable("WriteOPath", null);
    }
    public static void ClearAll()
    {
        Console.Clear();
        Console.WriteLine("\x1b[3J");
    }
}
public static class Files
{
    public static char Sep => Path.DirectorySeparatorChar;

    public static string JoinPath(string dir, string file)
        => dir.TrimEnd(Sep) + Sep + file;

    public static string NameFile { get; set; } = "name.txt";
    public static string Log { get; set; } = "";
    public static string Usr { get; } = "usr.json";
    public static string FS { get; set; } = "";
    public static string FilePath { get; set; } = string.Empty;
    public static string BlackList { get; set; } = "BlackList.ocht";
}
