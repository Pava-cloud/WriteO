using System.Text;
using static WriteO.Program;

namespace WriteO;

public static class Mode
{
    private static Modes[] options = { Modes.Messages, Modes.Files, Modes.Settings, Modes.Exit };
    private static int selectedIndex = 0;
    private static readonly string[] asciiTitle =
    {
        @" _    _      _ _        _____ ",
        @"| |  | |    (_) |      |  _  |",
        @"| |  | |_ __ _| |_ ___ | | | |",
        @"| |/\| | '__| | __/ _ \| | | |",
        @"\  /\  / |  | | ||  __/\ \_/ /",
        @" \/  \/|_|  |_|\__\___| \___/ "

    };
    public static void Select()
    {
        while (true)
        { // TODO: Update for mouse support
            ClearAll();
            for (int i = 0; i < asciiTitle.Length; i++)
            {
                StringExtras.WriteCenteredMarkupText(asciiTitle[i], "", i + 10);
            }
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

                    case ConsoleKey.Escape:
                        return;
                }

            } while (true);
        }
    }
    private static void DrawMenu()
    {
        string[] OutputModes = { Lang.GetText(Keys.menuMessages), Lang.GetText(Keys.menuFiles), Lang.GetText(Keys.menuSettings), Lang.GetText(Keys.menuExit) };
        Console.Clear();
        Console.CursorVisible = false;
        for (int i = 0; i < asciiTitle.Length; i++)
        {
            StringExtras.WriteCenteredMarkupText(asciiTitle[i], "[DeepPink4_2]", i + 10);
        }
        int windowWidth = Console.WindowWidth;
        int windowHeight = Console.WindowHeight;

        for (int i = 0; i < OutputModes.Length; i++)
        {
            string text = OutputModes[i];
            int x = (windowWidth - text.Length) / 2;
            int y = (windowHeight / 2 - OutputModes.Length / 2) + i;

            Console.SetCursorPosition(x, y);

            if (i == selectedIndex) text = $"[Blue]{text}[/]";
            Spectre.Console.AnsiConsole.MarkupLine(text);
        }
    }

    private static void HandleSelection()
    {
        Console.Clear();
        Console.CursorVisible = true;

        switch (selectedIndex)
        {
            case 0:
                Message();
                break;
            case 1:
                FileSystem.Show();
                break;
            case 2:
                SettingMode.Show();
                break;
            case 3:
                return;
        }
        selectedIndex = 0;
    }

    private static void Command(string cmd)
    {
        switch (cmd.Split(" ")[0])
        {
            case ":clear" or ":c" or ":cls":
                File.Delete(Files.Log);
                File.Create(Files.Log).Dispose();
                break;
            case ":help" or ":h":
                StringExtras.WriteMarkupWarning("RTFM", "[gray]");
                break;
            case ":changeserver" or ":cs":
                CmdChangeServer(cmd.Split(" ", 2, StringSplitOptions.TrimEntries)[1]);
                break;
            case ":changeencoding" or ":cenc":
                CmdCenc(cmd.Split(' ')[1]);
                break;
            case ":exit" or ":q":
                break;
            case ":blacklist" or ":bl":
                if (cmd.Split(" ").Length != 1)
                {
                    if (NewBlackList(cmd.Split(" ", 2)[1]))
                        StringExtras.WriteWarning(Lang.GetText(Keys.alreadyBlacklistedText));
                }
                else
                {
                    ClearAll();
                    Console.WriteLine(Lang.GetText(Keys.blacklistTitle));
                    Console.WriteLine(File.ReadAllText(Files.BlackList));
                    Spectre.Console.AnsiConsole.MarkupLine("[gray]" + Lang.GetText(Keys.keyToContinueText) + "[/]");
                    Console.ReadKey();
                }
                break;
            default:
                if (cmd.IndexOf(':', 1) != -1) Write(cmd);
                else StringExtras.WriteWarning(Lang.GetText(Keys.commandNotFoundText));
                break;
        }
    }
    /// <summary>
    /// Appends the user string to thw Blacklist File unless it starts with "-u ",
    /// in which case matching entries are removed. Returns true if the file
    /// remains unchanged.
    /// </summary>
    /// <param name="user">The user string to append or remove.</param>
    /// <returns>True if no changes were made; otherwise false.</returns>
    private static bool NewBlackList(string user)
    {
        string path = Files.BlackList;

        if (!File.Exists(path))
        {
            if (user.StartsWith("-u "))
            {
                return true;
            }

            File.WriteAllText(path, user + Environment.NewLine);
            return false;
        }

        string[] lines = File.ReadAllLines(path);
        bool changed = false;
        StringBuilder content = new();

        if (user.StartsWith("-u "))
        {
            string userName = user.Remove(0, 3);
            foreach (string line in lines)
            {
                if (!(line == userName))
                {
                    content.Append(line + Environment.NewLine);
                }
                else changed = true;
            }
            File.WriteAllText(path, content.ToString());
            return !changed;
        }
        else
        {
            bool exists = false;

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i] == user)
                {
                    exists = true;
                    break;
                }
            }

            if (exists)
            {
                return true;
            }

            File.AppendAllText(path, user + Environment.NewLine);
            return false;
        }
    }
    public static void Message()
    {
        Console.Title = "WriteO - " + Lang.GetText(Keys.menuMessages);
        string log, input;
        while (true)
        {
            if (IsBlackListed(Files.FilePath))
            {
                StringExtras.WriteWarning(Lang.GetText(Keys.gotBannedText));
                StringExtras.WriteCenteredText(Lang.GetText(Keys.serverSelectText), Console.WindowHeight / 2 + 1);
                CmdChangeServer(Console.ReadLine()!);
            }
            else
            {
                log = DataFetcher.Log();
                string[] splitLog = log.Split('\n');
                int lineCount = (splitLog.Length >= Console.WindowHeight - 4) ? Console.WindowHeight - 4 : splitLog.Length;

                ClearAll();
                #region Log Output
                Console.WriteLine($"-- " + Lang.GetText(Keys.menuMessages) + " --\n");
                for (int i = lineCount; i > 0; i--)
                {
                    Console.WriteLine(splitLog[splitLog.Length - i]);
                }
                for (int i = Console.WindowHeight - (lineCount + 4); i > 0; i--)
                {
                    Console.WriteLine();
                }
                #endregion Log Output
                input = Console.ReadLine()!;
                #region Input
                if (input.StartsWith(':'))
                {
                    Command(input);
                    if (input == ":exit" || input == ":q") break;
                }
                else
                {
                    Write(input);
                }
                #endregion Input
            }
        }
        Console.Title = "WriteO";
    } // DONE
    public static void Write(string input)
    {
        string log = DataFetcher.Log();
        string line = $"{User.Name}: {input}\n";
        BinaryWriter lineWriter = new(File.Open(Files.Log, System.IO.FileMode.Create));
        lineWriter.Write(StringExtras.EncodeText(log + line, User.Key));
        lineWriter.Close();
    }
    private static void CmdCenc(string input)
    {
        switch (input.ToLower())
        {
            case "u8":
                Console.OutputEncoding = Encoding.UTF8;
                break;
            case "ascii" or "ansi":
                Console.OutputEncoding = Encoding.ASCII;
                break;
            case "u16":
                Console.OutputEncoding = Encoding.Unicode;
                break;
            default:
                StringExtras.WriteWarning(Lang.GetText(Keys.invalidEncoding));
                break;
        }
    }
    private static void CmdChangeServer(string path)
    {
        if (!Directory.Exists(path))
        {
            do { } while (!Directory.Exists(path = Console.ReadLine()!));
        }
        if (!IsBlackListed(path))
        {
            if (Environment.OSVersion.Platform == PlatformID.Unix)
            {
                Files.Log = path + (path[^1] == '/' ? "log.ocht" : "/log.ocht");
                Files.BlackList = path + (path[^1] == '/' ? "blacklist.ocht" : "/blacklist.ocht");
                Files.FS = path + (path[^1] == '/' ? "files/" : "/files/");
            }
            else if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                Files.Log = path + (path[^1] == '\\' ? "log.txt" : "\\log.ocht");
                Files.BlackList = path + (path[^1] == '\\' ? "blacklist.ocht" : "\\blacklist.ocht");
                Files.FS = path + (path[^1] == '\\' ? "files\\" : "\\files\\");
            }
            using (StreamWriter streamWriter = new StreamWriter(Files.FilePath))
            {
                streamWriter.WriteLine(Files.Log + '\n' + Files.FS);
            }
        }
    }
    private static bool IsBlackListed(string path)
    {
        try
        {
            using (StreamReader reader = new(Files.BlackList))
            {
                while (!reader.EndOfStream)
                {
                    if (reader.ReadLine()!.ToLower() == User.Name.ToLower())
                        return true;
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An unexpected error occured ({ex.Message})");
        }
        return false;
    }
}

public enum Modes
{
    Messages,
    Files,
    Settings,
    Exit
}
