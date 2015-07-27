using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BXP_MobileApp_WindowsPhone.Model
{
    class Appointment : INotifyPropertyChanged
    {
        public Appointment()
        {

        }

        private int intAppointmentid;
        public int pintAppointmentID
        {
            get { return intAppointmentid; }
            set
            {
                intAppointmentid = value;
                NotifyPropertyChanged("pintAppointmentID");
            }
        }

        private string strappointmentName;
        public string pstrAppointmentName
        {
            get { return strappointmentName; }
            set
            {
                strappointmentName = value;
                NotifyPropertyChanged("pstrAppointmentName");
            }
        }

        private DateTime dteStart;
        public DateTime pdteStart
        {
            get { return dteStart; }
            set
            {
                dteStart = value;
                NotifyPropertyChanged("pdteStart");
            }
        }

        private DateTime dteEnd;
        public DateTime pdteEnd
        {
            get { return dteEnd; }
            set
            {
                dteEnd = value;
                NotifyPropertyChanged("pdteEnd");
            }
        }

        private string url;
        public string pUrl
        {
            get { return url; }
            set
            {
                url = value;
                NotifyPropertyChanged("pUrl");
            }

        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
