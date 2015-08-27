using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BXP_MobileApp_WindowsPhone.Model
{
    class Campaign : INotifyPropertyChanged
    {
        public Campaign() { }

        private int intErrorId;
        public int propIntErrorId
        {
            get { return intErrorId; }
            set { intErrorId = value;
            NotifyPropertyChanged("propIntErrorId");
            }
        }

        private string strFunction;
        public string propStrFunction
        {
            get { return strFunction; }
            set { strFunction = value;
            NotifyPropertyChanged("propStrFunction");
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

        private ObservableCollection<CampaignItem> campaignItems = new ObservableCollection<CampaignItem>();
        public ObservableCollection<CampaignItem> listOfCampaignItems
        {
            get { return campaignItems; }
            set { campaignItems = value;
            NotifyPropertyChanged("listOfCampaignItems");
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