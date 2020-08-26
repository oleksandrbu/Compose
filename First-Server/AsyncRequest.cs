using System.Collections.Generic;
using System.Threading.Tasks;

namespace First_Server{
    public class AsyncRequest : AbstructRequest, IRequest{
        public List<(string, string)> Request(List<string> urls){
            return AdditionalTask(urls).Result;
        }
        private async Task<List<(string, string)>> AdditionalTask(List<string> urls){
            List<(string, string)> words = new List<(string, string)>();

            Task<(string, string)> who, how, does, what;

            who = Task.Run(() => GetRequest($"http://{urls[0]}:3030/who"));
            how = Task.Run(() => GetRequest($"http://{urls[1]}:3030/how"));
            does = Task.Run(() => GetRequest($"http://{urls[2]}:3030/does"));
            what = Task.Run(() => GetRequest($"http://{urls[3]}:3030/what"));

            await Task.WhenAll(new[] { who, how, does, what});
            
            words.Add(((string, string)) who.Result);
            words.Add(((string, string)) how.Result);
            words.Add(((string, string)) does.Result);
            words.Add(((string, string)) what.Result);

            return words;
        }
    }
}