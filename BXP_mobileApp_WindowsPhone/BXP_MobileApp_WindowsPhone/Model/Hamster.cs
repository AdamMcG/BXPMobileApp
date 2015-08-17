using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace BXP_MobileApp_WindowsPhone.Model
{
    class Hamster : INotifyPropertyChanged
    {
        public Hamster() { }
        public Hamster(string name, int value)
        {
            this.propHamsterName = name;
            this.propHamsterValue = value;
        }
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));

        }

        public string propHamsterName
        { get; set; }

        public int propHamsterValue { get; set; }

        public SolidColorBrush gridColour { get; set; }

    }
}
