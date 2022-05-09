namespace SecurityScanner
{
    public class ScanObject
    {
        public ScanObject(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        public int Count { get; set; } = 0;
    }
}