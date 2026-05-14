namespace WriteO;

public static class SettingMode
{
    private static int selectedIndex = 0;
    private static int[] valueLines = new int[5];
    private static string[] options = new string[5];

    private static readonly string[] asciiTitle =
    {
        @"███████╗███████╗████████╗████████╗██╗███╗   ██╗ ██████╗ ███████╗",
        @"██╔════╝██╔════╝╚══██╔══╝╚══██╔══╝██║████╗  ██║██╔════╝ ██╔════╝",
        @"███████╗█████╗     ██║      ██║   ██║██╔██╗ ██║██║  ███╗███████╗",
        @"╚════██║██╔══╝     ██║      ██║   ██║██║╚██╗██║██║   ██║╚════██║",
        @"███████║███████╗   ██║      ██║   ██║██║ ╚████║╚██████╔╝███████║",
        @"╚══════╝╚══════╝   ╚═╝      ╚═╝   ╚═╝╚═╝  ╚═══╝ ╚═════╝ ╚══════╝",
    };
    private static void ReloadOptionsText()
    {
        Console.Title = "WriteO - " + Lang.GetText(Keys.menuSettings);
        options[0] = Lang.GetText(Keys.settingsNameText);
        options[1] = Lang.GetText(Keys.settingsServerLocText);
        options[2] = Lang.GetText(Keys.settingsKeyText);
        options[3] = Lang.GetText(Keys.settingsLanguageText);
        options[4] = Lang.GetText(Keys.menuExit);
    }
    public static void Show()
    {
        ReloadOptionsText();
        selectedIndex = 0;
        ConsoleKey key;
        do
        {
            DrawMenu();
            var keyInfo = Console.ReadKey(true);
            key = keyInfo.Key;
            int temp;
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
                    return;
                case ConsoleKey.N:
                    temp = selectedIndex;
                    selectedIndex = 0;
                    HandleSelection();
                    selectedIndex = temp;
                    break;
                case ConsoleKey.S:
                    temp = selectedIndex;
                    selectedIndex = 1;
                    HandleSelection();
                    selectedIndex = temp;
                    break;
                case ConsoleKey.I:
                    temp = selectedIndex;
                    selectedIndex = 2;
                    HandleSelection();
                    selectedIndex = temp;
                    break;
                case ConsoleKey.G:
                    temp = selectedIndex;
                    selectedIndex = 3;
                    HandleSelection();
                    selectedIndex = temp;
                    break;

            }

        } while (true);
    }

    private static void DrawMenu()
    {
        string[] modeKeys = { "N", "S", "I", "G", "Q" };
        string[] modeIcons = { "", "", "󰌆", "", "󰈆" };

        Console.Clear();
        Console.CursorVisible = false;
        for (int i = 0; i < asciiTitle.Length; i++)
        {
            StringExtras.WriteCenteredMarkupText(asciiTitle[i], "[DeepPink4_2]", i + 10);
        }
        int windowWidth = Console.WindowWidth;
        int windowHeight = Console.WindowHeight;

        for (int i = 0; i < options.Length; i++)
        {
            string text = $"{modeIcons[i]}  {options[i].PadRight(40)}{modeKeys[i]}";
            int x = (windowWidth - text.Length) / 2;
            int y = (windowHeight / 2 - options.Length / 2) + 3 * i;

            Console.SetCursorPosition(x, y);

            if (i == selectedIndex) text = $"[Blue]{text}[/]";
            Spectre.Console.AnsiConsole.MarkupLine(text);
            Console.SetCursorPosition(x, y + 1);
            valueLines[i] = Console.CursorTop;
            switch (i)
            {
                case 0:
                    Console.Write(User.Name);
                    break;
                case 1:
                    Console.Write(Path.GetDirectoryName(Files.Log));
                    break;
                case 2:
                    Console.Write(User.Key);
                    break;
                case 3:
                    Console.Write(User.Lang);
                    break;
            }
        }
    }

    private static void HandleSelection()
    {
        Console.CursorVisible = true;
        Console.CursorTop = valueLines[selectedIndex];
        int tmp = Console.CursorLeft;
        Console.Write(new string(' ', Console.WindowWidth / 2));
        Console.CursorLeft = tmp;

        switch (selectedIndex)
        {
            case 0:
                string temp = Console.ReadLine() ?? User.Name;
                if (temp.Length < 5)
                {
                    Console.CursorLeft = tmp;
                    Spectre.Console.AnsiConsole.Markup("[DeepPink4_2]" + Lang.GetText(Keys.errorNameTooShort) + "[/]");
                    Thread.Sleep(1000);
                }
                else User.Name = temp;
                break;

            case 1:
                Files.Log = Console.ReadLine() ?? Files.Log;
                break;

            case 2:
                if (int.TryParse(Console.ReadLine(), out int key))
                    User.Key = key;
                break;
            case 3:
                if (Enum.TryParse<Languages>(Console.ReadLine(), true, out Languages lang))
                {
                    User.setLang(lang);
                    ReloadOptionsText();
                }
                break;
            case 4:
                return;
        }
    }
}
