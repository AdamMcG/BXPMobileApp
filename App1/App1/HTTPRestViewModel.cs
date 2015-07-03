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


namespace App1
{
    class HTTPRestViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public string myCheckString ="11111";
        public string aString = "1223";
        public string myString
        {
            get { return myCheckString; }
            set { myCheckString = value;

            NotifyPropertyChanged("myString");
            }
        }

        public HTTPRestViewModel()
        {
            myString = "sfgf";
        }

       public async Task myHTTP()
        {
            try
            {

                HttpClient myClient = new HttpClient();
                var Uri = new Uri("https://ww3.allnone.ie/client/client_allnone/cti/userCTI_GenericEntry.asp");
                //Send a get request to pull down information in format.
                //var response = await myClient.GetStringAsync(Uri);
                //var responseText = await myClient.GetAsync(Uri);
              
                //for POST:
                //Set header
                var headers = myClient.DefaultRequestHeaders;
                headers.UserAgent.ParseAdd("application/x-www-form-urlencoded");
              //Attached encoded parameter content
            HttpFormUrlEncodedContent a = new HttpFormUrlEncodedContent(
                new[] { 
                new KeyValuePair<string, string>("user_id", "abc123"),
                new KeyValuePair<string, string>("user_key", "PASSKEY"),
                new KeyValuePair<string, string>("campaignid","bxpAPIDemo"),
                new KeyValuePair<string, string>("system", "datalogging")
                });

                //Submit a POST request with the encoded contnt a
                HttpRequestMessage putRequest = new HttpRequestMessage(new HttpMethod("POST"), Uri); ;

                putRequest.Content = a;

                //Return ResponseMessage
                HttpResponseMessage response = await myClient.SendRequestAsync(putRequest, HttpCompletionOption.ResponseContentRead);
                if(response.IsSuccessStatusCode)
                aString = "Success"; 
                else
                    aString = "fail";
                //Read Response as string
                 
                myString = await response.Content.ReadAsStringAsync();
                StorageFolder folder = ApplicationData.Current.LocalFolder;
                StorageFile File = await folder.CreateFileAsync("test.xml", CreationCollisionOption.ReplaceExisting);
                await Windows.Storage.FileIO.WriteTextAsync(File, myString);
                
                return;

            }
            catch (Exception e)
            {

                e.Message.ToString();
            }
      
        }


   /*     public async void myHTTP2()
        { 
            Windows.Web.Http.HttpClient oHttpClient = new Windows.Web.Http.HttpClient();
            Uri uri = new Uri("https://ww3.allnone.ie/client/client_demo/cti/userCTI_GenericEntry.asp"); // some Url
            string stringXml= "...";  // some xml string
            HttpRequestMessage mSent = new HttpRequestMessage(HttpMethod.Post, uri);
        mSent.Content = 
                        new HttpStringContent(String.Format("xml={0}", stringXml), 
                        Windows.Storage.Streams.UnicodeEncoding.Utf8);

HttpResponseMessage mReceived = await oHttpClient.SendRequestAsync(mSent,
                                   HttpCompletionOption.ResponseContentRead);

// to get the xml response:
if (mReceived.IsSuccessStatusCode)
{
  string strXmlReturned = await mReceived.Content.ReadAsStringAsync();
    myString = strXmlReturned;
}
        }*/


      


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
