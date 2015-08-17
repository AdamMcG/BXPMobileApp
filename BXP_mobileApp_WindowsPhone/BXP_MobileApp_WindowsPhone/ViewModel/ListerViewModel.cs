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
using System.ComponentModel;

namespace BXP_MobileApp_WindowsPhone.ViewModel
{
    class ListerViewModel : INotifyPropertyChanged
    {
        public ListerViewModel()
        {

        }

        private Lister myLister = new Lister();
        public Lister propMyLister
        {
            get { return myLister; }
            set
            {
                myLister = value;
                NotifyPropertyChanged("propMyLister");
            }
        }
        #region ElementsForParsing
        List<string> elementsList = new List<string>(new string[] { "intLister_Id", "intLister_ClientId", "strLister_Complete", "strLister_Personal", "dteLister_CompleteDate", "strLister_Title", "strLister_Description", "strLister_Deadline", "strLister_Category", "intLister_LinkId1", "intLister_LinkId2", "intLister_LinkId3", "intLister_LinkId4", "strLister_eCourseRef", "intLister_FromId", "strLister_FromDate", "strLister_FromSource", "intLister_ProjectId", "intLister_MeetingId", "intLister_MeetingActionPointId"});
#endregion

        //This is for pulling down the total Set of listees.
        public async Task fn_POSTToServerForAllListees(string day, string strFunctionToCall)
        {
            Login login = Login.Instance;
            HTTPRestViewModel oHttpViewModel = new HTTPRestViewModel();
            List<KeyValuePair<string, string>> kvListOfParameters = new List<KeyValuePair<string, string>>();

            if (strFunctionToCall.Equals("list_listee_incomplete"))
                fn_addingParametersFortotalToDoList(login, kvListOfParameters);
            else if (strFunctionToCall.Equals("list_listee_due"))
                fn_addingParametersForDayToDoList(day, login, kvListOfParameters);


            fn_parsingListeeList(await oHttpViewModel.RESTcalls_POST_BXPAPI(login.propStrFunctionURL, kvListOfParameters));
        }

        public async Task<Boolean> fn_POSTToServerInsert(string strtitle)
        {
            Login login = Login.Instance;
            HTTPRestViewModel oHttpViewModel = new HTTPRestViewModel();
            List<KeyValuePair<string, string>> kvListOfParameters = new List<KeyValuePair<string, string>>();
            fn_addingParametersForInsert(login, strtitle, kvListOfParameters);
            string check = await oHttpViewModel.RESTcalls_POST_BXPAPI(login.propStrFunctionURL, kvListOfParameters);
            if (check.Equals("N/A"))
            {
                return false;
            }
           return true;
        }

        private static void fn_addingParametersForInsert(Login login, string strTitle ,List<KeyValuePair<string, string>> kvListOfParameters)
        {
            KeyValuePair<string, string> kvParameters = new KeyValuePair<string, string>("strFunction", "insert_listee");
            kvListOfParameters.Add(kvParameters);
            kvParameters = new KeyValuePair<string, string>("strSystem", login.propStrSystemUsed);
            kvListOfParameters.Add(kvParameters);
            kvParameters = new KeyValuePair<string, string>("intClient_id", login.propIntClient_Id.ToString());
            kvListOfParameters.Add(kvParameters);
            kvParameters = new KeyValuePair<string, string>("strClient_SessionField", login.propStrClient_SessionField);
            kvListOfParameters.Add(kvParameters);
            kvParameters = new KeyValuePair<string, string>("strTitle", strTitle);
            kvListOfParameters.Add(kvParameters);
        }


        private static void fn_addingParametersFortotalToDoList(Login login, List<KeyValuePair<string, string>> kvListOfParameters)
        {
            KeyValuePair<string, string> kvParameters = new KeyValuePair<string, string>("strFunction", "list_listee_incomplete");
            kvListOfParameters.Add(kvParameters);
            kvParameters = new KeyValuePair<string, string>("strSystem", login.propStrSystemUsed);
            kvListOfParameters.Add(kvParameters);
            kvParameters = new KeyValuePair<string, string>("intClient_id", login.propIntClient_Id.ToString());
            kvListOfParameters.Add(kvParameters);
            kvParameters = new KeyValuePair<string, string>("strClient_SessionField", login.propStrClient_SessionField);
            kvListOfParameters.Add(kvParameters);
        }

