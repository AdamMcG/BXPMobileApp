using System;
using BXP_MobileApp_WindowsPhone.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BXP_MobileApp_WindowsPhone.ViewModel
{
    class ticketViewModel
    {
        public ticketViewModel() { }

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
    }
}
