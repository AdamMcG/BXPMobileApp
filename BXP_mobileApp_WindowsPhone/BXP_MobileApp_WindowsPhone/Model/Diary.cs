using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BXP_MobileApp_WindowsPhone.Model
{
    class Diary : INotifyPropertyChanged
    {
        private ObservableCollection<Appointment> ColAppointment = new ObservableCollection<Appointment>();

        public ObservableCollection<Appointment> pColAppointment
        {
            get { return ColAppointment; }
            set { ColAppointment = value; }
        }

        private string strFunction;
        public string propStrFunction
        {
            get { return strFunction; }
            set
            {
                strFunction = value;
                NotifyPropertyChanged("propStrFunction");
            }
        }

        private int intErrorId;
        public int propIntErrorId
        {
            get { return intErrorId; }
            set
            {
                intErrorId = value;
                NotifyPropertyChanged("propIntErrorId");
            }
        }

        private DateTime dtePeriodStart;
        public DateTime propDtePeriodStart
        {
            get { return dtePeriodStart; }
            set
            {
                dtePeriodStart = value;
                NotifyPropertyChanged("propDtePeriodStart");
            }
        }

        private DateTime dtePeriodEnd;
        public DateTime propDtePeriodEnd
        {
            get { return dtePeriodEnd; }
            set
            {
                dtePeriodEnd = value;
                NotifyPropertyChanged("propDtePeriodEnd");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));


        }
    }
}
