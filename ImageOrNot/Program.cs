using System;
using WebWindows;
using WebWindows.Blazor;

namespace ImageOrNot
{
    class Program
    {
        static void Main(string[] args)
        {
            ComponentsDesktop.Run<Startup>("My Blazor App", "wwwroot/index.html");
        }
    }
}
