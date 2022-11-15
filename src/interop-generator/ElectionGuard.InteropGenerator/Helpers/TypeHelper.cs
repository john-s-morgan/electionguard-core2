﻿namespace ElectionGuard.InteropGenerator.Helpers
{
    internal static class TypeHelper
    {
        private static readonly Dictionary<string, string> ReturnTypes = new()
        {
            { "string", "char *" },
            { "bool", "bool " },
            { "ulong", "uint64_t " },
        };

        public static string CsToC(string csType)
        {
            return ReturnTypes[csType];
        }
    }
}