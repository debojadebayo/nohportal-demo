using NUnit.Framework;

// Enable parallel execution at the assembly level
[assembly: Parallelizable(ParallelScope.Fixtures)]
// Limit the number of worker threads to avoid overwhelming the system
[assembly: LevelOfParallelism(4)]
