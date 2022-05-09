using System.Text;

namespace SecurityScanner;

public class Scanner
{
    private readonly string _path;
    private readonly List<string> _files;
    private readonly List<ScanObject> _scanItems = new ();
    private List<string> _dirtyFiles = new();
    private int _hitsCounter;

    public Scanner(string path, string filePattern = "*.cs")
    {
        _path = path;
        var secretsFileName = "SecretesToFind.txt";
        var secrets = File.ReadAllLines(secretsFileName);
        foreach (var secret in secrets)
        {
            _scanItems.Add(new ScanObject(secret));
        }
        _files = Directory.GetFiles(path, filePattern, SearchOption.AllDirectories).ToList();
        if (filePattern == "*.cs")
        {
            _files.RemoveAll(x => x.Contains(".g.cs"));
        }
    }

    public void Scan()
    {

        var sb = new StringBuilder();
        GetFormattedHeader(sb,"Scan details");
        sb.AppendLine($"searching for items: {_scanItems.ToCommaSeparatedString()}");
        sb.AppendLine($"searching on path: {_path}");
        GetFormattedHeader(sb, "Marked files");
        GetSuspectedFiles(sb);
        var total = _dirtyFiles.Count;
        GetFormattedHeader(sb, "Occurrences list");

        var falsePositives = Constants.FalsePositives;
        for (var j = 0; j < total; j++)
        {
            ScanSuspectedFiles(j, total, falsePositives, sb);
        }

        HitsCountReport(sb);
        File.WriteAllText(@"./result.txt", sb.ToString());
    }

    private void ScanSuspectedFiles(int j, int total, List<string> falsePositives, StringBuilder sb)
    {
        Console.WriteLine($"Scanning {j} out of {total}");
        var lines = File.ReadAllLines(_dirtyFiles[j]);
        bool added = false;

        for (var i = 0; i < lines.Length; i++)
        {
            for (var l = 0; l < _scanItems.Count; l++)
            {
                if (lines[i].Contains(_scanItems[l].Name) &&
                    !falsePositives.Exists(x => lines[i].ToLower().Contains(x.ToLower())))
                {
                    if (_scanItems[l].Name == "token")
                    {
                        if (lines[i].Length < 120)
                            continue;
                    }

                    if (!added)
                    {
                        sb.AppendLine($"File {_dirtyFiles[j]}");
                        added = true;
                    }

                    sb.AppendLine($"{lines[i].ToString()}");
                    sb.AppendLine($"\t'{_scanItems[l]}' at line {i}");
                    _hitsCounter++;
                    _scanItems[l].Count++;
                }
            }
        }
    }

    private void HitsCountReport(StringBuilder sb)
    {
        GetFormattedHeader(sb, "Summary");

        sb.AppendLine($"Found {_hitsCounter} items");
        sb.AppendLine("----------------------------");
        for (var i = 0; i < _scanItems.Count; i++)
        {
            sb.AppendLine($"'{_scanItems[i].Name}' : {_scanItems[i].Count} times ");
        }
    }

    private void GetFormattedHeader(StringBuilder sb,string header)
    {
        sb.AppendLine();
        sb.AppendLine("============================");
        sb.AppendLine(header);
        sb.AppendLine("============================");
        sb.AppendLine();
    }

    private void GetSuspectedFiles(StringBuilder sb)
    {
        foreach (var file in _files)
        {
            var txt = File.ReadAllText(file).ToLower();
            if (_scanItems.Any(x => txt.Contains(x.Name)))
            {
                _dirtyFiles.Add(file);
                sb.AppendLine($"File: {file}");
            }
        }
    }
}