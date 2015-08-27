using BXP_MobileApp_WindowsPhone.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace BXP_MobileApp_WindowsPhone.ViewModel
{
    class SettingsViewModel : INotifyPropertyChanged
    {
        public StylingViewModel styling = new StylingViewModel();
        private Setting obSetting = new Setting();
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

        //Prepare and send POST Request to Specified URL
        public async Task<Boolean> fn_retrieveLoginSession(string strPassword)
        {
            Boolean check = false;
            try
            {
                string pstrSystem = "client_" + propStrSystem.ToLower();
                HTTPRestViewModel oHttpRestVm = new HTTPRestViewModel();
                string function = "https://ww3.allnone.ie/client/" + pstrSystem + "/cti/userAPP_main.asp";
                string strForOutput = "";
                List<KeyValuePair<string, string>> listKVmyParameter = fnAddParamsToLoginPOST(strPassword, pstrSystem);
                strForOutput = await oHttpRestVm.RESTcalls_POST_BXPAPI(function, listKVmyParameter);
                if (strForOutput == "N/A")
                    return check;

                check = fn_ParseLoginXMLDocument(strForOutput, function, pstrSystem);

            }
            catch (Exception e)
            {
                e.Message.ToString();

            }
            return check;
        }

        //Prepare Parameters for Login Post
        private static List<KeyValuePair<string, string>> fnAddParamsToLoginPOST(string strPassword, string pstrSystem)
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

        //Parse through Login Response XML
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

        //Prepare Post call for Primary configuration settings of app.
        public async Task<Boolean> fn_retrieveConfigPrimaryData()
        {
            Login myLogin = Login.Instance;
            HTTPRestViewModel ohttpViewModel = new HTTPRestViewModel();
            #region ParametersForPost
            List<KeyValuePair<string, string>> listKvparameters = new List<KeyValuePair<string, string>>();
            KeyValuePair<string, string> kvMyparameters = new KeyValuePair<string, string>("strFunction", "config_primary");
            listKvparameters.Add(kvMyparameters);
            kvMyparameters = new KeyValuePair<string, string>("strSystem", myLogin.propStrSystemUsed);
            listKvparameters.Add(kvMyparameters);
            kvMyparameters = new KeyValuePair<string, string>("intClient_id", myLogin.propIntClient_Id.ToString());
            listKvparameters.Add(kvMyparameters);
            kvMyparameters = new KeyValuePair<string, string>("strClient_SessionField", myLogin.propStrClient_SessionField);
            listKvparameters.Add(kvMyparameters);
            #endregion
            string xmlParsingOutput = await ohttpViewModel.RESTcalls_POST_BXPAPI(myLogin.propStrFunctionURL, listKvparameters);
            return fn_parsePrimaryConfigXML(xmlParsingOutput);
        }

        //Prepare Post call for the set of buttons. Cycles through all buttons
        public async Task fn_retrieveSettingsbuttonData()
        {
            Login myLogin = Login.Instance;
            HTTPRestViewModel ohttpViewModel = new HTTPRestViewModel();
            for (int i = 0; i < 10; i++)
            {
                #region ParametersForPost
                List<KeyValuePair<string, string>> listKvparameters = new List<KeyValuePair<string, string>>();
                KeyValuePair<string, string> kvMyparameters = new KeyValuePair<string, string>("strFunction", "config_button" + i);
                listKvparameters.Add(kvMyparameters);
                kvMyparameters = new KeyValuePair<string, string>("strSystem", myLogin.propStrSystemUsed);
                listKvparameters.Add(kvMyparameters);
                kvMyparameters = new KeyValuePair<string, string>("intClient_id", myLogin.propIntClient_Id.ToString());
                listKvparameters.Add(kvMyparameters);
                kvMyparameters = new KeyValuePair<string, string>("strClient_SessionField", myLogin.propStrClient_SessionField);
                listKvparameters.Add(kvMyparameters);
                #endregion
                string a = await ohttpViewModel.RESTcalls_POST_BXPAPI(myLogin.propStrFunctionURL, listKvparameters);
                propObSetting.propstrInterfaceButtons.Add(fn_parseButtonXML(a, i));
            }
        }

        //Parse through XML from primary configuration response
        public Boolean fn_parsePrimaryConfigXML(string xmlToBeParsed)
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
                obSetting.intSystemId = Int32.Parse(xmlSettingsData.Element("intInterface_SystemId").Value);
                obSetting.intInterfaceColumns = Int32.Parse(xmlSettingsData.Element("intInterface_Columns").Value);
                obSetting.RSSTitle = xmlSettingsData.Element("strInterface_RSSTitle").Value;
                obSetting.RSSFeed = xmlSettingsData.Element("strInterface_RSSFeed").Value;
                obSetting.boolInterfaceStoreUsername = Boolean.Parse(xmlSettingsData.Element("strInterface_StoreSystemAndUsername").Value);
                obSetting.strImageLogoUrl = xmlSettingsData.Element("strInterface_Image_LogoURL").Value;
                obSetting.strImageBackground = xmlSettingsData.Element("strInterface_Image_Background").Value;
                obSetting.strFontColors = xmlSettingsData.Element("strInterface_FontColours").Value;
                obSetting.strFontSizes = xmlSettingsData.Element("strInterface_FontFaces").Value;
                obSetting.strFontFaces = xmlSettingsData.Element("strInterface_FontSizes").Value;
                obSetting.keywords = xmlSettingsData.Element("strInterface_SystemKeywords").Value;


                count = true;
            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
            return count;
        }

        //Parse through XML from button configuration response
        public Button fn_parseButtonXML(string strSettingXML, int intButNum)
        {
            XDocument xmlButtonConfig = XDocument.Parse(strSettingXML);
            var xmlSettingsData = xmlButtonConfig.Element("data");
            string button = "strInterface_Button";
            Button strButton = new Button();
            button = button + intButNum;
            strButton.strInterfaceButtonTitle = xmlSettingsData.Element(button + "_Title").Value;
            strButton.strInterfaceButtonStyle = xmlSettingsData.Element(button + "_Styling").Value;
            strButton.strInterfaceButtonLayout = xmlSettingsData.Element(button + "_Layout").Value;
            strButton.strInterfaceButtonFunctionType = xmlSettingsData.Element(button + "_FunctionType").Value;
            strButton.strInterfaceButtonAPICall = xmlSettingsData.Element(button + "_APICall").Value;
            strButton.strInterfaceButtonConfig = xmlSettingsData.Element(button + "_Config").Value;
            strButton.strInterfaceButtonURL = xmlSettingsData.Element(button + "_URL").Value;
            strButton.strInterfaceParameters = xmlSettingsData.Element(button + "_Parameters").Value;
            return strButton;
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
