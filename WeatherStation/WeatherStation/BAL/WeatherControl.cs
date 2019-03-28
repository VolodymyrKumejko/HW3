using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherStation.Shared;

namespace WeatherStation.BAL
{
    class WeatherControl : IObservable<WeatherConditions>
    {
        WeatherConditions weatherConditionsOld = new WeatherConditions();
        private List<IObserver<WeatherConditions>> observers;

        public WeatherControl()
        {
            observers = new List<IObserver<WeatherConditions>>();
        }


        public IDisposable Subscribe(IObserver<WeatherConditions> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
           
            return new Unsubscriber(observers, observer);
        }

        public int countGet;
        public void GetWeather(string city)
        {
            WeatherConditions weatherConditions = new WeatherConditions();

             var client = new OpenWeatherAPI.OpenWeatherAPI("d9a69d574ef7c5bdccdc52b2b0b13458");

            var results = client.Query(city);
            if (client!=null)
            {
                weatherConditions.CurentTemperature = results.Main.Temperature.CelsiusCurrent;
                weatherConditions.Direction = results.Wind.Direction;
                weatherConditions.MaxTemperature = results.Main.Temperature.CelsiusMaximum;
                weatherConditions.MinTemperature = results.Main.Temperature.CelsiusMaximum;
                weatherConditions.SeaLevel = results.Main.SeaLevelAtm;
                weatherConditions.WindSpeedMetersPerSecond = results.Wind.SpeedMetersPerSecond;
                //weatherConditions.RainLevel = results.Rain.H3;
                weatherConditions.Degri = results.Wind.Degree;
                weatherConditionsOld = weatherConditions;
                weatherConditions.Cyti = city;
                countGet +=1;
                weatherConditions.CountCondition = countGet;

                foreach (var item in observers)
                {
                    item.OnNext(weatherConditions);
                }
            }
            else
            {
                foreach (var item in observers)
                {
                    item.OnError(new ExceptionWeather());
                }

            }
            
        }

        private class Unsubscriber : IDisposable
        {
            private List<IObserver<WeatherConditions>> _observers;
            private IObserver<WeatherConditions> _observer;

            public Unsubscriber(List<IObserver<WeatherConditions>> observers, IObserver<WeatherConditions> observer)
            {
                this._observers = observers;
                this._observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }
    }
}
