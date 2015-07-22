using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BXP_MobileApp_WindowsPhone.Model
{
    class Hamster : INotifyPropertyChanged
    {
        public Hamster() { }
        public Hamster(int id, string subject, string description) {
            pIntHamsterID = id;
            pstrHamsterDescription = description;
            pStrHamsterSubject = subject;
        }
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));

        }


        private int intHamsterID;
        public int pIntHamsterID
        {
            get { return intHamsterID; }
            set
            {
                intHamsterID = value;
                NotifyPropertyChanged("pintHamsterID");
            }

        }

        private string strHamsterSubject;
        public string pStrHamsterSubject
        {
            get { return strHamsterSubject; }
            set
            {
                strHamsterSubject = value;
                NotifyPropertyChanged("pStrHamsterSubject");
            }
        }

        private string strHamsterDescription;
        public string pstrHamsterDescription
        {
            get { return strHamsterDescription; }
            set
            {
                strHamsterDescription = value;
                NotifyPropertyChanged("pstrHamsterDescription");
            }
        }


    }
}
