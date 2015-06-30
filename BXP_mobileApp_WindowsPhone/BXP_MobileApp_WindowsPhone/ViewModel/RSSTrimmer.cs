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

        public async void testRss()
        {
            try
            {
                SyndicationClient client = new SyndicationClient();
                client.BypassCacheOnRetrieve = true;

                Uri RSSURI = new Uri("http://www.rte.ie/news/rss/entertainment.xml");
              await client.RetrieveFeedAsync(RSSURI);
               
               
            }
            catch (Exception e)
            {
                e.ToString();
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
