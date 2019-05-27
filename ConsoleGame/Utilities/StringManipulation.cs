using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.Utilities
{
    public static class StringManipulation
    {
        public static IEnumerable<string> Split(this string str, int chunkSize)
        {
            return Enumerable.Range(0, str.Length / chunkSize)
                .Select(i => str.Substring(i * chunkSize, chunkSize));
        }
    }
}
