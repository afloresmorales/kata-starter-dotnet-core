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
            if (numbers.Count() == 1)
                return numbers.First();

            return numbers.Sum();
        }
    }
}