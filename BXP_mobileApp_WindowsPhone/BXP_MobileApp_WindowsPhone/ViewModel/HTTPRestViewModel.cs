using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace BXP_MobileApp_WindowsPhone.ViewModel
{

    /*this class controls the RESTful HTTP calls to the BXPAPI. */
    class HTTPRestViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public string aString;
        public HTTPRestViewModel() { }

        //Send up a POST call to the specified URL
        public async Task<string> RESTcalls_POST_BXPAPI(string myfunction, List<KeyValuePair<string, string>> myParameters)
        {
            string output = "";
            try
            {
                HttpClient myClient = new HttpClient();
                var headers = myClient.DefaultRequestHeaders;
                headers.UserAgent.ParseAdd("application/x-www-form-urlencoded");
                HttpFormUrlEncodedContent a = new HttpFormUrlEncodedContent(myParameters);
                Uri uri = new Uri(myfunction);
                HttpRequestMessage postRequest = new HttpRequestMessage(new HttpMethod("POST"), uri);
                postRequest.Content = a;
                HttpResponseMessage response = await myClient.SendRequestAsync(postRequest, HttpCompletionOption.ResponseContentRead);
                if (response.IsSuccessStatusCode)
                    aString = "Success";
                else
                    aString = "fail";

                output = await response.Content.ReadAsStringAsync();

            }
            catch (Exception)
            {
                return output = "N/A";
            }
            return output;
        }

        public async Task RESTcalls_GET_BXPAPI(string output, string strmyFunction)
        {
            try
            {
                HttpClient myClient = new HttpClient();
                var Uri = new Uri(strmyFunction);
                //Send a get request to pull down information in format.
                var response = await myClient.GetStringAsync(Uri);

                output = response;
            }
            catch (Exception e)
            {
                e.Message.ToString();
                e.Source.ToString();
                e.HelpLink.ToString();
            }
        }

        public async Task RESTcalls_GET_BXPAPI_with_parameters(string output, string strmyFunction, List<KeyValuePair<string, string>> myParameters)
        {
            try
            {
                HttpClient myClient = new HttpClient();
                var uri = new Uri(strmyFunction);
                var encodedContent = new HttpFormUrlEncodedContent(myParameters);
                HttpRequestMessage httpGetRequest = new HttpRequestMessage(new HttpMethod("GET"), uri);
                httpGetRequest.Content = encodedContent;
                HttpResponseMessage getWithParametersResponse = await myClient.SendRequestAsync(httpGetRequest, HttpCompletionOption.ResponseContentRead);
                if (getWithParametersResponse.IsSuccessStatusCode)
                    aString = "Success!";
                else
                    aString = "Failure!";

                output = await getWithParametersResponse.Content.ReadAsStringAsync();
                return;
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
