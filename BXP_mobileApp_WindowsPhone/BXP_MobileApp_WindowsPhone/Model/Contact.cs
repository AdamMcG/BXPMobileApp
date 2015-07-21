using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BXP_MobileApp_WindowsPhone.Model
{
    class Contact : INotifyPropertyChanged
    {
        public Contact() { }

        public int intcontactID;
        public int pintContactID
        {
            get { return intcontactID; }
            set
            {
                intcontactID = value;
                NotifyPropertyChanged("pintContactID");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }

        }
    }
}