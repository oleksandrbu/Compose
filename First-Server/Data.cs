using System;
using System.Collections.Generic;

namespace First_Server{
    public class Data{
        public static string RandomWord(string[] wordList){
            var rand = new Random();
            return wordList[rand.Next(0, wordList.Length)];
        }
        public static string RandomWord(List<string> urls){
            var rand = new Random();
            return urls[rand.Next(0, urls.Count)];
        }
    }
}