using BXP_MobileApp_WindowsPhone.Model;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.Storage;
using System;

namespace BXP_MobileApp_WindowsPhone.ViewModel
{
    class CampaignViewModel : INotifyPropertyChanged
    {
        Login myLogin = Login.Instance;
        public CampaignViewModel() { }
        private Campaign myCampaign;
        public Campaign propMyCampaign
        {
            get { return myCampaign; }
            set { myCampaign = value;
            NotifyPropertyChanged("propMyCampaign");
            }
        }
        private static string strRetainContact = "";
        public string propStrRetainContact
        {
            get { return strRetainContact; }
            set
            {
                strRetainContact = value;
                NotifyPropertyChanged("propStrRetainContact");
            }
        }

        //Pull down the list of contacts under a particular search name
        public async Task fnGetContactList(string strcontactToSearchFor, int formToLookup)
        {
            string strSearchValue1 = strcontactToSearchFor;
            string strSearchValue2 = "";
            if (strcontactToSearchFor.Contains(" "))
            {
                string[] words = strcontactToSearchFor.Split(' ');
                strSearchValue1 = words[0];
                strSearchValue2 = words[1];
            }
            HTTPRestViewModel oHttpRestViewModel = new HTTPRestViewModel();

            #region Parameters
            List<KeyValuePair<string, string>> kvHttpRequestParameters = new List<KeyValuePair<string, string>>();
            KeyValuePair<string, string> parameter = new KeyValuePair<string, string>("strFunction", "formlookup");
            kvHttpRequestParameters.Add(parameter);
            parameter = new KeyValuePair<string, string>("strSystem", myLogin.propStrSystemUsed);
            kvHttpRequestParameters.Add(parameter);
            parameter = new KeyValuePair<string, string>("intClient_Id", myLogin.propIntClient_Id.ToString());
            kvHttpRequestParameters.Add(parameter);
            parameter = new KeyValuePair<string, string>("strClient_SessionField", myLogin.propStrClient_SessionField);
            kvHttpRequestParameters.Add(parameter);
            parameter = new KeyValuePair<string, string>("intCampaign_Id", formToLookup.ToString());
            kvHttpRequestParameters.Add(parameter);
            parameter = new KeyValuePair<string, string>("strSearch_Field", "strCDA_" + formToLookup + "_field_1_15");
            kvHttpRequestParameters.Add(parameter);
            parameter = new KeyValuePair<string, string>("strSearch_Value", strSearchValue1);
            kvHttpRequestParameters.Add(parameter);
            parameter = new KeyValuePair<string, string>("strReturn_Fields", "strCDA_" + formToLookup + "_field_3_15");
            kvHttpRequestParameters.Add(parameter);
            #endregion

            string output = await oHttpRestViewModel.RESTcalls_POST_BXPAPI(myLogin.propStrFunctionURL, kvHttpRequestParameters);
            fnParseContactXMLDocument(output);
            return;
        }

        //Push a record into the campaign
        public async void fnInjectContactRecord(string fieldToInjectContent, int formToLookup)
        {
            HTTPRestViewModel ohttp = new HTTPRestViewModel();
            #region Parameters
            List<KeyValuePair<String, string>> parameters = new List<KeyValuePair<string, string>>();
            KeyValuePair<string, string> parameter = new KeyValuePair<string, string>("strFunction", "insertformrecord");
            parameters.Add(parameter);
            parameter = new KeyValuePair<string, string>("strSystem", myLogin.propStrSystemUsed);
            parameters.Add(parameter);
            parameter = new KeyValuePair<string, string>("intClient_Id", myLogin.propIntClient_Id.ToString());
            parameters.Add(parameter);
            parameter = new KeyValuePair<string, string>("strClient_SessionField", myLogin.propStrClient_SessionField);
            parameters.Add(parameter);
            parameter = new KeyValuePair<string, string>("intCampaign_Id", formToLookup.ToString());
            parameters.Add(parameter);
            parameter = new KeyValuePair<string, string>("strSearch_Field", "strCDA_" + formToLookup + "_0_1");
            parameters.Add(parameter);
            parameter = new KeyValuePair<string, string>("strSearch_Value", fieldToInjectContent);
            parameters.Add(parameter);
            parameter = new KeyValuePair<string, string>("strReturn_Fields", "strCDA_" + formToLookup + "_0_1");
            parameters.Add(parameter);
            #endregion
            string output = await ohttp.RESTcalls_POST_BXPAPI(myLogin.propStrFunctionURL, parameters);
            return;
        }

        //Parse through the list of contacts in the xml document
        public void fnParseContactXMLDocument(string fileContactXmlDocument)
        {
            XDocument campaignXMLdocument = XDocument.Parse(fileContactXmlDocument);
            var campaignXmlData = campaignXMLdocument.Element("data").Elements();
            foreach (var item in campaignXmlData)
            {
                Campaign myCampaignItem = new Campaign();

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
