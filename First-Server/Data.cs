using System;

namespace First_Server{
    public class Data{
        public static string[] urls = {
            "http://server1:3030",
            "http://server2:3030",
            "http://server3:3030"
        };
        public static string[] links = {"/who", "/how", "/does", "/what"};

        public static string RandomWord(string[] wordList){
            var rand = new Random();
            return wordList[rand.Next(0, wordList.Length)];
        }
    }
}