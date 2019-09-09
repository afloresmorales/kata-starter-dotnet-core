using System;
using System.Collections.Generic;
using System.Linq;

namespace Kata
{
    public class Calculator
    {
        public int Add(string s="")
        {
            if(string.IsNullOrEmpty(s))
                return  0;

            var numbers = GetNumbers(s);
            VerifyNegatives(numbers);

            return numbers.Sum();
        }

        static void VerifyNegatives(IEnumerable<int> numbers)
        {
            var negatives = numbers.Where(x => x < 0);
            if (negatives.Any())
            {
                throw new Exception($@"negatives not allowed: {string.Join(", ", negatives)}");
            }
        }

        static IEnumerable<int> GetNumbers(string s)
        {
            var separator = new[] {"\n", ","};

            if (s.StartsWith("//"))
            {
                var parts = s.Split("\n");
                separator = parts[0].Replace("//", "").Replace("[", "").Split("]");
                s = parts[1];
            }

            var numbers = s.Split(separator, StringSplitOptions.None).Select(int.Parse).Where(x => x < 1001);
            return numbers;
        }
    }
}