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
        string returnField;
        string returnField2;
        string returnField3;
        string returnField4;
        public CampaignViewModel() { }
        private Campaign myCampaign = new Campaign();
        public Campaign propMyCampaign
        {
            get { return myCampaign; }
            set
            {
                myCampaign = value;
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
            strcontactToSearchFor = strcontactToSearchFor.TrimEnd(' ');
            HTTPRestViewModel oHttpRestViewModel = new HTTPRestViewModel();
            returnField = "strCDA_" + formToLookup + "_field_1_15";
            returnField2 = "strCDA_" + formToLookup + "_field_3_15";
            string searchfield = returnField + "[[--SEP--]]" + returnField2;
            returnField3 = "strCDA_" + formToLookup + "_field_0_4";
            returnField4 = "strCDA_" + formToLookup + "_field_0_6";
            string strSearchValue1;
            string strSearchValue2;
            string searchValues = strcontactToSearchFor;
            if (strcontactToSearchFor.Contains(" ")){
                string[] words = strcontactToSearchFor.Split(' ');
                strSearchValue1 = words[0];
                strSearchValue2 = words[1];
                if (!words[1].Equals(" "))
                {
                    searchValues = strSearchValue1 + "[[--SEP--]]" + strSearchValue2;
                    await fnSearchByFirstAndLastName(formToLookup, oHttpRestViewModel, searchfield, searchValues);
                    return;
                }
            }

            await fnsearchByFirstname(formToLookup, searchValues, oHttpRestViewModel);
            if (propMyCampaign.listOfCampaignItems.Count == 0)
            {
                await SearchByLastName(formToLookup, searchValues, oHttpRestViewModel);
            }
            return;
        }

        //Searches a refined searched based on first and Last name.
        private async Task fnSearchByFirstAndLastName(int formToLookup, HTTPRestViewModel oHttpRestViewModel, string searchfield, string searchValues)
        {
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
            parameter = new KeyValuePair<string, string>("strSearch_Field", searchfield);
            kvHttpRequestParameters.Add(parameter);
            parameter = new KeyValuePair<string, string>("strSearch_Value", searchValues);
            kvHttpRequestParameters.Add(parameter);
            parameter = new KeyValuePair<string, string>("strReturn_Fields", returnField + "," + returnField2 + "," + returnField3 + "," + returnField4);
            kvHttpRequestParameters.Add(parameter);
            #endregion
            string output = await oHttpRestViewModel.RESTcalls_POST_BXPAPI(myLogin.propStrFunctionURL, kvHttpRequestParameters);
            fnParseContactXMLDocument(output);
        }

        //Searches through campaign DB based on First Name.
        private async Task fnsearchByFirstname(int formToLookup, string searchfield, HTTPRestViewModel oHttpRestViewModel)
        {
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
            parameter = new KeyValuePair<string, string>("strSearch_Field", returnField);
            kvHttpRequestParameters.Add(parameter);
            parameter = new KeyValuePair<string, string>("strSearch_Value", searchfield);
            kvHttpRequestParameters.Add(parameter);
            parameter = new KeyValuePair<string, string>("strReturn_Fields", returnField + "," + returnField2 + "," + returnField3 + "," + returnField4);
            kvHttpRequestParameters.Add(parameter);
            #endregion

            string output = await oHttpRestViewModel.RESTcalls_POST_BXPAPI(myLogin.propStrFunctionURL, kvHttpRequestParameters);
            fnParseContactXMLDocument(output);
        }

        //Searches through Campaign DB based on Lastname
        private async Task SearchByLastName(int formToLookup, string searchValues, HTTPRestViewModel oHttpRestViewModel)
        {
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
            parameter = new KeyValuePair<string, string>("strSearch_Field", returnField2);
            kvHttpRequestParameters.Add(parameter);
            parameter = new KeyValuePair<string, string>("strSearch_Value", searchValues);
            kvHttpRequestParameters.Add(parameter);
            parameter = new KeyValuePair<string, string>("strReturn_Fields", returnField + "," + returnField2 + "," + returnField3 + "," + returnField4);
            kvHttpRequestParameters.Add(parameter);
            #endregion

            string output = await oHttpRestViewModel.RESTcalls_POST_BXPAPI(myLogin.propStrFunctionURL, kvHttpRequestParameters);
            fnParseContactXMLDocument(output);
        }

        //Parse through the list of contacts in the xml document
        public void fnParseContactXMLDocument(string fileContactXmlDocument)
        {
            if (propMyCampaign.listOfCampaignItems.Count > 0)
                propMyCampaign.listOfCampaignItems.Clear();

            XDocument campaignXMLdocument = XDocument.Parse(fileContactXmlDocument);
            var campaignXmlData = campaignXMLdocument.Element("data");
            var campaignItems = campaignXmlData.Elements("item");
            foreach (var item in campaignItems)
            {
                CampaignItem CampITem = new CampaignItem();
                CampITem.propCampaignItems.Add(item.Element("intId").Value);
                string firstname = item.Element(returnField).Value;
                CampITem.propCampaignItems.Add(firstname + " " + item.Element(returnField2).Value);
                CampITem.propCampaignItems.Add("Work: " + item.Element(returnField3).Value);
                CampITem.propCampaignItems.Add("Email: " + item.Element(returnField4).Value);
                propMyCampaign.listOfCampaignItems.Add(CampITem);
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
