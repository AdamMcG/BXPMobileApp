using BXP_MobileApp_WindowsPhone.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BXP_MobileApp_WindowsPhone.ViewModel
{
    class SettingsViewModel
    {
        public async Task<Boolean> fn_retrieveLoginSessionID(string strSystem, string strUsername, string strPassword)
        {
            HTTPRestViewModel oHttpRestVm = new HTTPRestViewModel();
            string function = "https://ww3.allnone.ie/client/" + strSystem + "/cti/userAPP_main.asp";
            string strForOutput = "";
            #region PostParameters
            List<KeyValuePair<string, string>> listKVmyParameter = new List<KeyValuePair<string, string>>();
            KeyValuePair<string, string> myParameter = new KeyValuePair<string, string>("strFunction", "login");
            listKVmyParameter.Add(myParameter);

            myParameter = new KeyValuePair<string, string>("strSystem", strSystem);
            listKVmyParameter.Add(myParameter);
            myParameter = new KeyValuePair<string, string>("strClient_Username", strUsername);
            listKVmyParameter.Add(myParameter);
            myParameter = new KeyValuePair<string, string>("strClient_Password", strPassword);
            listKVmyParameter.Add(myParameter);
            #endregion

            strForOutput = await oHttpRestVm.RESTcalls_POST_BXPAPI(function, listKVmyParameter);
            return fn_ParseLoginXMLDocument(strForOutput, function, strSystem);
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
                myLogin.propIntClient_Id = Int32.Parse(xmlDataElement.Element("intClient_Id").Value);
                myLogin.propStrClient_SessionField = xmlDataElement.Element("strClient_SessionField").Value;
                myLogin.propStrFunctionURL = function;
                myLogin.propStrSystemUsed = strSystem;
                count = true;
            }
            catch (Exception e)
            {

                e.Message.ToString();
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
                Setting obSetting = new Setting();
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
    }
}
