using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                var Uri = new Uri("http://ww3.allnone.ie/client/client_allnone/message/atomfeed.asp");
                var responseText = await myClient.GetStringAsync(Uri);
                
               myString = responseText;
                int a = 001;
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
