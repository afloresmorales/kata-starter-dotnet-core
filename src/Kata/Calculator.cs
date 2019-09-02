using System;
using System.Linq;

namespace Kata
{
    public class Calculator
    {
        public int Add(string s="")
        {
            if(string.IsNullOrEmpty(s))
                return 0;
            var separator = new []{",","\n"};
            if (s.StartsWith("//"))
            {
                var parts = s.Split("\n");
                separator = parts[0].Replace("//", "").Replace("[","").Split("]");
                s = parts[1];
            }
            var numbers = s.Split(separator,StringSplitOptions.None).Select(int.Parse);
            var negatives = numbers.Where(x => x < 0);
            if (negatives.Any())
            {
                throw new Exception($@"negatives not allowed: {string.Join(", ", negatives)}");
            }

            var numbersLessThanThousandOne = numbers.Where(x => x < 1001);
            if (numbersLessThanThousandOne.Any())
            {
                return numbersLessThanThousandOne.Sum();
            }
            return numbers.Sum();
 
        }
    }
}