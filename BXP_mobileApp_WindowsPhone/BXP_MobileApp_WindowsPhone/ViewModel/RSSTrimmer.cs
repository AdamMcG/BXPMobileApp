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
    class RSSTrimmer
    {
        ObservableCollection<SyndicationItem> syndItemcollection = new ObservableCollection<SyndicationItem>();
        public ObservableCollection<SyndicationItem> propSynCollection
        {
            get { return syndItemcollection; }

            set { propSynCollection = value; }
        }
        public async Task testRss()
        {
            try
            {
                SyndicationClient client = new SyndicationClient();
                client.BypassCacheOnRetrieve = true;
                Uri RSSURI = new Uri("http://ww3.allnone.ie/client/client_allnone/message/rssfeed.asp");
                SyndicationFeed synRssFeed = await client.RetrieveFeedAsync(RSSURI);
             foreach (SyndicationItem feed in synRssFeed.Items)
             {
                 propSynCollection.Add(feed); 
             }
 
             return;
            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
        }


            public async void awaitRSS()
            {
                await testRss();
            }

            public RSSTrimmer()
            {
            }
        }
    }
