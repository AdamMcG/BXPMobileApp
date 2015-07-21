using BXP_MobileApp_WindowsPhone.Model;
using BXP_MobileApp_WindowsPhone.ViewModel;
using Windows.Data.Xml.Dom;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace BXP_MobileApp_WindowsPhone.ViewModel
{
    class ListeeViewModel
    {
        private ObservableCollection<ToDo> colTodoList;
        public ObservableCollection<ToDo> pColTodolist
        {
            get { return colTodoList; }
            set { colTodoList = value; }
        }

        public async Task fnGetListees()
        {//Supply the http string for get request.
            string uGetToDoListItems = "https://ww3.allnone.ie/client/client_allnone/cti/userCTI_XML_AppFunctions.asp";
           //Initialise httpViewModelObject. 
            HTTPRestViewModel oHttpViewModel = new HTTPRestViewModel();
            string myHttpResponse = "";
           //Call the BXP_Get method to pull down XML.
            await oHttpViewModel.RESTcalls_GET_BXPAPI(myHttpResponse, uGetToDoListItems);
            //For use of Windows.data.xml.dom, a storage file is needed. 
            StorageFile todoXmlDocument = await ApplicationData.Current.LocalFolder.CreateFileAsync("ToDo.xml");
            //Write to that file. 
            await FileIO.WriteTextAsync(todoXmlDocument, myHttpResponse);
            //Call the parsing method to parse through the XML string. This method may change. 
            await fnparsingListeeList(todoXmlDocument);
        }

        public async Task fnparsingListeeList(StorageFile strXMLToParse)
        {
            var toDoXML = await XmlDocument.LoadFromFileAsync(strXMLToParse);
      
        }
    }
}
