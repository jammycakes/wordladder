namespace WordLadder
{
    public class Sowpods
    {
        private IDictionary<int, string[]> _words = new Dictionary<int, string[]>();

        private IEnumerable<string> ReadAll()
        {
            using var stream = this.GetType().Assembly
                .GetManifestResourceStream(this.GetType(), "data.sowpods.txt");

            using var reader = new StreamReader(stream);

            while (!reader.EndOfStream) {
                var word = reader.ReadLine().Trim().ToLowerInvariant();
                if (!String.IsNullOrWhiteSpace(word)) {
                    yield return word;
                }
            }
        }

        public void Load()
        {
            _words = ReadAll()
                .GroupBy(w => w.Length)
                .ToDictionary(g => g.Key, g => g.ToArray());
        }

        private IEnumerable<string> GetAdjacents(string word, int length)
        {
            if (_words.ContainsKey(length)) {
                return _words[length].Where(w => w.IsAdjacent(word));
            }
            else {
                return Enumerable.Empty<string>();
            }
        }

        public string[] GetAdjacents(string word)
        {
            return GetAdjacents(word, word.Length - 1)
                .Concat(GetAdjacents(word, word.Length))
                .Concat(GetAdjacents(word, word.Length + 1))
                .ToArray();
        }
    }
}
