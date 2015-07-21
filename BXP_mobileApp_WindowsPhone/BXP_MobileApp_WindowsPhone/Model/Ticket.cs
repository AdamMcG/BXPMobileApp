using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BXP_MobileApp_WindowsPhone.Model
{
    class Ticket : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
            
        }


        private int intTicketID;
        public int pIntTicketID
        {
            get { return intTicketID; }
            set
            {
                intTicketID = value;
                NotifyPropertyChanged("pintTicketID");
            }

        }

        private string strticketSubject;
        public string pStrTicketSubject
        {
            get { return strticketSubject; }
            set
            {
                strticketSubject = value;
                NotifyPropertyChanged("pStrTicketSubject");
            }
        }

        private string strticketDescription;
        public string pstrTicketDescription
        {
            get { return strticketDescription; }
            set { strticketDescription = value;
            NotifyPropertyChanged("pstrTicketDescriptin");
            }
        }


    }
}
