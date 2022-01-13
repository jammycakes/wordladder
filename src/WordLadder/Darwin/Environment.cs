namespace WordLadder.Darwin
{
    public class Environment
    {
        public Sowpods Dictionary { get; }
        public string First { get; }
        public string Last { get; }

        public int PopulationSize { get; set; } = 100;

        public IList<Creature> Population { get; private set; } = new List<Creature>();

        public Random Random { get; } = new();

        public Environment(Sowpods dictionary, string first, string last)
        {
            Dictionary = dictionary;
            First = first;
            Last = last;
        }

        public void Populate()
        {
            /*
             * The initial population consists of creatures whose genome consists
             * solely of the first word.
             */
            var population = Enumerable.Range(0, PopulationSize)
                .Select(i => new Creature(this, new[] {First}));

            Population = population.ToList();
        }

        public int Grow()
        {
            int grown = 0;

            foreach (var creature in Population) {
                grown += creature.Grow() ? 1 : 0;
            }

            return grown;
        }

        public void Generate(int maxSteps)
        {
            for (var i = 0; i < maxSteps; i++) {
                Grow();
            }
        }

        public void Cull()
        {
            var numberToSurvive = (int)Math.Sqrt(PopulationSize) + 1;
            var survivors = Population
                .OrderByDescending(p => p.GetFitness())
                .Take(numberToSurvive + 1);
            Population = survivors.ToList();
        }

        public void NextGeneration()
        {
            // First let's apply the selection criteria.

            var newPopulation =
                from dad in Population
                from mum in Population
                where dad != mum
                select dad.BreedWith(mum);

            Population = newPopulation.Take(PopulationSize).ToList();
        }
    }
}
