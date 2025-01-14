﻿using ElectionGuard.InteropGenerator.Models;
using System.Text.Json;
using ElectionGuard.InteropGenerator.FileGenerators;

namespace ElectionGuard.InteropGenerator
{
    /// <summary>
    /// Responsible for generating interop code for all languages for ElectionGuard
    /// </summary>
    internal class InteropGenerator
    {
        private readonly List<FileGeneratorBase> _generators;

        public InteropGenerator()
        {
            _generators = new List<FileGeneratorBase>
            {
                new CsFileGenerator(),
                new CFileGenerator(),
            };
        }

        public async Task GenerateAll(string interopJsonPath)
        {
            var egClasses = await GetInteropClasses(interopJsonPath);
            foreach (var egClass in egClasses)
            {
                var generatedResults = _generators.Select(g => g.Generate(egClass));
                foreach (var generatedResult in generatedResults)
                {
                    await generatedResult.Write();
                }
            }
        }

        private static async Task<EgClass[]> GetInteropClasses(string interopJsonPath)
        {
            await using var openStream = File.OpenRead(interopJsonPath);
            var egClasses = JsonSerializer.Deserialize<EgClass[]>(openStream);
            if (egClasses == null) throw new Exception("EgInteropClasses.json was empty");
            return egClasses;
        }
    }
}
