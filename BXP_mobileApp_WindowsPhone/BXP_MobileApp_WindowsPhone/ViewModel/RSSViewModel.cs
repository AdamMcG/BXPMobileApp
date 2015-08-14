using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Windows.Foundation;
using Windows.Web;
using Windows.Web.Syndication;

namespace BXP_MobileApp_WindowsPhone.Model
{
    //This class controls the list of RSS items for the RSS feed. 
    class RSSViewModel
    {
        ObservableCollection<SyndicationItem> syndItemcollection = new ObservableCollection<SyndicationItem>();
        public ObservableCollection<SyndicationItem> propSynCollection{
            get { return syndItemcollection; }

            set { propSynCollection = value; }
        }
        private async Task fn_retrieveRSSItems()
        {
            try
            {
                SyndicationClient client = new SyndicationClient();
                client.BypassCacheOnRetrieve = true;
                SyndicationFeed synRssFeed;
                synRssFeed = await client.RetrieveFeedAsync(new Uri("http://ww3.allnone.ie/client/client_demo/message/rss2feed.asp"));
                foreach (SyndicationItem feed in synRssFeed.Items)
                    propSynCollection.Add(feed);

            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
             return;

        }

            public async void fnCallRetrievalFunction()
            {
                await fn_retrieveRSSItems();
            }
        }
    }
