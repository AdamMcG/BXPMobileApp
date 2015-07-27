using BXP_MobileApp_WindowsPhone.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace BXP_MobileApp_WindowsPhone.Model
{
    class ToDo : INotifyPropertyChanged
    {
        public ToDo()
        {
            strTodoType = "Reminder";
            strTodosubject = "This is a test";

        }


        public ToDo(int id, string type, DateTime from, DateTime to, string subject, string body, string link, string data)
        {
            intToDoID = id;
            dtetodoFrom = from;
            dtetodoTo = to;
            strTodosubject = subject;
            strtoDoBody = body;
            strToDoLink = link;
            strToDoData = data;
        }


        private StylingViewModel myStyle = new StylingViewModel();
        public StylingViewModel styleMyStyle
        {
            get { return myStyle; }
            set { myStyle = value; }
        }

        private int toDoID;
        public int intToDoID
        {
            get { return toDoID; }
            set
            {
                toDoID = value; NotifyPropertyChanged("intToDoID");
            }
        }

        private string todoType;
        public string strTodoType
        {
            get { return todoType; }
            set { todoType = value; NotifyPropertyChanged("strtodoType"); }
        }

        private DateTime toDoFrom;
        public DateTime dtetodoFrom
        {
            get
            {
                return toDoFrom;
            }
            set
            {
                toDoFrom = value;
                NotifyPropertyChanged("dtetodoFrom");
            }
        }

        private DateTime toDoTo;
        public DateTime dtetodoTo
        {

            get { return toDoTo; }
            set
            {
                toDoTo = value;
                NotifyPropertyChanged("dtetodoTo");
            }
        }

        private string todoSubject;
        public string strTodosubject
        {
            get { return todoSubject; }
            set
            {
                todoSubject = value;
                NotifyPropertyChanged("strTodosubject");
            }
        }

        private string todoBody;
        public string strtoDoBody
        {
            get { return todoBody; }
            set
            {
                todoBody = value;
                NotifyPropertyChanged("toDoBody");
            }
        }

        private string toDoLink;
        public string strToDoLink
        {
            get { return toDoLink; }
            set
            {
                toDoLink = value;
                NotifyPropertyChanged("strToDoLink");
            }
        }

        private string toDoData;
        public string strToDoData
        {
            get { return toDoData; }
            set
            {
                toDoData = value;
                NotifyPropertyChanged("strToDoData");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
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
