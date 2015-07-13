using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml.Media;


namespace BXP_MobileApp_WindowsPhone.ViewModel
{
    //This class controls the styling and configuration settings from the config file. 
    class StylingViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };


        public string userName;

        public string strUserName
        {
            get { return userName; }
            set { userName = value; }
        }


        //Uses Solid color brush for coloring. 
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
