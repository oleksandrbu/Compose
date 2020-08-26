using System.Collections.Generic;

namespace First_Server{
    public class SyncRequest : AbstructRequest, IRequest{
        public static string[] endpoints = {"/who", "/how", "/does", "/what"};
        public List<(string, string)> Request(List<string> urls){
            List<(string, string)> words = new List<(string, string)>();

            for (int i = 0; i < 4; i++){
                words.Add(GetRequest($"http://{urls[i]}:3030{endpoints[i]}"));
            }
            
            return words;
        }
    }
}