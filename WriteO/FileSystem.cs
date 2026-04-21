namespace WriteO;

internal static class FileSystem
{
    private static string[] asciiTitle =
    {
        @"______ _ _        _____           _                 ",
        @"|  ___(_) |      /  ___|         | |                ",
        @"| |_   _| | ___  \ `--. _   _ ___| |_ ___ _ __ ___  ",
        @"|  _| | | |/ _ \  `--. \ | | / __| __/ _ \ '_ ` _ \ ",
        @"| |   | | |  __/ /\__/ / |_| \__ \ ||  __/ | | | | |",
        @"\_|   |_|_|\___| \____/ \__, |___/\__\___|_| |_| |_|",
        @"                         __/ |                      ",
        @"                        |___/                       "
    };
    private static string[] options =
    {
        "Upload",
        "Download",
        "Exit"
    };
    private static int selectedIndex = 0;

    public static void Show()
    {
        Console.Title = "WriteO - Files";
        ConsoleKey key;
        do
        {
            DrawMenu();

            var keyInfo = Console.ReadKey(true);
            key = keyInfo.Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    selectedIndex = (selectedIndex - 1 + options.Length) % options.Length;
                    break;
                case ConsoleKey.K:
                    selectedIndex = (selectedIndex - 1 + options.Length) % options.Length;
                    break;

                case ConsoleKey.DownArrow:
                    selectedIndex = (selectedIndex + 1) % options.Length;
                    break;
                case ConsoleKey.J:
                    selectedIndex = (selectedIndex + 1) % options.Length;
                    break;
                case ConsoleKey.Enter:
                    if (selectedIndex != options.Length - 1) HandleSelection();
                    else
                    {
                        selectedIndex = 0;
                        Console.Title = "WriteO";
                        return;
                    }
                    break;

                case ConsoleKey.Spacebar:
                    if (selectedIndex != options.Length - 1) HandleSelection();
                    else
                    {
                        selectedIndex = 0;
                        Console.Title = "WriteO";
                        return;
                    }
                    break;

                case ConsoleKey.Escape:
                    Console.Title = "WriteO";
                    return;
            }

        } while (true);
    }
    private static void Move(FileModes mode, string local, string name)
    {
        switch (mode)
        {
            case FileModes.Upload:
                File.Move(local, Files.FS + name);
                break;
            case FileModes.Download:
                File.Move(Files.FS + name, local + name);
                break;
            default:
                break;
        }
    }
    private static void Upload()
    {
        Console.WriteLine("Choose a file to upload or \":exit\" to exit.");
        string path = Console.ReadLine()!;
        if (path == ":exit" || string.IsNullOrEmpty(path)) return;
        string fileName;
        if (Environment.OSVersion.Platform == PlatformID.Unix)
            fileName = path.Split('/')[^1];
        else fileName = path.Split('\\')[^1];
        Move(FileModes.Upload, path, fileName);
    }
    private static void Download()
    {
        string[] files = Directory.GetFiles(Files.FS);
        foreach (string file in files)
        {
            if (Environment.OSVersion.Platform == PlatformID.Unix)
                Console.WriteLine(file.Split('/')[^1]);
            else Console.WriteLine(file.Split('\\')[^1]);
        }
        Console.WriteLine("Which file to download? (\":exit\" to exit)");
        string dfile = Console.ReadLine()!;
        if (dfile == ":exit") return;
        if (File.Exists(Files.FS + dfile))
        {
            Console.WriteLine("Where to download to? (\":exit\" to exit)");
            string path = Console.ReadLine()!;
            Move(FileModes.Download, path, dfile);
            Console.WriteLine("Success!");
            Thread.Sleep(500);
        }
    }
    private static void DrawMenu()
    {
        Console.Clear();
        Console.CursorVisible = false;
        for (int i = 0; i < asciiTitle.Length; i++)
        {
            String.WriteCenteredMarkupText(asciiTitle[i], "[cyan3]", i + 10);
        }
        int windowWidth = Console.WindowWidth;
        int windowHeight = Console.WindowHeight;

        for (int i = 0; i < options.Length; i++)
        {
            string text = options[i];
            int x = (windowWidth - text.Length) / 2;
            int y = (windowHeight / 2 - options.Length / 2) + i;

            Console.SetCursorPosition(x, y);

            if (i == selectedIndex) text = $"[Blue]{text}[/]";
            Spectre.Console.AnsiConsole.MarkupLine(text);
        }
    }
    private static void HandleSelection()
    {
        Console.Clear();
        switch (selectedIndex)
        {
            case 0:
                Upload();
                break;
            case 1:
                Download();
                break;
            case 2:
                return;
        }
        Console.WriteLine("\nPress any key to return...");
        selectedIndex = 0;
        Console.ReadKey(true);
    }
}
internal enum FileModes
{
    Upload,
    Download
}
