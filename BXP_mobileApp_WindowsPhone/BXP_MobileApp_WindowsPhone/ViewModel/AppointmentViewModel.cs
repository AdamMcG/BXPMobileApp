using BXP_MobileApp_WindowsPhone.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BXP_MobileApp_WindowsPhone.ViewModel
{
    class AppointmentViewModel : INotifyCollectionChanged
    {
        public AppointmentViewModel() { }


        public ObservableCollection<Appointment> ColAppointment = new ObservableCollection<Appointment>();

        public ObservableCollection<Appointment> pColAppointment
        {
            get { return ColAppointment; }
            set { ColAppointment = value; }

        }


        public async Task fnParsingAppointmentList()
        {

        }


    }
}
