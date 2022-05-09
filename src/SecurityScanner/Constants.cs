namespace SecurityScanner
{
    public class Constants
    {
        // False positive - consider move to file
        public static readonly List<string> FalsePositives = new() { "Password;", "_passwords[", "username = null;", "password\"", "password = lines[", "Administrator()", "(token", "global::System", "///", "Token.", "password = \"\"", ".Password", "Password,", "password)" };
        public static readonly List<string> FalsePositivesPatterns = new() { "token[,.)]", "username[,.}]", ".username" };
    }
}