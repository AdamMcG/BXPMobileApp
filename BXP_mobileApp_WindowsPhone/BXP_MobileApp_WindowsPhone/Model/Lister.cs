using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BXP_MobileApp_WindowsPhone.Model
{
    class Lister: INotifyPropertyChanged
    {
        public string strError { get; set;}
        public string strFunction { get; set; }
        public int intError { get; set; }

        private ObservableCollection<Listee> colTodoList = new ObservableCollection<Listee>();
        public ObservableCollection<Listee> pColTodolist
        {
            get { return colTodoList; }
            set { colTodoList = value;
            NotifyPropertyChanged("pColTodolist");
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
