// See https://aka.ms/new-console-template for more information

using WordLadder;
using WordLadder.Darwin;

var dict = new Sowpods();
dict.Load();
var environment = new WordLadder.Darwin.Environment(dict, "mol", "stone");

environment.Populate();
for (var generation = 0; generation <= 10; generation++) {
    Console.WriteLine($"Generation {generation}");
    environment.Generate(25);
    environment.Cull();
    foreach (var item in environment.Population.OrderByDescending(p => p.GetFitness())) {
        Console.WriteLine($"{String.Join(", ", item.GetSteps())} (fitness {item.GetFitness():N3})");
    }
    Console.WriteLine();

    environment.NextGeneration();
}