        private static void fn_addingParametersForDayToDoList(string day, Login login, List<KeyValuePair<string, string>> kvListOfParameters)
        {
            KeyValuePair<string, string> kvParameters = new KeyValuePair<string, string>("strFunction", "list_listee_due");
            kvListOfParameters.Add(kvParameters);
            kvParameters = new KeyValuePair<string, string>("strSystem", login.propStrSystemUsed);
            kvListOfParameters.Add(kvParameters);
            kvParameters = new KeyValuePair<string, string>("intClient_id", login.propIntClient_Id.ToString());
            kvListOfParameters.Add(kvParameters);
            kvParameters = new KeyValuePair<string, string>("strClient_SessionField", login.propStrClient_SessionField);
            kvListOfParameters.Add(kvParameters);
            kvParameters = new KeyValuePair<string, string>("strWhen", day);
            kvListOfParameters.Add(kvParameters);
        }

        public void fn_parsingListeeList(string strXMLToParse)
        {

            XElement recordElement = XDocument.Parse(strXMLToParse, LoadOptions.PreserveWhitespace).Element("data");
            propMyLister.strFunction = recordElement.Element("strFunction").Value;
            propMyLister.intError = Int32.Parse(recordElement.Element("intErrorId").Value);
            propMyLister.strError = recordElement.Element("strError").Value;
            fn_pullingFromParseForEachListeeItem(recordElement);
        }

        private void fn_pullingFromParseForEachListeeItem(XElement recordElement)
        {
            var collectionOfElements = recordElement.Elements("item");
            foreach (var item in collectionOfElements)
            {
                Listee ListeetoAdd = new Listee();
                ListeetoAdd.intLister_id = int.Parse(item.Element(elementsList[0]).Value);
                ListeetoAdd.intListerClient_id = int.Parse(item.Element(elementsList[1]).Value);
                ListeetoAdd.strListerComplete = item.Element(elementsList[2]).Value;
                ListeetoAdd.strListerPersonal = item.Element(elementsList[3]).Value;
                ListeetoAdd.dteListerDate = DateTime.Parse(item.Element(elementsList[4]).Value);
                ListeetoAdd.strListerTitle = item.Element(elementsList[5]).Value;
                ListeetoAdd.strListerDescription = item.Element(elementsList[6]).Value;
                ListeetoAdd.strListerDeadline = item.Element(elementsList[7]).Value;
                ListeetoAdd.strListerCatagory = item.Element(elementsList[8]).Value;
                ListeetoAdd.intLister_linkId1 = int.Parse(item.Element(elementsList[9]).Value);
                ListeetoAdd.intLister_linkid2 = int.Parse(item.Element(elementsList[10]).Value);
                ListeetoAdd.intLister_linkid3 = int.Parse(item.Element(elementsList[11]).Value);
                ListeetoAdd.intLister_linkid4 = int.Parse(item.Element(elementsList[12]).Value);
                ListeetoAdd.strListerEcourseID = item.Element(elementsList[13]).Value;
                ListeetoAdd.intListerFromId = int.Parse(item.Element(elementsList[14]).Value);
                ListeetoAdd.strLister_fromDate = item.Element(elementsList[15]).Value;
                ListeetoAdd.strLister_fromSource = item.Element(elementsList[16]).Value;
                ListeetoAdd.intListerProjectId = int.Parse(item.Element(elementsList[17]).Value);
                ListeetoAdd.intListerMeetingId = int.Parse(item.Element(elementsList[18]).Value);
                ListeetoAdd.intListerMeetingActionId = int.Parse(item.Element(elementsList[19]).Value);
                propMyLister.pColTodolist.Add(ListeetoAdd);
            }
        }


        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}


