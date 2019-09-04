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
            
            if (s.Contains("//"))
            {
                var str = s.Split("\n");
                separator = new[] {str.First().Replace("//", "")};
                s = str.Last();

            }
            var numbers = s.Split(separator, StringSplitOptions.None).Select(int.Parse);

         return numbers.Sum();
        }
    }
}