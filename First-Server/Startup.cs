using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace First_Server
{
    public class Startup
    {
        public bool FLAG = false;
        public string[] who = {"Повар", "Чебурек", "Кек"};
        public string[] how = {"влажно", "вздыхая", "задумчиво"};
        public string[] does = {"готовит", "прогает", "говорит"};
        public string[] what = {"орех", "котлетка", "кот"};
        public List<string> urlsList;
        IRequest request;
        public Startup(){
            request = new SyncRequest();
            urlsList = new List<string>();

            string line = Environment.GetEnvironmentVariable("COMPOSE_URLS");
            string[] enviromentUrls = line.Split(':');

            for (int i = 0; i < enviromentUrls.Length; i++){
                urlsList.Add(enviromentUrls[i]);
            }

            foreach (string str in urlsList){
                Console.WriteLine("{0}", str);
            }
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env){
            if (env.IsDevelopment()){
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/who", async context =>
                {
                    context.Response.ContentType = "text/html; charset=utf-8";
                    context.Response.Headers.Add("InCamp-Student", Environment.GetEnvironmentVariable("HOSTNAME"));

                    await context.Response.WriteAsync(Data.RandomWord(who));
                });
                endpoints.MapGet("/how", async context =>
                {
                    context.Response.ContentType = "text/html; charset=utf-8";
                    context.Response.Headers.Add("InCamp-Student", Environment.GetEnvironmentVariable("HOSTNAME"));

                    await context.Response.WriteAsync(Data.RandomWord(how));
                });
                endpoints.MapGet("/does", async context =>
                {
                    context.Response.ContentType = "text/html; charset=utf-8";
                    context.Response.Headers.Add("InCamp-Student", Environment.GetEnvironmentVariable("HOSTNAME"));

                    await context.Response.WriteAsync(Data.RandomWord(does));
                });
                endpoints.MapGet("/what", async context =>
                {
                    context.Response.ContentType = "text/html; charset=utf-8";
                    context.Response.Headers.Add("InCamp-Student", Environment.GetEnvironmentVariable("HOSTNAME"));

                    await context.Response.WriteAsync(Data.RandomWord(what));
                });
                endpoints.MapGet("/quote", async context =>
                {
                    context.Response.ContentType = "text/html; charset=utf-8";
                    context.Response.Headers.Add("InCamp-Student", Environment.GetEnvironmentVariable("HOSTNAME"));

                    await context.Response.WriteAsync($"{Data.RandomWord(who)} {Data.RandomWord(how)} {Data.RandomWord(does)} {Data.RandomWord(what)}.");
                });
                endpoints.MapGet("/incamp18-quote", async context =>
                {
                    context.Response.ContentType = "text/html; charset=utf-8";
                    context.Response.Headers.Add("InCamp-Student", Environment.GetEnvironmentVariable("HOSTNAME"));

                    List<string> urls = new List<string>();

                    for (int i = 0; i < 4; i++){
                        urls.Add(Data.RandomWord(urlsList));
                    }  
                    DateTime start = DateTime.Now;
                    List<(string, string)> words = request.Request(urls);
                    TimeSpan timeItTook = DateTime.Now - start;
                                     
                    string requestLine = "", requestWords = "";
                    for (int i = 0; i < 4; i++)
                    {
                        requestWords += $"{words[i].Item1} ";
                        requestLine += $"{words[i].Item1} from {words[i].Item2}\n";
                    }
                    
                    await context.Response.WriteAsync($"{requestWords}.\n{requestLine}\nFor:{timeItTook.Milliseconds}ms.\n");
                });
                endpoints.MapGet("/tests", async context =>
                {
                    FLAG = !FLAG;
                    if (FLAG){
                        request = new AsyncRequest();
                    } else {
                        request = new SyncRequest();
                    } 
                    if (FLAG){
                        await context.Response.WriteAsync("Async enable.");
                    } else {
                        await context.Response.WriteAsync("Async disable.");
                    }                    
                });
            });
        }
    }
}
