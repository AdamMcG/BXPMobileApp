using BXP_MobileApp_WindowsPhone.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BXP_MobileApp_WindowsPhone.ViewModel
{
    class SettingsViewModel : INotifyPropertyChanged
    {
        private Setting obSetting;
        public Setting propObSetting
        {
            get { return obSetting; }
            set
            {
                obSetting = value;
                NotifyPropertyChanged("propObSetting");
            }
        }

        private static string strSystem = "";
        public string propStrSystem
        {
            get { return strSystem; }
            set
            {
                strSystem = value;
                NotifyPropertyChanged("propStrSystem");
            }
        }

        private static string strUsername = "";
        public string propStrUsername
        {
            get { return strUsername; }
            set
            {
                strUsername = value;
                NotifyPropertyChanged("propStrUsername");
            }
        }
        public async Task<Boolean> fn_retrieveLoginSession(string strPassword)
        {
            Boolean check = false;
            try
            {
                string pstrSystem = "client_" + propStrSystem.ToLower();
                HTTPRestViewModel oHttpRestVm = new HTTPRestViewModel();
                string function = "https://ww3.allnone.ie/client/" + pstrSystem + "/cti/userAPP_main.asp";
                string strForOutput = "";
                List<KeyValuePair<string, string>> listKVmyParameter = fn_addParamsToList(strPassword, pstrSystem);
                strForOutput = await oHttpRestVm.RESTcalls_POST_BXPAPI(function, listKVmyParameter);
                if (strForOutput == "N/A")
                    return check;

                   check = fn_ParseLoginXMLDocument(strForOutput, function, pstrSystem);
                   
            }
            catch (Exception e) {
                e.Message.ToString();
               
            }
            return check;
        }

        private static List<KeyValuePair<string, string>> fn_addParamsToList(string strPassword, string pstrSystem)
        {
            List<KeyValuePair<string, string>> listKVmyParameter = new List<KeyValuePair<string, string>>();
            KeyValuePair<string, string> myParameter = new KeyValuePair<string, string>("strFunction", "login");
            listKVmyParameter.Add(myParameter);
            myParameter = new KeyValuePair<string, string>("strSystem", pstrSystem);
            listKVmyParameter.Add(myParameter);
            myParameter = new KeyValuePair<string, string>("strClient_Username", strUsername);
            listKVmyParameter.Add(myParameter);
            myParameter = new KeyValuePair<string, string>("strClient_Password", strPassword);
            listKVmyParameter.Add(myParameter);
            return listKVmyParameter;
        }

        public Boolean fn_ParseLoginXMLDocument(string output, string function, string strSystem)
        {
            Boolean count = false;
            try
            {
                var xmlDocument = XDocument.Parse(output);
                var xmlDataElement = xmlDocument.Element("data");
                Login myLogin = Login.Instance;
                int a = Int32.Parse(xmlDataElement.Element("intErrorId").Value);
                myLogin.propIntErrorId = a;
                myLogin.propStrError = xmlDataElement.Element("strError").Value;
                if (myLogin.propIntErrorId == 0)
                {
                    myLogin.propIntClient_Id = Int32.Parse(xmlDataElement.Element("intClient_Id").Value);
                    myLogin.propStrClient_SessionField = xmlDataElement.Element("strClient_SessionField").Value;
                }
                myLogin.propStrFunctionURL = function;
                myLogin.propStrSystemUsed = strSystem;
                count = true;
            }
            catch (Exception e)
            {

                e.ToString();
            }
            return count;
        }

        public async Task<Boolean> fn_retrieveSettingsData()
        {
            Login myLogin = Login.Instance;
            HTTPRestViewModel ohttpViewModel = new HTTPRestViewModel();
            #region ParametersForPost
            List<KeyValuePair<string, string>> listKvparameters = new List<KeyValuePair<string, string>>();
            KeyValuePair<string, string> kvMyparameters = new KeyValuePair<string, string>("strFunction", "settings");
            listKvparameters.Add(kvMyparameters);
            kvMyparameters = new KeyValuePair<string, string>("strSystem", myLogin.propStrSystemUsed);
            listKvparameters.Add(kvMyparameters);
            kvMyparameters = new KeyValuePair<string, string>("intClient_id", myLogin.propIntClient_Id.ToString());
            listKvparameters.Add(kvMyparameters);
            kvMyparameters = new KeyValuePair<string, string>("strClient_SessionField", myLogin.propStrClient_SessionField);
            listKvparameters.Add(kvMyparameters);
            #endregion
            string xmlParsingOutput = await ohttpViewModel.RESTcalls_POST_BXPAPI(myLogin.propStrFunctionURL, listKvparameters);
            return fn_parseSettingsXMLDocument(xmlParsingOutput);
        }

        public Boolean fn_parseSettingsXMLDocument(string xmlToBeParsed)
        {
            Boolean count = false;
            try
            {
                XDocument xmlSettingsDocument = XDocument.Parse(xmlToBeParsed);
                XElement xmlSettingsData = xmlSettingsDocument.Element("data");
                obSetting = new Setting();
                obSetting.propStrFunction = xmlSettingsData.Element("strFunction").Value;
                obSetting.propStrError = xmlSettingsData.Element("strError").Value;
                obSetting.propIntErrorId = Int32.Parse(xmlSettingsData.Element("intErrorId").Value);
                count = true;
            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
            return count;
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
