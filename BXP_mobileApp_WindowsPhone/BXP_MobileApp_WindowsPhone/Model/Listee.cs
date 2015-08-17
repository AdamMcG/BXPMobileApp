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
    class Listee : INotifyPropertyChanged
    {
        public Listee()
        {

        }

        public int intLister_id { get; set; }
        public int intListerClient_id { get; set; }
        public string strListerComplete { get; set; }
        public string strListerPersonal { get; set; }
        public DateTime dteListerDate { get; set; }

        public string strListerTitle { get; set; }
        public string strListerDescription { get; set; }
        public string strListerDeadline { get; set; }
        public string strListerCatagory { get; set; }
        public int intLister_linkId1 { get; set; }

        public int intLister_linkid2 { get; set; }
        public int intLister_linkid3 { get; set; }
        public int intLister_linkid4 { get; set; }
        public string strListerEcourseID { get; set; }
        public int intListerFromId { get; set; }

        public string strLister_fromDate { get; set; }
        public string strLister_fromSource { get; set; }
        public int intListerProjectId { get; set; }
        public int intListerMeetingId { get; set; }
        public int intListerMeetingActionId { get; set; }

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
