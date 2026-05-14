namespace WriteO;

internal static class FileSystem
{
    private static string[] asciiTitle =
    {
        @"███████╗██╗██╗     ███████╗███████╗",
        @"██╔════╝██║██║     ██╔════╝██╔════╝",
        @"█████╗  ██║██║     █████╗  ███████╗",
        @"██╔══╝  ██║██║     ██╔══╝  ╚════██║",
        @"██║     ██║███████╗███████╗███████║",
        @"╚═╝     ╚═╝╚══════╝╚══════╝╚══════╝",
    };
    private static string[] options = new string[3];
    private static int selectedIndex = 0;
    private static void ReloadOptionsText()
    {
        options[0] = Lang.GetText(Keys.fileUploadText);
        options[1] = Lang.GetText(Keys.fileDownLoadText);
        options[2] = Lang.GetText(Keys.menuExit);
    }
    public static void Show()
    {
        Console.Title = "WriteO - " + Lang.GetText(Keys.menuFiles);
        ReloadOptionsText();
        ConsoleKey key;
        do
        {
            DrawMenu();

            var keyInfo = Console.ReadKey(true);
            key = keyInfo.Key;

            switch (key)
            {
                case ConsoleKey.UpArrow or ConsoleKey.K:
                    selectedIndex = (selectedIndex - 1 + options.Length) % options.Length;
                    break;
                case ConsoleKey.DownArrow or ConsoleKey.J:
                    selectedIndex = (selectedIndex + 1) % options.Length;
                    break;
                case ConsoleKey.Enter or ConsoleKey.Spacebar:
                    if (selectedIndex != options.Length - 1) HandleSelection();
                    else
                    {
                        selectedIndex = 0;
                        Console.Title = "WriteO";
                        return;
                    }
                    break;

                case ConsoleKey.Q:
                    Console.Title = "WriteO";
                    return;
                case ConsoleKey.U:
                    Upload();
                    break;
                case ConsoleKey.D:
                    Download();
                    break;
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
        string[] modeKeys = { "U", "D", "Q" };
        string[] modeIcons = { "", "", "󰈆" };
        Console.Clear();
        Console.CursorVisible = false;
        for (int i = 0; i < asciiTitle.Length; i++)
        {
            StringExtras.WriteCenteredMarkupText(asciiTitle[i], "[cyan3]", i + 10);
        }
        int windowWidth = Console.WindowWidth;
        int windowHeight = Console.WindowHeight;

        for (int i = 0; i < options.Length; i++)
        {
            string text = $"{modeIcons[i]}  {options[i].PadRight(40)}{modeKeys[i]}";

            int x = (windowWidth - text.Length) / 2;
            int y = (windowHeight / 2 - options.Length / 2) + 2 * i;

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
        Console.WriteLine(Lang.GetText(Keys.keyToContinueText));
        selectedIndex = 0;
        Console.ReadKey(true);
    }
}
internal enum FileModes
{
    Upload,
    Download
}
