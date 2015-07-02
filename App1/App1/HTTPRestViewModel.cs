using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.Web.Http;

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
                var Uri = new Uri("https://ww3.allnone.ie/client/client_demo/cti/userCTI_GenericEntry.asp");
              //Send a get request to pull down information in format.
                var response = await myClient.GetStringAsync(Uri);
                var responseText = await myClient.GetAsync(Uri);

                HttpStringContent a = new HttpStringContent("1234");
                
                //send a post request to push up information for submission. 
               var putrequest = await myClient.PostAsync(Uri,responseText.Content );

                myString = putrequest.Content.ToString();

                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
               StorageFile File = await localFolder.CreateFileAsync("test.txt", CreationCollisionOption.ReplaceExisting);

            
                return;
            }
            catch (Exception e)
            {

                
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
