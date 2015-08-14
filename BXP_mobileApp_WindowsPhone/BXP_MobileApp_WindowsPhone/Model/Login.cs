using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BXP_MobileApp_WindowsPhone.Model
{
    class Login : INotifyPropertyChanged
    {
        private static Login instance;

        private Login() { }


        private string strSystemUsed;
        public string propStrSystemUsed
        {
            get { return strSystemUsed; }
            set
            {
                strSystemUsed = value;
                NotifyPropertyChanged("propStrSystemUsed");
            }
        }

        private string strFunctionURL;
        public string propStrFunctionURL
        {
            get { return strFunctionURL; }
            set
            {
                strFunctionURL = value;
                NotifyPropertyChanged("propStrFunctionURL");
            }
        }

        public static Login Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Login();
                }
                return instance;
            }
        }

        private int intClient_Id;
        public int propIntClient_Id
        {
            get { return intClient_Id; }
            set
            {
                intClient_Id = value;
                NotifyPropertyChanged("propIntClient_Id");
            }

        }

        private string strClient_SessionField;
        public string propStrClient_SessionField
        {
            get { return strClient_SessionField; }
            set
            {
                strClient_SessionField = value;
                NotifyPropertyChanged("propStrClient_SessionField");
            }
        }

        private int intErrorId;
        public int propIntErrorId
        {
            get { return intErrorId; }
            set
            {
                intErrorId = value;
                NotifyPropertyChanged("intErrorId");
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
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
