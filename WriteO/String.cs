using Spectre.Console;
namespace WriteO;

public class String
{
    internal static string EncodeText(string input, int key)
    {
        string retval = "";
        foreach (char c in input)
        {
            retval += (char)(c + key);
        }
        return retval;
    }
    internal static string DecodeText(string input, int key)
    {
        return EncodeText(input, -key);
    }
    internal static void WriteCenteredText(string input, int line)
    {
        int x = (Console.WindowWidth - input.Length) / 2;
        Console.SetCursorPosition(x, line);
        Console.WriteLine(input);
    }
    internal static void WriteCenteredMarkupText(string input, int line)
    {
        int x = (Console.WindowWidth - input.Length) / 2;
        Console.SetCursorPosition(x, line);
        Spectre.Console.AnsiConsole.MarkupLine(input);
    }
}
