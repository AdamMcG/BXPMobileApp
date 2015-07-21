using BXP_MobileApp_WindowsPhone.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.Storage;

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

        public async Task fnGetAppointments()
        {
            HTTPRestViewModel oHttpViewModel = new HTTPRestViewModel();
            string strAppointmentDocumentText = "";
            string RESTAPIFunction = "";
            await oHttpViewModel.RESTcalls_GET_BXPAPI(RESTAPIFunction, strAppointmentDocumentText);
            StorageFile fAppointmentsXmlDocument = await ApplicationData.Current.LocalFolder.CreateFileAsync("Appointments.xml", CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(fAppointmentsXmlDocument, strAppointmentDocumentText);
            await fnParsingAppointmentList(fAppointmentsXmlDocument);
        }

        public async Task fnParsingAppointmentList(StorageFile AppointmentXMLDocument)
        {
            var appointmentDocument = await XmlDocument.LoadFromFileAsync(AppointmentXMLDocument);
        }


    }
}
