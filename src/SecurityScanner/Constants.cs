namespace SecurityScanner
{
    public class Constants
    {
        // 
        public static readonly List<string> ScanItems = new() { "AKCp5cbwV3mVFHUuKe2Z8npYYFXukj9mo1hXW8e3Yuaf7mjKUHEjsmHNCALFR3XtCAbsJ6VRi", "P@ssw0rd", "Administrator", "username", "password", "token" };

        // False positive 
        public static readonly List<string> FalsePositives = new() { "Password;", "_passwords[", "username = null;", "password\"", "password = lines[", "Administrator()", "(token", "global::System", "///", "Token.", "password = \"\"", ".Password", "Password,", "password)" };
        public static readonly List<string> FalsePositivesPatterns = new() { "token[,.)]", "username[,.}]", ".username" };
    }
}