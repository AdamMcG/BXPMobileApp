using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Web;
using Windows.Web.Syndication;

namespace BXP_MobileApp_WindowsPhone.Model
{
    class RSSTrimmer
    {
        ObservableCollection<SyndicationItem> mycollection = new ObservableCollection<SyndicationItem>();
        public ObservableCollection<SyndicationItem> _collection
        {
            get { return mycollection; }

            set { _collection = value; }
        }
        public async void testRss()
        {
            try
            {
                SyndicationClient client = new SyndicationClient();
                client.BypassCacheOnRetrieve = true;

                Uri RSSURI = new Uri("http://www.rte.ie/news/rss/news-headlines.xml");
             SyndicationFeed addd = await client.RetrieveFeedAsync(RSSURI);
             int check =0;
             foreach (SyndicationItem feed in addd.Items)
             {
                 _collection.Add(feed);
                 check++;
             }
               
            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
        }


            public async void awaitRSS()
            {
                testRss();
            }

            public RSSTrimmer()
            {
            }
        }
    }
