using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BXP_MobileApp_WindowsPhone.Model
{
    class HamsterList : INotifyPropertyChanged
    {
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

        public string propStrFunction
        { get; set; }

        public string propStrError
        { get; set; }


        private ObservableCollection<Hamster> colListOfHamsters = new ObservableCollection<Hamster>();
        public ObservableCollection<Hamster> propColListOfHamsters
        {
            get { return colListOfHamsters; }
            set { colListOfHamsters = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
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
