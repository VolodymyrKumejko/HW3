using OpenWeatherAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherStation.BAL;
using WeatherStation.Window;

namespace WeatherStation
{
    class Program
    {
        static void Main(string[] args)
        {
            WeatherControl provider = new WeatherControl();
            Window1 window1 = new Window1();
            window1.Subscribe(provider);
            Window2 window2 = new Window2();
            window2.Subscribe(provider);
            Window3 window3 = new Window3();
            window3.Subscribe(provider);

            do
            {
                provider.GetWeather("Korosten");
            } while (((ConsoleKeyInfo)Console.ReadKey()).Key!=ConsoleKey.Enter);
            window3.Unsubscribe();
            do
            {
                provider.GetWeather("Kyiv");
            } while (((ConsoleKeyInfo)Console.ReadKey()).Key != ConsoleKey.Enter);

            // var client = new OpenWeatherAPI.OpenWeatherAPI("d9a69d574ef7c5bdccdc52b2b0b13458");
            // var results = client.Query("Kyiv");
            // Console.WriteLine($"The temperature in Kyiv is {results.Main.Temperature.CelsiusCurrent}C. There is {results.Wind.SpeedMetersPerSecond} f/s wind in the {results.Wind.Direction} direction.");

            Console.ReadLine();
        }
    }
}
