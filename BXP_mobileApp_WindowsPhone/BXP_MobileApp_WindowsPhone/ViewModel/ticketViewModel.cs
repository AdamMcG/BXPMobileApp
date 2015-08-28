using System;
using BXP_MobileApp_WindowsPhone.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using BXP_MobileApp_WindowsPhone.Model;
using Windows.UI.Popups;

namespace BXP_MobileApp_WindowsPhone.ViewModel
{
    class ticketViewModel : INotifyPropertyChanged
    {
        Login myLogin = Login.Instance;
        public ticketViewModel() { }

        private static string strSubject;
        public string propStrSubject
        {
            get { return strSubject; }
            set { strSubject = value;
            NotifyPropertyChanged("propStrSubject");
            }
        }

        private static string strDescription;
        public string propStrDescription
        {
            get { return strDescription; }
            set { strDescription = value;
            NotifyPropertyChanged("propStrDescription");
            }
        }

        //Push a record into the campaign
        public async Task fnInjectContactRecord(string fieldToInjectSubject, string fieldToInjectContentDescription, int formToLookup)
        {
            HTTPRestViewModel ohttp = new HTTPRestViewModel();
           string subjectBox =  "strCDA_" + formToLookup + "_field_0_1";
            string descriptionBox = "strCDA_" + formToLookup + "_field_0_2";
            #region Parameters
            List<KeyValuePair<String, string>> parameters = new List<KeyValuePair<string, string>>();
            KeyValuePair<string, string> parameter = new KeyValuePair<string, string>("strFunction", "insert_formrecord");
            parameters.Add(parameter);
            parameter = new KeyValuePair<string, string>("strSystem", myLogin.propStrSystemUsed);
            parameters.Add(parameter);
            parameter = new KeyValuePair<string, string>("intClient_Id", myLogin.propIntClient_Id.ToString());
            parameters.Add(parameter);
            parameter = new KeyValuePair<string, string>("strClient_SessionField", myLogin.propStrClient_SessionField);
            parameters.Add(parameter);
            parameter = new KeyValuePair<string, string>("intCampaign_Id", formToLookup.ToString());
            parameters.Add(parameter);
            parameter = new KeyValuePair<string, string>("strSearch_Field", subjectBox + "[[--SEP--]]" + descriptionBox);
            parameters.Add(parameter);
            parameter = new KeyValuePair<string, string>("strSearch_Value", fieldToInjectSubject + "[[--SEP--]]" + fieldToInjectContentDescription);
            parameters.Add(parameter);
            parameter = new KeyValuePair<string, string>("strReturn_Fields", "intCDA_" + formToLookup+ "_Id");
            parameters.Add(parameter);
            #endregion
            string output = await ohttp.RESTcalls_POST_BXPAPI(myLogin.propStrFunctionURL, parameters);
            if (output.Contains("<intErrorId>0</intErrorId>"))
            {
                MessageDialog myMessage = new MessageDialog("Successfully added a ticket!");
               await myMessage.ShowAsync();
            }
            return;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propetyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propetyName));
            }
        }
    }
}
