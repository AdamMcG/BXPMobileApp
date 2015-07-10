using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace BXP_MobileApp_WindowsPhone.ViewModel
{
    class HTTPRestViewModel : INotifyPropertyChanged
    {
         public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public string myCheckString ="11111";
        public string aString;

        public string myString
        {
            get { return myCheckString; }
            set { myCheckString = value;

            NotifyPropertyChanged("myString");
            }
        }

        public HTTPRestViewModel(){}



       public async Task RESTcalls_POST_BXPAPI(string myfunction, List<KeyValuePair<string, string>> myParameters)
       {
           try
           {   
               HttpClient myClient = new HttpClient();
               var Uri = new Uri(myfunction);
 
               //for POST:
               //Set header
               var headers = myClient.DefaultRequestHeaders;
               headers.UserAgent.ParseAdd("application/x-www-form-urlencoded");
               //Attached encoded parameter content
               HttpFormUrlEncodedContent a = new HttpFormUrlEncodedContent(myParameters);

               //Submit a POST request with the encoded contnt a
               HttpRequestMessage postRequest = new HttpRequestMessage(new HttpMethod("POST"), Uri);
               postRequest.Content = a;
               //Return ResponseMessage
               HttpResponseMessage response = await myClient.SendRequestAsync(postRequest, HttpCompletionOption.ResponseContentRead);
               if (response.IsSuccessStatusCode)
                   aString = "Success";
               else
                   aString = "fail";
               //Read Response as string
               myString = await response.Content.ReadAsStringAsync();

               return;

           }
           catch (Exception e)
           {

               e.Message.ToString();
               e.Source.ToString();
               e.HelpLink.ToString();
           }
       }

       public async Task RESTcalls_PUT_BXPAPI(string strmyFunction, List<KeyValuePair<string, string>> myParameters)
       {
           try
           {
               HttpClient myClient = new HttpClient();
               var Uri = new Uri(strmyFunction);
               //Send a get request to pull down information in format.
               var content = new HttpFormUrlEncodedContent(myParameters);
               HttpRequestMessage putRequest = new HttpRequestMessage(new HttpMethod("PUT"), Uri);
               putRequest.Content = content;

               HttpResponseMessage putResponse = await myClient.SendRequestAsync(putRequest, HttpCompletionOption.ResponseContentRead);
               if (putResponse.IsSuccessStatusCode)
                   aString = "Success!";
               else
                   aString = "Fail!";
               myString = await putResponse.Content.ReadAsStringAsync();
               return;
           }
           catch (Exception e)
           {
               e.Message.ToString();
               e.Source.ToString();
               e.HelpLink.ToString();
           }
       }

       public async Task RESTcalls_GET_BXPAPI(string strmyFunction)
       {
           try
           {
               HttpClient myClient = new HttpClient();
               var Uri = new Uri(strmyFunction);
               //Send a get request to pull down information in format.
               var response = await myClient.GetStringAsync(Uri);
               var responseText = await myClient.GetAsync(Uri);
           }
           catch (Exception e)
           {
              e.Message.ToString();
              e.Source.ToString();
              e.HelpLink.ToString();
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
        
    }
}
