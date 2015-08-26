using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BXP_MobileApp_WindowsPhone.Model
{
    class Setting : INotifyPropertyChanged
    {
        private string strFunction;
        public string propStrFunction
        {
            get { return strFunction; }
            set
            {
                strFunction = value;
                NotifyPropertyChanged("strFunction");
            }
        }

        private int intErrorId;
        public int propIntErrorId
        {
            get { return intErrorId; }
            set
            {
                intErrorId = value;
                NotifyPropertyChanged("propIntErrorId");
            }
        }

        private string strError;
        public string propStrError
        {
            get { return strError; }
            set { strError = value;
            NotifyPropertyChanged("propStrError");
            }
        }

        public  int intSystemId { get; set; }
        public  int intInterfaceColumns { get; set; }
        public  string RSSTitle { get; set; }
        public  string RSSFeed { get; set; }
        public  Boolean boolInterfaceStoreUsername { get; set; }
        public  string strImageLogoUrl { get; set; }
        public  string strImageBackground { get; set; }
        public  string strFontColors { get; set; }
        public  string strFontFaces { get; set; }
        public  string strFontSizes { get; set; }
        public  string keywords { get; set; }
        private  List<Button> strInterfacebuttons = new List<Button>();
        public List<Button> propstrInterfaceButtons
        {
            get { return strInterfacebuttons; }
            set { strInterfacebuttons = value;
            NotifyPropertyChanged("propstrInterfaceButtons");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
