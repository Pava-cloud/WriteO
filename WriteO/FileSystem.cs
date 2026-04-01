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
/*    public static void OldShow()
    { // BROKEN ??
        int mode = 3;
        do
        {
            ClearAll();
            for (int i = 0; i < asciiTitle.Length; i++)
            {
                String.WriteCenteredMarkupText(asciiTitle[i], "", i);
            }
            WriteLine("1.....Upload\n2.....Download\n3.....Exit\n");
            if (int.TryParse(ReadLine(), out mode))
            {
                #region Upload
                if (mode == 1)
                {
                    WriteLine("Choose a file to upload or \":exit\" to exit.");
                    string path = ReadLine()!;
                    if (path == ":exit" || string.IsNullOrEmpty(path)) break;
                    string fileName;
                    if (Environment.OSVersion.Platform == PlatformID.Unix)
                        fileName = path.Split('/')[^1];
                    else
                        fileName = path.Split('\\')[^1];
                    File.Move(path, Files.FS + fileName);
                }
                #endregion Upload
                #region Download
                if (mode == 2)
                {
                    string[] files = Directory.GetFiles(Files.FS);
                    foreach (string file in files)
                    {
                        if (Environment.OSVersion.Platform == PlatformID.Unix)
                            WriteLine(file.Split('/')[^1]);
                        else WriteLine(file.Split('\\')[^1]);
                    }
                    WriteLine("Which file to download? (\":exit\" to exit)");
                    string dfile = ReadLine()!;
                    if (dfile == ":exit") break;
                    if (File.Exists(Files.FS + dfile))
                    {
                        WriteLine("Where to download to? (\":exit\" to exit)");
                        string path = ReadLine()!;
                        File.Move(Files.FS + dfile, path! + dfile);
                        WriteLine("Success!");
                        Thread.Sleep(500);
                    }
                }
                #endregion Download
            }
        } while (mode != 3);
    } // BROKEN ?? */
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

                case ConsoleKey.DownArrow:
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
            String.WriteCenteredMarkupText(asciiTitle[i], "[cyan3]", i);
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
