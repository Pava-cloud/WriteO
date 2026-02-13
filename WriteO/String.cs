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
}
