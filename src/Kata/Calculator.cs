using System;
using System.Linq;

namespace Kata
{
    public class Calculator
    {
        public int Add(string s="")
        {
            if (String.IsNullOrEmpty(s))
                return 0;

            var separator = new [] {",","\n"};
            if (s.Contains("//"))
            {
                var str = s.Split('\n');
                separator = new[] {str.First().Replace("//", "")};
                s = str.Last(); // 1;2;3
            }
            var numbers = s.Split(separator, StringSplitOptions.None).Select(int.Parse);

            var negatives = numbers.Where(x => x < 0);

            if (negatives.Any())
            {
                throw new Exception($@"negatives not allowed: {negatives.First()}");
            }


            if (numbers.Count() == 1)
                return numbers.First();

            return numbers.Sum();
        }
    }
}