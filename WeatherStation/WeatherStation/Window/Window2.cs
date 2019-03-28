using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherStation.Shared;

namespace WeatherStation.Window
{
    class Window2 : IObserver<WeatherConditions>
    {
        private IDisposable unsubscriber;

        public virtual void Subscribe(IObservable<WeatherConditions> observable)
        {
            unsubscriber = observable.Subscribe(this);
        }


        public void OnCompleted()
        {
            Console.WriteLine("Вызвано OnCompleted ");
        }

        public void OnError(Exception error)
        {
            Console.WriteLine("Вызвано OnError ");
        }

        public void OnNext(WeatherConditions value)
        {
            Console.WriteLine("Вызвано OnNext2 " + value.Degri);
        }
    }
}
