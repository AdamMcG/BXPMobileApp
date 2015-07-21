using BXP_MobileApp_WindowsPhone.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BXP_MobileApp_WindowsPhone.ViewModel
{
    class ContactViewModel{

        public ContactViewModel() { }

        private ObservableCollection<Contact> colContacts;
        public ObservableCollection<Contact> pColContacts
        {
            get { return colContacts; }
            set { colContacts = value; }
        }


    }
}
