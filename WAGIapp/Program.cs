using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Cors.Infrastructure;

using WAGIapp;

using MudBlazor.Services;
using WAGIapp.AI;

using System;
using System.Threading;
using System.Runtime.Versioning;
using System.Runtime.InteropServices.JavaScript;
using System.Net.Http;

[assembly: SupportedOSPlatform("browser")]

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddMudServices();
/*
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyHeader();  //set the allowed origin  
            policy.AllowAnyMethod();
            policy.AllowAnyOrigin();
        });
});*/

/*
Master.Singleton.Actions.AddAction("Thinking", LogAction.ThinkIcon);
Master.Singleton.Actions.AddAction("Creating a memory", LogAction.MemoryIcon);
Master.Singleton.Actions.AddAction("Hello there");
*/


Task.Run(async () =>
{
    while (true)
    {
        await Task.Delay(10000);
        await Master.Singleton.Tick();
        /*switch (Random.Shared.Next(0,6))
        {
            case 0:
                var a = Master.Singleton.Actions.AddAction("Thinking", LogAction.ThinkIcon);
                await Task.Delay(4000);
                a.Text = "testing text is cool and all but does the transition finally work of what? im quite tired of waiting for it to start working .. i would love to get to other things and stop obsesing about this.";
                break;
            case 1:
                Master.Singleton.Actions.AddAction("Creating a memory", LogAction.MemoryIcon);
                break;
            case 2:
                Master.Singleton.Actions.AddAction("Hello there");
                break;
            case 3:
                Master.Singleton.Notes.Add("Notes are cool and its cool i can show them!");
                break;
            case 4:
                Master.Singleton.Notes.Add("Notes are cool and its\ncool i can show them!");
                break;
            case 5:
                Master.Singleton.Notes.Add("Notes are cool and its cool i can show them! but what if they are looooong as fuck .. will the ui manage or will it breaky?");
                break;

        }*/



    }
}).ConfigureAwait(true);



await builder.Build().RunAsync();
