using System;
using System.Collections.Generic;
using TestCommon;

namespace A6
{
    public class Q1ConstructBWT : Processor
    {
        public Q1ConstructBWT(string testDataName) 
        : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<String, String>)Solve);

        /// <summary>
        /// Construct the Burrows–Wheeler transform of a string
        /// </summary>
        /// <param name="text"> A string Text ending with a “$” symbol </param>
        /// <returns> BWT(Text) </returns>
        public string Solve(string text)
        {
            // text.Insert(0, text[text.Length - 1].ToString());
            // text.Remove(text.Length - 1);
            List<string> Strings = new List<string>();
            for(int i = 0; i < text.Length; i++)
            {
                Strings.Add(text);
                text = text.Insert(0, text[text.Length - 1].ToString());
                text = text.Remove(text.Length - 1);
            }
            Strings.Sort();
            string BWT = "";
            foreach (var s in Strings)
            {
                BWT += s[text.Length - 1];
            }
            return BWT;
        }
    }
}
