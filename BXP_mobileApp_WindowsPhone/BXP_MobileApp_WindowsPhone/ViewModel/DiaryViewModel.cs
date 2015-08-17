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
    class DiaryViewModel : INotifyPropertyChanged
    {
        public DiaryViewModel() { }

        private Diary obdiary = new Diary();
        public Diary propObDiary
        {
            get { return obdiary; }
            set
            {
                obdiary = value;
                NotifyPropertyChanged("propObDiary");
            }
        }
       
        public async Task fn_PostingToServerForDiary(string strFunctionToCall)
        {
            Login myLogin = Login.Instance;
            HTTPRestViewModel oHttpViewModel = new HTTPRestViewModel();
            List<KeyValuePair<string, string>> listKVParameters = fn_addingPostParamsToList(strFunctionToCall, myLogin);
                 string RESTAPIFunction = myLogin.propStrFunctionURL;
                 if (RESTAPIFunction.Equals("N/A"))
                 {
                     return;
                 }
                 fnParsingThroughRetrievedDiary( await oHttpViewModel.RESTcalls_POST_BXPAPI(RESTAPIFunction, listKVParameters));
        }

        private static List<KeyValuePair<string, string>> fn_addingPostParamsToList(string strFunctionToCall, Login myLogin)
        {
            #region KeyValueParameters
            List<KeyValuePair<string, string>> listKVParameters = new List<KeyValuePair<string, string>>();
            KeyValuePair<string, string> kvMyParameters = new KeyValuePair<string, string>("strFunction", strFunctionToCall);
            listKVParameters.Add(kvMyParameters);
            kvMyParameters = new KeyValuePair<string, string>("strSystem", myLogin.propStrSystemUsed);
            listKVParameters.Add(kvMyParameters);
            kvMyParameters = new KeyValuePair<string, string>("intClient_Id", myLogin.propIntClient_Id.ToString());
            listKVParameters.Add(kvMyParameters);
            kvMyParameters = new KeyValuePair<string, string>("strClient_SessionField", myLogin.propStrClient_SessionField);
            listKVParameters.Add(kvMyParameters);
            kvMyParameters = new KeyValuePair<string, string>("intDiary", "3");
            listKVParameters.Add(kvMyParameters);
            #endregion
            return listKVParameters;
        }

        public void fnParsingThroughRetrievedDiary(string AppointmentXMLDocument)
        {
            try
            {
                XElement xmlAppointmentDataElement = XDocument.Parse(AppointmentXMLDocument, LoadOptions.PreserveWhitespace).Element("data");
                propObDiary.propStrFunction = xmlAppointmentDataElement.Element("strFunction").Value;
                propObDiary.propIntErrorId = Int32.Parse(xmlAppointmentDataElement.Element("intErrorId").Value);
                propObDiary.propDtePeriodStart = DateTime.Parse(xmlAppointmentDataElement.Element("dtePeriodStart").Value);
                propObDiary.propDtePeriodEnd = DateTime.Parse(xmlAppointmentDataElement.Element("dtePeriodEnd").Value);
                var xmlListOfItems = xmlAppointmentDataElement.Elements("item");
                fn_pushingParsedDataToAppointmentList(xmlListOfItems);

            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
        }

        private void fn_pushingParsedDataToAppointmentList(IEnumerable<XElement> xmlListOfItems)
        {
            try
            {
                foreach (var diaryItemElement in xmlListOfItems)
                {
                    Appointment obAppointmentForDiary = new Appointment();
                    obAppointmentForDiary.pintAppointmentID = Int32.Parse(diaryItemElement.Element("id").Value);
                    obAppointmentForDiary.pstrAppointmentName = diaryItemElement.Element("title").Value;
                    obAppointmentForDiary.pdteStart = DateTime.Parse(diaryItemElement.Element("start").Value);
                    obAppointmentForDiary.pdteEnd = DateTime.Parse(diaryItemElement.Element("end").Value);
                    obAppointmentForDiary.pUrl = diaryItemElement.Element("url").Value;
                    propObDiary.pColAppointment.Add(obAppointmentForDiary);
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
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
