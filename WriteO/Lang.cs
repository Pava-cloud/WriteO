using static WriteO.Languages;
using static WriteO.Keys;
namespace WriteO;

public static class Lang
{
    private static Dictionary<(Languages, Keys), string> Dict = new();
    public static string getText(Keys val)
    {
        return Dict[(User.Lang, val)];
    }
    public static void Init()
    {
        InitEN();
        InitDE();
    }
    private static void InitEN()
    {
        Dict.Add((en, serverSelectText), "Please select a path for the server.");
        Dict.Add((en, alreadyBlacklistedText), "User already Blacklisted");
        Dict.Add((en, keyToContinueText), "Press any key to continue");
        Dict.Add((en, commandNotFoundText), "Command not found");
        Dict.Add((en, blacklistTitle), "Blacklisted Users:");
        Dict.Add((en, menuMessages), "Messages");
        Dict.Add((en, menuFiles), "Files");
        Dict.Add((en, menuSettings), "Settings");
        Dict.Add((en, menuExit), "Exit");
        Dict.Add((en, gotBannedText), "You have been banned from this server.");
        Dict.Add((en, invalidEncoding), "Not a valid encoding.");

    }
    private static void InitDE()
    {
        Dict.Add((de, serverSelectText), "Bitte Pfad für den Server wählen.");
        Dict.Add((de, alreadyBlacklistedText), "Nutzer bereits blockiert");
        Dict.Add((de, keyToContinueText), "Drücken Sie eine beliebige Taste");
        Dict.Add((de, commandNotFoundText), "Befehl nicht gefunden");
        Dict.Add((de, blacklistTitle), "Blockierte Nutzer:");
        Dict.Add((de, menuMessages), "Nachrichten");
        Dict.Add((de, menuFiles), "Dateien");
        Dict.Add((de, menuSettings), "Einstellungen");
        Dict.Add((de, menuExit), "Verlassen");
        Dict.Add((de, gotBannedText), "Sie wurden von diesem Verlauf blockiert.");
        Dict.Add((de, invalidEncoding), "Keine erlaubte Verschlüsselung");
    }
}

public enum Languages
{
    en,
    de,
}
public enum Keys
{
    serverSelectText,
    alreadyBlacklistedText,
    keyToContinueText,
    commandNotFoundText,
    blacklistTitle,
    menuMessages,
    menuFiles,
    menuSettings,
    menuExit,
    gotBannedText,
    invalidEncoding,
}
