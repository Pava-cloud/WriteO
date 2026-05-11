namespace WriteO;

public static class SettingMode
{
    private static int selectedIndex = 0;

    private static string[] options = new string[5];

    private static readonly string[] asciiTitle =
    {
        @" _____      _   _   _                 ",
        @"/  ___|    | | | | (_)                ",
        @"\ `--.  ___| |_| |_ _ _ __   __ _ ___ ",
        @" `--. \/ _ \ __| __| | '_ \ / _` / __|",
        @"/\__/ /  __/ |_| |_| | | | | (_| \__ \",
        @"\____/ \___|\__|\__|_|_| |_|\__, |___/",
        @"                             __/ |    ",
        @"                            |___/     "
    };
    private static void ReloadOptionsText()
    {
        Console.Title = "WriteO - " + Lang.GetText(Keys.menuSettings);
        options[0] = Lang.GetText(Keys.settingsNameText);
        options[1] = Lang.GetText(Keys.settiingsServerLocText);
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
        Console.CursorVisible = true;

        switch (selectedIndex)
        {
            case 0:
                Console.Write(Lang.GetText(Keys.nameEnterText));
                string temp = Console.ReadLine() ?? User.Name;
                User.Name = temp.Length > 4 ? temp : User.Name;
                break;

            case 1:
                Console.Write(Lang.GetText(Keys.serverSelectText));
                Files.Log = Console.ReadLine() ?? Files.Log;
                break;

            case 2:
                Console.Write(Lang.GetText(Keys.keyEnterText));
                if (int.TryParse(Console.ReadLine(), out int key))
                    User.Key = key;
                break;
            case 3:
                Console.Write(Lang.GetText(Keys.languageEnterText));
                if (Enum.TryParse<Languages>(Console.ReadLine(), true, out Languages lang))
                {
                    User.setLang(lang);
                    ReloadOptionsText();
                }
                break;
            case 4:
                return;
        }
        Console.WriteLine(Lang.GetText(Keys.keyToContinueText));
        selectedIndex = 0;
        Console.ReadKey(true);
    }
}
