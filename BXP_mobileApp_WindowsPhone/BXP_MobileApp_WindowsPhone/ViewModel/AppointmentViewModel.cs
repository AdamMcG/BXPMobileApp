using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BXP_MobileApp_WindowsPhone.ViewModel
{
    class AppointmentViewModel:INotifyPropertyChanged
    {
        public AppointmentViewModel() { }

        public PropertyChangedEventHandler PropertyChanged = delegate { };
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
            
        }
    }
}
