using Spectre.Console;
using System.Text;
using static System.Console;
using static WriteO.Program;
namespace WriteO;

public static class Mode
{
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
                String.WriteCenteredMarkupText(asciiTitle[i], "", i);
            }
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<Modes>()
                    .WrapAround()
                    //.HighlightStyle()
                    .AddChoices(Modes.Messages, Modes.FileSystem, Modes.Settings, Modes.Exit)); // Same as return values

            if (choice == Modes.Exit) break;
            else if (choice == Modes.Messages) Message();
            else if (choice == Modes.FileSystem) FileSystem.Show();
            else if (choice == Modes.OldFileSystem) FileSystem.Show();
            else if (choice == Modes.Settings) SettingMode.Show();
        }
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
                File.Create (Files.Log).Dispose();
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
            default:
                String.WriteMarkupWarning("Command not found", "[red]");
                break;
        }
    }
    public static void Message()
    {
        Console.Title = "WriteO - Messages";
        string line = "";
        string log, input;
        while (true)
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
        Console.Title = "WriteO";
    } // DONE
    private static void CmdCenc(string input)
    {
        switch(input)
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
		do { } while(!Directory.Exists(path = ReadLine()!));
		if (Environment.OSVersion.Platform == PlatformID.Unix)
		{
			Files.Log = path + (path[^1] == '/' ? "log.txt" : "/log.ocht");
			Files.FS = path + (path[^1] == '/' ? "files/" : "/files/");
		}
		else if (Environment.OSVersion.Platform == PlatformID.Win32NT)
		{
			Files.Log = path + (path[^1] == '\\' ? "log.txt" : "\\log.ocht");
			Files.FS = path + (path[^1] == '\\' ? "files\\" : "\\files\\");
		}
    	using (StreamWriter streamWriter = new StreamWriter(Files.FilePath))
		{
			streamWriter.WriteLine(Files.Log + '\n' +  Files.FS);
		}
    }
}
public enum Modes
{
    Messages,
    FileSystem,
    OldFileSystem,
    Settings,
    Exit
}
