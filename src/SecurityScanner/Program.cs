using System.Security.Cryptography.X509Certificates;
using System.Text;

var mainline = @"C:\Users\Administrator\Perforce\AUTOMATION_TOOLS\Unification_Tester\Mainline";
var files = Directory.GetFiles(mainline, "*.cs", SearchOption.AllDirectories).ToList();
files.RemoveAll(x => x.Contains(".g.cs"));
var scanItems = new List<string>() { "P@ssw0rd", "Administrator", "username", "password", "token" };
var falsePositives = new List<string>() { "Password;","_passwords[","username = null;", "password\"","password = lines[","Administrator()","(token","global::System", "///", "Token.", "password = \"\"", ".Password", "Password,", "password)" };
var falsePositivesPatterns = new List<string>() { "token[,.)]", "username[,.}]", ".username" };
var dirtyFiles = new List<string>();
var sb = new StringBuilder();
var resultCounter = 0;
var elementCounter = new int[scanItems.Count];
foreach (var file in files)
{
    var txt = File.ReadAllText(file).ToLower();
    if (scanItems.Any(x => txt.Contains(x)))
    {
        dirtyFiles.Add(file);
        sb.AppendLine($"File: {file}");
    }
}

sb.AppendLine("File list loaded");
sb.AppendLine("============================");
sb.AppendLine("============================");
sb.AppendLine("");
var total = dirtyFiles.Count;
for (var j = 0; j < total; j++)
{
    var temp = j % 9;
    if (temp == 0)
        Console.WriteLine($"Scanning {j} out of {total}");
    var lines = File.ReadAllLines(dirtyFiles[j]);
    bool added = false;

    for (var i = 0; i < lines.Length; i++)
    {
        for(var l=0;l<scanItems.Count;l++)
        {
            if (lines[i].Contains(scanItems[l]) && !falsePositives.Exists(x=>lines[i].ToLower().Contains(x.ToLower())))
            {
                if (scanItems[l] == "token")
                {
                    if (lines[i].Length < 120)
                        continue;
                }
                if (!added)
                {
                    
                    sb.AppendLine($"File {dirtyFiles[j]}");
                    added = true;
                }
                sb.AppendLine($"{lines[i].ToString()}");

                sb.AppendLine($"\t'{scanItems[l]}' at line {i}");
                resultCounter++;
                elementCounter[l]++;
            }
        }
    }
}
sb.AppendLine("============================");
sb.AppendLine($"Found {resultCounter} hits");
for (var i=0;i< scanItems.Count;i++)
{
    sb.AppendLine($"'{scanItems[i]}' : {elementCounter[i]} times ");

}
File.WriteAllText(@"./result.txt", sb.ToString());