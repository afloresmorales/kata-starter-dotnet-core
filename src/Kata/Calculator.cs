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
                return 0;
            var numbers = SplitNumbers(s);
            VerifyNegatives(numbers);
            return numbers.Sum();
        }

        static IEnumerable<int> SplitNumbers(string s)
        {
            var separator = new[] {",", "\n"};
            if (s.Contains("//"))
            {
                var str = s.Split("\n");
                separator = str.First().Replace("//", "").Replace("[","").Split("]");
                s = str.Last();
            }

            var numbers = s.Split(separator, StringSplitOptions.None).Select(int.Parse).Where(x => x < 1001);
            return numbers;
        }

        static void VerifyNegatives(IEnumerable<int> numbers)
        {
            var negatives = numbers.Where(x => x < 0);
            if (negatives.Any())
            {
                var multipleNegatives = string.Join(", ", negatives);
                throw new Exception($@"negatives not allowed: {multipleNegatives}");
            }
        }
    }
}