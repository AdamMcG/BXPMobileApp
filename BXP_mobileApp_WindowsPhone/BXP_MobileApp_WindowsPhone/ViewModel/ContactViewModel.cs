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
    class ContactViewModel
    {

        public ContactViewModel() { }

        private ObservableCollection<Contact> colContacts;
        public ObservableCollection<Contact> pColContacts
        {
            get { return colContacts; }
            set { colContacts = value; }
        }

        //Pull down the list of contacts under a particular search name
        public async void fnGetContactList(string strcontactToSearchFor)
        {
            string strAPIFunction = "";
            HTTPRestViewModel oHttpRestViewModel = new HTTPRestViewModel();
            string strXmlDocumentContent = "";
            List<KeyValuePair<string, string>> kvHttpRequestParameters = new List<KeyValuePair<string, string>>();
            KeyValuePair<string, string> kvMyParameter = new KeyValuePair<string, string>("value", strcontactToSearchFor);
            await oHttpRestViewModel.RESTcalls_GET_BXPAPI_with_parameters(strXmlDocumentContent, strAPIFunction, kvHttpRequestParameters);
            fnParseContactXMLDocument(strXmlDocumentContent);
            return;
        }

        //Parse through the list of contacts in the xml document
        public void fnParseContactXMLDocument(string fileContactXmlDocument)
        {
            XElement ContactDataElement = XDocument.Parse(fileContactXmlDocument, LoadOptions.PreserveWhitespace).Root.Element("data");
            var colContactRecordElement = ContactDataElement.Elements("record");

            foreach (var xmlRecordElement in colContactRecordElement)
            {
                string id = xmlRecordElement.Element("intID").Value;
                int intId = Int32.Parse(id);
                //Other elements in the xml file will go here for parsing through
                Contact oContactObject = new Contact();
                pColContacts.Add(oContactObject);
            }
        }
    }
}
