namespace SecurityScanner
{
    public class Constants
    {
        public static readonly List<string> ScanItems = new() { "P@ssw0rd", "Administrator", "username", "password", "token" };
        public static readonly List<string> FalsePositives = new() { "Password;", "_passwords[", "username = null;", "password\"", "password = lines[", "Administrator()", "(token", "global::System", "///", "Token.", "password = \"\"", ".Password", "Password,", "password)" };
        public static readonly List<string> FalsePositivesPatterns = new() { "token[,.)]", "username[,.}]", ".username" };
    }
}