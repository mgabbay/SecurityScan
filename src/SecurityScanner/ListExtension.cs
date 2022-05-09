namespace SecurityScanner;

public static class ListExtension
{
    public static string ToCommaSeparatedString<T>(this List<T> list)
    {
        return $"[{string.Join(", ", list)}]";
    }
}