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

        private DateTime dtefrom;
        public DateTime pdteFrom
        {
            get { return dtefrom; }
            set
            {
                dtefrom = value;
                NotifyPropertyChanged("pdteFrom");
            }
        }

        private DateTime dteto;
        public DateTime pdteTo
        {
            get { return dteto; }
            set
            {
                dteto = value;
                NotifyPropertyChanged("pdteto");
            }
        }

        private string strsubject;
        public string pstrSubject
        {
            get { return strsubject; }
            set
            {
                strsubject = value;
                NotifyPropertyChanged("pstrSubject");
            }
        }

        private string strbody;
        public string pstrBody
        {
            get { return strbody; }
            set
            {
                strbody = value;
                NotifyPropertyChanged("pstrBody");
            }
        }

        private string strLink;
        public string pstrLink
        {
            get { return strLink; }
            set
            {
                strLink = value;
                NotifyPropertyChanged("pstrLink");
            }
        }

        private string strdata;
        public string pstrData
        {
            get { return strdata; }
            set
            {
                strdata = value;
                NotifyPropertyChanged("strData");
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
