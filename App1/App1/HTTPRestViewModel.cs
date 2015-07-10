using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Windows.Web.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.Data.Xml.Dom;


namespace App1
{
    class HTTPRestViewModel : INotifyPropertyChanged
    {
        public string myCheckString = "11111";
        public string aString = "1223";
        public string myString
        {
            get { return myCheckString; }
            set
            {
                myCheckString = value;

                NotifyPropertyChanged("myString");
            }
        }

        public HTTPRestViewModel()
        {
            myString = "sfgf";
        }

        public async Task RESTcalls_POST_BXPAPI(HttpFormUrlEncodedContent a)
        {
            try
            {
                //Set up client
                HttpClient myClient = new HttpClient();
                //supply the API URI to be called. 
                var Uri = new Uri("https://ww3.allnone.ie/client/client_demo/cti/userCTI_GenericEntry.asp");

                //Set header on the client
                var headers = myClient.DefaultRequestHeaders;
                headers.UserAgent.ParseAdd("application/x-www-form-urlencoded");
                //Attached encoded parameter content

                #region KeyValueParameters

                #endregion


                //Submit a POST request with the encoded content a(This will add the parameters as querystrings to the end,
                HttpRequestMessage putRequest = new HttpRequestMessage(new HttpMethod("POST"), Uri); ;
                putRequest.Content = a;

                //Return ResponseMessage
                HttpResponseMessage response = await myClient.SendRequestAsync(putRequest, HttpCompletionOption.ResponseContentRead);
                if (response.IsSuccessStatusCode)
                    aString = "Success";
                else
                    aString = "fail";
                //Read Response as string

                //Read the returned XML into the textbox if successful, or the encountered hamster if a fail.  
                myString = await response.Content.ReadAsStringAsync();

                return;

            }
            catch (Exception e)
            {

                e.Message.ToString();
            }

        }

        public async Task RESTcalls_GET_BXPAPI()
        {
            try
            {
                //create client to establish connection
                HttpClient myClient = new HttpClient();

                //Add headers to the client packets. 
                var headers = myClient.DefaultRequestHeaders;
                headers.UserAgent.ParseAdd("application/x-www-form-urlencoded");

                //Supply the URI for the API to be called. 
                var Uri =
                    new Uri("https://ww3.allnone.ie/client/client_demo/cti/userCTI_GenericEntry.asp?");

                //create GetRequest message with the method needed.
                HttpRequestMessage getRequest = new HttpRequestMessage(new HttpMethod("GET"), Uri);

                //Create your encoded parameters(insert fields are currently system generated; looking at renaming them to English legiable names i.e. strCDA_751_field_4_10 to "Country"
                List<KeyValuePair<string, string>> getRequestParameters = new List<KeyValuePair<string, string>>();

                #region KeyValueParameters
                getRequestParameters.Add(
                    new KeyValuePair<string, string>("user_id", "adammcgivern"));

                getRequestParameters.Add(
                    new KeyValuePair<string, string>("user_key", "password"));

                getRequestParameters.Add(
                    new KeyValuePair<string, string>("campaignid", "751"));

                getRequestParameters.Add(
                      new KeyValuePair<string, string>("system", "datasearch"));

                getRequestParameters.Add(
                    new KeyValuePair<string, string>("searchfield", "strCDA_751_field_4_10"));

                getRequestParameters.Add(
                    new KeyValuePair<string, string>("value", "1"));

                getRequestParameters.Add(
                    new KeyValuePair<string, string>("responsefields", "intCDA_751_Id,strCDA_751_field_1_1,"
                        + "strCDA_751_field_2_1,"
                + "strCDA_751_field_3_1"));
                #endregion


                //Attach the parameters to the getRequest packet in URLEncodedform(added on at end as queryString)
                HttpFormUrlEncodedContent getContent = new HttpFormUrlEncodedContent(getRequestParameters);
                getRequest.Content = getContent;

                //Request a response message. 
                HttpResponseMessage getResponse = await myClient.SendRequestAsync(getRequest, HttpCompletionOption.ResponseContentRead);

                //Check if response is successful. if not, message "Failure"
                if (getResponse.IsSuccessStatusCode)
                    aString = "Success";
                else
                    aString = "Failure";

                //This will return the correct XML if successful, or the hamster encountered if fails. 
                myString = await getResponse.Content.ReadAsStringAsync();
                return;
            }
            catch (Exception e)
            {
                e.Message.ToString();
                e.Source.ToString();
                e.HelpLink.ToString();
            }
        }

        public async Task RESTcalls_TESTING_BXPAPI(Uri myfunction, HttpFormUrlEncodedContent testRequestContent)
        {
            try{ HttpClient myClient = new HttpClient();

                var headers = myClient.DefaultRequestHeaders;
                headers.UserAgent.ParseAdd("application/x-www-form-urlendcoded");

                Uri apiUri = myfunction;


                HttpRequestMessage testRequest = new HttpRequestMessage(new HttpMethod("GET"), apiUri);
                testRequest.Content = testRequestContent;

                HttpResponseMessage testResponseMessage = await myClient.SendRequestAsync(testRequest, HttpCompletionOption.ResponseContentRead);
                if (testResponseMessage.IsSuccessStatusCode)
                    aString = "Success!";
                else
                    aString = "failure";

                myString = await testResponseMessage.Content.ReadAsStringAsync();

            }
            catch (Exception e)
            {
                e.Message.ToString();
                e.Source.ToString();
                e.HelpLink.ToString();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
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
