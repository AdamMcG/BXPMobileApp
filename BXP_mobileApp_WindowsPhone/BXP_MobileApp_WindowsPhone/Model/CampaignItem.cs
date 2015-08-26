using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BXP_MobileApp_WindowsPhone.Model
{
    class CampaignItem: INotifyPropertyChanged
    {

        private List<string> campaignItems;
        public List<string> propCampaignItems
        {
            get { return campaignItems; }
            set
            {
                campaignItems = value;
                NotifyPropertyChanged("propCampaignItems");
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
