using BXP_MobileApp_WindowsPhone.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;


namespace BXP_MobileApp_WindowsPhone.ViewModel
{
    //This class controls the styling and configuration settings from the config file. 
    class StylingViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        Login myLogin = Login.Instance;
        public StylingViewModel()
        {
        }

        public void assignValues()
        {
            pappFont = new FontFamily("Calibri");
            plargeFontSize = 35;
            pmediumFontSize = 27;
            psmallFontSize = 20;
            pbuttonBackground = GetColorFromHex("#FF0000");
            pbuttonForeground = GetColorFromHex("#FF0000");
            BitmapImage myimage = new BitmapImage(new Uri("ms-appx:///Assets/maxresdefault.jpg"));
            pbackgroundSource = myimage;
            pbackgroundBrush = new ImageBrush { ImageSource = pbackgroundSource };
            pLogoSource = new BitmapImage(new Uri("ms-appx:///Assets/Logo.scale-100.png"));
            pLogoBrush = new ImageBrush { ImageSource = pLogoSource };
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
        public string strUserName
        {
            get { return userName; }
            set
            {
                userName = value;
                NotifyPropertyChanged("strUserName");
            }
        }

        private static FontFamily appFont = new FontFamily("Calibri");
        private static double largeFontSize = 10;
        private static double mediumFontSize = 10;
        private static double smallFontSize = 10;
        private static SolidColorBrush buttonBackground = GetColorFromHex("#FF0000");
        private static SolidColorBrush buttonForeground = GetColorFromHex("#FF0000");

        //Fonts
        public FontFamily pappFont
        {
            get
            { return appFont; }
            set
            {
                appFont = value;
                NotifyPropertyChanged("pappFont");
            }
        }
        public double plargeFontSize
        {
            get { return largeFontSize; }
            set
            {
                largeFontSize = value;
                NotifyPropertyChanged("pLargeFontSize");
            }
        }
        public double pmediumFontSize
        {
            get { return mediumFontSize; }
            set
            {
                mediumFontSize = value;
                NotifyPropertyChanged("pmediumeFontSize");
            }
        }
        public double psmallFontSize
        {
            get { return smallFontSize; }
            set
            {
                smallFontSize = value;
                NotifyPropertyChanged("psmallFontSize");
            }
        }

        //Button
        public SolidColorBrush pbuttonBackground
        {
            get { return buttonBackground; }
            set
            {
                buttonBackground = value;
                NotifyPropertyChanged("pbuttonBackground");
            }
        }
        public SolidColorBrush pbuttonForeground
        {
            get { return buttonForeground; }
            set
            {
                buttonForeground = value;
                NotifyPropertyChanged("pbuttonForground");
            }
        }

        //AppBackground
        private static ImageBrush backgroundBrush;
        public ImageBrush pbackgroundBrush
        {
            get { return backgroundBrush; }
            set
            {
                backgroundBrush = value;
                NotifyPropertyChanged("pbackgroundBrush");
            }
        }
        private static ImageSource backgroundSource;
        public ImageSource pbackgroundSource
        {
            get { return backgroundSource; }
            set
            {
                backgroundSource = value;
                NotifyPropertyChanged("pbackgroundSource");
            }
        }

        //Logo
        private static ImageBrush LogoBrush;
        public ImageBrush pLogoBrush
        {
            get { return LogoBrush; }
            set
            {
                LogoBrush = value;
                NotifyPropertyChanged("pLogoBrush");
            }
        }
        private static ImageSource logoSource;
        public ImageSource pLogoSource
        {
            get { return logoSource; }
            set
            {
                logoSource = value;
                NotifyPropertyChanged("pLogoSource");
            }
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
