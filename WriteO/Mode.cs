using Spectre.Console;
using System.Text;
using static System.Console;
using static WriteO.Program;

namespace WriteO;

public static class Mode
{
    private static Modes[] options = { Modes.Messages, Modes.Files, Modes.Settings, Modes.Exit };
    private static int selectedIndex = 0;
    private static readonly string[] @asciiTitle =
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
                String.WriteCenteredMarkupText(asciiTitle[i], "", i + 10);
            }
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
                        return;
                }

            } while (true);
        }
    }
    private static void DrawMenu()
    {
        Console.Clear();
        Console.CursorVisible = false;
        for (int i = 0; i < asciiTitle.Length; i++)
        {
            String.WriteCenteredMarkupText(asciiTitle[i], "[DeepPink4_2]", i + 10);
        }
        int windowWidth = Console.WindowWidth;
        int windowHeight = Console.WindowHeight;

        for (int i = 0; i < options.Length; i++)
        {
            string text = options[i].ToString();
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
        Console.WriteLine("\nPress any key to return...");
        selectedIndex = 0;
        Console.ReadKey(true);
    }



    private static void Command(string cmd)
    {
        switch (cmd.Split(" ")[0])
        {
            case ":clear":
                File.Delete(Files.Log);
                File.Create(Files.Log).Dispose();
                break;
            case ":c":
                File.Delete(Files.Log);
                File.Create(Files.Log).Dispose();
                break;
            case ":cls":
                File.Delete(Files.Log);
                File.Create(Files.Log).Dispose();
                break;
            case ":h":
                String.WriteMarkupWarning("RTFM", "[gray]");
                break;
            case ":help":
                String.WriteMarkupWarning("RTFM", "[gray]");
                break;
            case ":cs":
                CmdChangeServer(cmd.Split(" ", 2, StringSplitOptions.TrimEntries)[1]);
                break;
            case ":changeserver":
                CmdChangeServer(cmd.Split(" ", 2, StringSplitOptions.TrimEntries)[1]);
                break;
            case ":cenc":
                CmdCenc(cmd.Remove(0, 6));
                break;
            case ":changeencoding":
                CmdCenc(cmd.Remove(0, 16));
                break;
            case ":q":
                break;
            case ":exit":
                break;
            case ":bl":
                if (BlackList(cmd.Split(" ", 2)[1]))
                    String.WriteWarning("User already blacklisted.");
                break;
            default:
                String.WriteMarkupWarning("Command not found", "[red]");
                break;
        }
    }
    private static bool BlackList(string user)
    {
        if (!File.Exists(Files.BlackList))
            File.Create(Files.BlackList).Dispose();
        using (StreamReader sr = new(Files.BlackList))
        {
            while (!sr.EndOfStream)
            {
                string name = sr.ReadLine()!;
                if (name == user) return true;
            }
        }
        using (StreamWriter sw = new(Files.BlackList, true))
        {
            sw.WriteLine(user);
        }
        return false;
    }
    public static void Message()
    {
        Console.Title = "WriteO - Messages";
        string line = "";
        string log, input;
        while (true)
        {
            if (IsBlackListed(Files.FilePath))
            {
                String.WriteWarning("You have been banned from this server.");
                String.WriteCenteredText("Please enter an addess for another server.", Console.WindowHeight / 2 + 1);
                CmdChangeServer(Console.ReadLine()!);
            }
            else
            {
                log = DataFetcher.Log();
                string[] splitLog = log.Split('\n');
                int lineCount = (splitLog.Length >= WindowHeight - 4) ? WindowHeight - 4 : splitLog.Length;

                ClearAll();
                #region Log Output
                WriteLine($"-- Messages --\n");
                for (int i = lineCount; i > 0; i--)
                {
                    WriteLine(splitLog[splitLog.Length - i]);
                }
                for (int i = WindowHeight - (lineCount + 4); i > 0; i--)
                {
                    WriteLine();
                }
                #endregion Log Output
                input = ReadLine()!;
                #region Commands
                if (input.StartsWith(':'))
                {
                    Command(input);
                    if (input == ":exit" || input == ":q") break;
                }
                #endregion Commands
                else
                {
                    line = $"{User.Name}: {input}\n";
                    BinaryWriter lineWriter = new(File.Open(Files.Log, System.IO.FileMode.Create));
                    lineWriter.Write(String.EncodeText(log + line, User.Key));
                    lineWriter.Close();
                }
            }
        }
        Console.Title = "WriteO";
    } // DONE
    private static void CmdCenc(string input)
    {
        switch (input)
        {
            case "u8":
                OutputEncoding = Encoding.UTF8;
                break;
            case "ascii":
                OutputEncoding = Encoding.ASCII;
                break;
            case "u16":
                OutputEncoding = Encoding.Unicode;
                break;
            //case "u32":
            //    OutputEncoding = Encoding.UTF32;
            //    break;
            default:
                String.WriteMarkupWarning("[bold red]Not a valid encoding", "[bold red]");
                break;
        }
    }
    private static void CmdChangeServer(string path)
    {
        do { } while (!Directory.Exists(path = ReadLine()!));
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
                    string blacklist = reader.ReadLine()!;
                    if (blacklist.ToLower() == User.Name.ToLower())
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
