using BXP_MobileApp_WindowsPhone.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BXP_MobileApp_WindowsPhone.ViewModel
{
    class HamsterViewModel
    {

        private HamsterList HamsterList = new HamsterList();
        public HamsterList propHamsterList
        {
            get { return HamsterList; }
            set { HamsterList = value; }
        }

        //POST to API URL for retieval of Hamsters
        public async Task fn_PostCallForHamsterXMLRetrieval(){
            HTTPRestViewModel oHttpClient = new HTTPRestViewModel();
            Login login = Login.Instance;
            #region Parameters
            List<KeyValuePair<string, string>> colKVParams = new List<KeyValuePair<string, string>>();
            addingParamsToList(login, colKVParams);
            #endregion
            fn_ParseThroughHamsterXML(await oHttpClient.RESTcalls_POST_BXPAPI(login.propStrFunctionURL, colKVParams));

        }

        //Add parameters to your HTTP Request packet for Post call
        private static void addingParamsToList(Login login, List<KeyValuePair<string, string>> colKVParams)
        {
            KeyValuePair<string, string> kvParameters = new KeyValuePair<string, string>("strFunction", "list_hamsters");
            colKVParams.Add(kvParameters);
            kvParameters = new KeyValuePair<string, string>("strSystem", login.propStrSystemUsed);
            colKVParams.Add(kvParameters);
            kvParameters = new KeyValuePair<string, string>("intClient_Id", login.propIntClient_Id.ToString());
            colKVParams.Add(kvParameters);
            kvParameters = new KeyValuePair<string, string>("strClient_SessionField", login.propStrClient_SessionField);
            colKVParams.Add(kvParameters);
        }

        //Parse through Post Call response XML
        public void fn_ParseThroughHamsterXML(string xmldocumentation)
        {
            XElement hamsterXML = XDocument.Parse(xmldocumentation).Element("data");
            propHamsterList.propStrFunction = hamsterXML.Element("strFunction").Value;
            propHamsterList.propIntErrorId = Int32.Parse(hamsterXML.Element("intErrorId").Value);
            propHamsterList.propStrError = hamsterXML.Element("strError").Value;
            var hamsterItemInXml = hamsterXML.Element("hamsters");
            KeyValuePair<string, int> hamsterItem = new KeyValuePair<string, int>();
            fn_assignItemsToHamsterCollection(hamsterItemInXml, ref hamsterItem);

        }

        //Add Hamster items to the Hamster List.
        private void fn_assignItemsToHamsterCollection(XElement hamsterItemInXml, ref KeyValuePair<string, int> hamsterItem)
        {
            List<string> strListOfHamsters = new List<string>(new string[] { "green", "jade", "emerald", "apple", "help", "white", "olive", "lime", "suggestion", "newUser", "excellent",
                "good", "poor","awful"});

            foreach (var item in strListOfHamsters)
                HamsterList.propColListOfHamsters.Add(new Hamster(hamsterItemInXml.Element(item).Name.ToString(), Int32.Parse(hamsterItemInXml.Element(item).Value)));
        }

    }
}
