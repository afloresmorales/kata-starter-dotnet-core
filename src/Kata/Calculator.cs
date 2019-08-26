using System;

namespace Kata
{
    public class Calculator
    {
        public int Add(string s = "")
        {
            return string.IsNullOrEmpty(s) ? 0 : Int32.Parse(s);
        }
    }
}