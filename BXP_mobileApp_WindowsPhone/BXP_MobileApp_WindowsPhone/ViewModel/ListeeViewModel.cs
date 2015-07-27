using BXP_MobileApp_WindowsPhone.Model;
using BXP_MobileApp_WindowsPhone.ViewModel;
using System.Xml.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Media;

namespace BXP_MobileApp_WindowsPhone.ViewModel
{
    class ListeeViewModel
    {
        public ListeeViewModel()
        {
            try
            {

                for (int i = 0; i < 20; i++)
                {
                    ToDo mytest = new ToDo();
                    mytest.strTodosubject = mytest.strTodosubject + ": " + i;
                    mytest.styleMyStyle.brusTextForeground = new SolidColorBrush(Windows.UI.Colors.BlueViolet);
                    pColTodayToDoList.Add(mytest);
                }
            }
            catch (Exception e)
            {
                
                e.Message.ToString();
            }
        }

        private ObservableCollection<ToDo> colTodoList = new ObservableCollection<ToDo>();
        private ObservableCollection<ToDo> colTomorrowToDoList = new ObservableCollection<ToDo>();
        private ObservableCollection<ToDo> colTodayToDoList = new ObservableCollection<ToDo>();

        public ObservableCollection<ToDo> pColTodolist
        {
            get { return colTodoList; }
            set { colTodoList = value; }
        }
        public ObservableCollection<ToDo> pColTomorrowToDoList
        {
            get { return colTomorrowToDoList; }
            set { colTomorrowToDoList = value; }
        }
        public ObservableCollection<ToDo> pColTodayToDoList
        {
            get { return colTodayToDoList; }
            set { colTodayToDoList = value; }

        }

        //This is for pulling down the total Set of listees.
        public async Task fnGetListees()
        {
            string uGetToDoListItems = "https://ww3.allnone.ie/client/client_allnone/cti/userCTI_XML_AppFunctions.asp";
            HTTPRestViewModel oHttpViewModel = new HTTPRestViewModel();
            string myHttpResponse = "";
            List<KeyValuePair<string, string>> kvListOfParameters = new List<KeyValuePair<string, string>>();

            KeyValuePair<string, string> kvMyParameter = new KeyValuePair<string, string>("strFunction",
                "GetSessionId, ToDo_today,Diary_Today,Diary_Tomorrow,Diary_ThisWeek");
            kvListOfParameters.Add(kvMyParameter);
            kvMyParameter = new KeyValuePair<string, string>("strUsername", "abc123");
            kvListOfParameters.Add(kvMyParameter);
            kvMyParameter = new KeyValuePair<string, string>("strPassword", "abc123");
            kvListOfParameters.Add(kvMyParameter);

            await oHttpViewModel.RESTcalls_GET_BXPAPI_with_parameters
                (myHttpResponse, uGetToDoListItems, kvListOfParameters);

            fnparsingListeeList(myHttpResponse);
            return;
        }

        //This is for parsing the listee Xml File.
        public void fnparsingListeeList(string strXMLToParse)
        {

            XElement recordElement = XDocument.Parse(strXMLToParse, LoadOptions.PreserveWhitespace).Root.Element("data");
            var collectionOfElements = recordElement.Elements("record");

            foreach (XElement record in collectionOfElements)
            {
                string id = record.Element("intId").Value;
                int intId = Int32.Parse(id);
                string type = record.Element("strType").Value;
                string from = record.Element("dtefrom").Value;
                DateTime dteFrom = DateTime.Parse(from);
                string to = record.Element("dteto").Value;
                DateTime dteTo = DateTime.Parse(to);
                string strSubject = record.Element("strSubject").Value;
                string strBody = record.Element("strBody").Value;
                string strLink = record.Element("strLink").Value;
                string strData = record.Element("strData").Value;
                ToDo xmlObject = new ToDo(intId, type, dteFrom, dteTo, strSubject, strBody, strLink, strData);

                if (dteTo == DateTime.Now)
                {
                    pColTodolist.Add(xmlObject);
                    pColTodayToDoList.Add(xmlObject);
                }
                else if (dteTo == DateTime.Now.AddDays(1))
                {
                    pColTomorrowToDoList.Add(xmlObject);
                    pColTodolist.Add(xmlObject);
                }
                else
                    pColTodolist.Add(xmlObject);

            }
        }

        //This is to post a new listee up to the BXPAPI system.
        public void fnPostingNewListeeToSystem(string ListeeToAdd)
        {

        }
    }
}
