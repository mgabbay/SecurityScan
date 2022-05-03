
//var source = @"C:\Users\mgabbay\Downloads";

using System.Text;

var files = Directory.GetFiles(args[0], "*.*",SearchOption.AllDirectories)
    .Where(f=>new FileInfo(f).Length / (1024 * 1024) > 50);
var sb = new StringBuilder();
foreach (var file in files)
{
    sb.AppendLine(file);
}
File.WriteAllText(@"./result.txt",sb.ToString());