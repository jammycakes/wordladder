using System.Threading.Tasks.Dataflow;

namespace WordLadder.Darwin
{
    public class Creature
    {
        private readonly Environment _environment;
        private List<string> _genome = new List<string>();

        public Creature(Environment environment, string[] genome)
        {
            _environment = environment;
            _genome = new List<string>(genome);
        }

        public IEnumerable<string> GetSteps() => _genome.AsEnumerable();

        /* ====== Cross-breed with another ladder ====== */

        /// <summary>
        ///  Breeds two creatures together. Their new genome is the genome that both
        ///  have in common.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>

        public Creature BreedWith(Creature other)
        {
            var i = 0;
            var newGenome = this.GetSteps().Zip(other.GetSteps())
                .TakeWhile(s => s.First == s.Second)
                .Select(s => s.First)
                .ToArray();

            return new Creature(_environment, newGenome);
        }

        public double GetFitness()
        {
            /*
             * For genomes that haven't yet hit the target, the fitness is derived
             * from the Levenshtein distance between the last step in the ladder,
             * and the target word. In this case, high distance is less fit
             * whereas low distance is more fit. So use 1/Levenshtein.
             *
             * Genomes have hit the target when the last step is adjacent to the
             * target. In this case, we want smaller genomes to count for more than
             * larger ones. So use 1 + 5 / genome length.
             *
             * TODO: try experimenting with different values here
             */

            var lastStep = this._genome.Last();
            var target = this._environment.Last;
            if (lastStep.IsAdjacent(target)) {
                return 1 + 5 / (double)this._genome.Count;
            }
            else {
                return 1 / (double)lastStep.Levenshtein(target);
            }
        }

        public bool Grow()
        {
            /*
             * If we have hit the target, we don't need to grow any more.
             */

            var lastStep = this._genome.Last();
            if (lastStep.IsAdjacent(_environment.Last)) {
                return false;
            }

            /*
             * If we haven't hit the target, look for a candidate word
             * that hasn't been used yet. Group by Levenshtein distance
             */

            var candidates = _environment.Dictionary.GetAdjacents(lastStep)
                .Except(this._genome)
                .GroupBy(c => c.Levenshtein(_environment.Last))
                .OrderBy(g => g.Key)
                .ToList();

            if (!candidates.Any()) {
                return false;
            }

            var r1 = 11 / (11 - 10 * _environment.Random.NextDouble()) - 1;
            var index = (int)Math.Floor(r1 * candidates.Count / 10);
            var chosen = candidates[index].ToList();
            var r2 = _environment.Random.Next(chosen.Count);
            this._genome.Add(chosen[r2]);

            return true;
        }
    }
}
