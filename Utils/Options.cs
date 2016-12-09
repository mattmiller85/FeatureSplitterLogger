namespace FeatureSplitterLogger.Utils
{
    public class Options
    {
        public string FeaturePath { get; set; }
        public string Profile { get; set; }
        private string LineNumberRangeString { get; set; }
        public bool HasLineNumberRange { get; set; }
        public LineNumberRange LineNumberRange { get; set; }
        private string LineNumberString { get; set; }
        private int[] LineNumbers { get; set; }
        public bool HasLineNumbers { get; set; }
    }

    public struct LineNumberRange
    {
        public int From { get; set; }
        public int To { get; set; }
    }
}