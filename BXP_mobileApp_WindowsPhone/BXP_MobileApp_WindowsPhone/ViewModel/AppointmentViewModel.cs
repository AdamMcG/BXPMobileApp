using BXP_MobileApp_WindowsPhone.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Storage;

namespace BXP_MobileApp_WindowsPhone.ViewModel
{
    class AppointmentViewModel
    {
        public AppointmentViewModel() { }


        private ObservableCollection<Appointment> ColAppointmentToday = new ObservableCollection<Appointment>();
        private ObservableCollection<Appointment> ColAppointmentTomorrow = new ObservableCollection<Appointment>();


        public ObservableCollection<Appointment> pColAppointmentToday
        {
            get { return ColAppointmentToday; }
            set { ColAppointmentToday = value; }
        }
        public ObservableCollection<Appointment> pColAppointmentTomorrow
        {
            get { return ColAppointmentTomorrow; }
            set { ColAppointmentTomorrow = value; }
        }

        //Pull down all of the appointments.
        public async Task fnGetAppointments()
        {
            HTTPRestViewModel oHttpViewModel = new HTTPRestViewModel();
            string strAppointmentDocumentText = "";
            string RESTAPIFunction = "";
            await oHttpViewModel.RESTcalls_GET_BXPAPI(RESTAPIFunction, strAppointmentDocumentText);

            fnParsingAppointmentList(strAppointmentDocumentText);
        }

        //Parse through the xml document of appointment objects.
        public void fnParsingAppointmentList(string AppointmentXMLDocument)
        {
            XElement xmlAppointmentDataElement = XDocument.Parse(AppointmentXMLDocument, LoadOptions.PreserveWhitespace).Root.Element("data");

            var colxmlAppointmentRecordElement = xmlAppointmentDataElement.Elements("record");

            foreach (var xmlRecordElement in colxmlAppointmentRecordElement)
            {
                string id = xmlRecordElement.Element("intId").Value;
                int intId = Int32.Parse(id);
                //Add rest of xml parsing elements here. Add constructor in Appointment class too. 
                int a = 2;
                Appointment oAppointmentObject = new Appointment();
                if (a == 4)
                    pColAppointmentToday.Add(oAppointmentObject);
                else if (a == 5)
                    pColAppointmentTomorrow.Add(oAppointmentObject);
                else
                { }
            }
        }


    }
}
