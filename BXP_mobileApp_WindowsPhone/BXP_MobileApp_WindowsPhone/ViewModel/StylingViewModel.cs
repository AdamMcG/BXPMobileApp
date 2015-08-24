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
     class StylingViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public StylingViewModel() {
            appFont = new FontFamily("Calibri");
            largeFontSize = 18;
            mediumFontSize = 12;
            smallFontSize = 10;
            buttonBackground = GetColorFromHex("#FFFF00");
            buttonForeground = GetColorFromHex("#000000");
        }

        #region helpfulMethods
        public static Windows.UI.Xaml.Media.SolidColorBrush GetColorFromHex(string hexaColor)
        {
            return new Windows.UI.Xaml.Media.SolidColorBrush(
                Windows.UI.Color.FromArgb(
                    255,
                    Convert.ToByte(hexaColor.Substring(1, 2), 16),
                    Convert.ToByte(hexaColor.Substring(3, 2), 16),
                    Convert.ToByte(hexaColor.Substring(5, 2), 16)
                )
            );
        }
        #endregion



        private string userName;
        private SolidColorBrush textForeground;
        public SolidColorBrush brusTextForeground {
            get { return textForeground; }
            set { textForeground = value;
            NotifyPropertyChanged("textForeground");
            }
        }
        public string strUserName
        {
            get { return userName; }
            set
            {
                userName = value;
                NotifyPropertyChanged("strUserName");
            }
        }


        //Fonts
        public static FontFamily appFont { get; set; }
        public static double largeFontSize { get; set; }
        public static double mediumFontSize { get; set; }
        public static double smallFontSize { get; set; }
         
        //Button
        public static SolidColorBrush buttonBackground { get; set; }
        public static SolidColorBrush buttonForeground { get; set; }
        
        //AppBackground
        public ImageSource backgroundSource {get; set;}




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
