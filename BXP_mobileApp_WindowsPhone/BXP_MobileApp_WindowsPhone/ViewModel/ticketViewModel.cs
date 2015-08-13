using System;
using BXP_MobileApp_WindowsPhone.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BXP_MobileApp_WindowsPhone.ViewModel
{
    class ticketViewModel : INotifyPropertyChanged
    {
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


        //pull data from the ticket GUI, post up to the function to add the ticket to the system.
        public async Task fnAddTicketToSystem(string strSubject, string strDescription)
        {
            HTTPRestViewModel ohttpRestViewModel = new HTTPRestViewModel();
            List<KeyValuePair<string, string>> kvMyParameterList = new List<KeyValuePair<string, string>>();
            KeyValuePair<string, string> kvMyParameter = new KeyValuePair<string, string>("subject", strSubject);
            kvMyParameterList.Add(kvMyParameter);
            kvMyParameter = new KeyValuePair<string, string>("description", strDescription);
            kvMyParameterList.Add(kvMyParameter);
            string strXMLDocument = "";
            string strMyFunction = "";
         strXMLDocument = await ohttpRestViewModel.RESTcalls_POST_BXPAPI( strMyFunction, kvMyParameterList);
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
