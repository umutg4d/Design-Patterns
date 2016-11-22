using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2_Observer
{
    class Program
    {
        static void Main(string[] args)
        {
            WeatherData subject = new WeatherData();
            CurrentConditions cDisplay = new CurrentConditions(subject);
            HeatIndexDisplay hDisplay = new HeatIndexDisplay(subject);
            subject.setMeasurements(80, 65, 30.4f);
            subject.setMeasurements(82, 70, 29.2f);
            subject.setMeasurements(78, 90, 29.2f);
            Console.ReadLine();
        }
    }
    class WeatherData:ISubject
    {
        private float temp, hum, pressure;
        private ArrayList observers;
        public WeatherData()
        {
            observers = new ArrayList();
        }
        public void measurementsChanged()
        {
            notifyObserver();
        }

        public void registerObserver(IObserver a)
        {
            observers.Add(a);
        }

        public void removeObserver(IObserver a)
        {
            int index = observers.IndexOf(a);
           if (index >= 0)
                observers.RemoveAt(index);
            
        }

        public void notifyObserver()
        {
            for (int i = 0; i < observers.Count; i++)
            {
                ((IObserver)observers[i]).update(temp, hum, pressure);
            }
        }
        public void setMeasurements(float t,float h,float p)
        {
            this.temp = t;
            this.hum = h;
            this.pressure = p;
            measurementsChanged();
        }
    }
    class CurrentConditions : IObserver,IDisplayElement
    {
        private float temperature;
        private float humidity;
        private ISubject subject;
        public CurrentConditions(ISubject s)
        {
            subject = s;
            s.registerObserver(this);
        }
        public void display()
        {
            Console.WriteLine("Current conditions: "+temperature+"F degrees and "+humidity+"% humidity");
        }

        public void update(float temp, float hum, float pressure)
        {
            this.temperature = temp;
            this.humidity = hum;
            display();
        }
    }
    class HeatIndexDisplay : IObserver, IDisplayElement
    {
        private ISubject wData;
        float heatIndex = 0.0f;
        public HeatIndexDisplay(ISubject s)
        {
            this.wData = s;
            wData.registerObserver(this);
        }
        public void display()
        {
            Console.WriteLine("Heat index is {0}",heatIndex);
        }

        public void update(float temp, float hum, float pressure)
        {
            heatIndex = computeHeatIndex(temp, hum);
            display();
        }
        private float computeHeatIndex(float t, float rh)
        {
            float index = (float)((16.923 + (0.185212 * t) + (5.37941 * rh) - (0.100254 * t * rh)
                + (0.00941695 * (t * t)) + (0.00728898 * (rh * rh))
                + (0.000345372 * (t * t * rh)) - (0.000814971 * (t * rh * rh)) +
                (0.0000102102 * (t * t * rh * rh)) - (0.000038646 * (t * t * t)) + (0.0000291583 *
                (rh * rh * rh)) + (0.00000142721 * (t * t * t * rh)) +
                (0.000000197483 * (t * rh * rh * rh)) - (0.0000000218429 * (t * t * t * rh * rh)) +
                0.000000000843296 * (t * t * rh * rh * rh)) -
                (0.0000000000481975 * (t * t * t * rh * rh * rh)));
            return index;
        }
    }
    interface IDisplayElement
    {
        void display();
    }
    interface ISubject
    {
        void registerObserver(IObserver a);
        void removeObserver(IObserver a);
        void notifyObserver();
    }
    interface IObserver
    {
         void update(float temp, float hum, float pressure);
    }
}
