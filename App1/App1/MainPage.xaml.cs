using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.Web.Http;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using App1;
using System.Threading.Tasks;
using Windows.UI.Popups;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        HTTPRestViewModel myTest = new HTTPRestViewModel();

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.DataContext = myTest;
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private async void myButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await myTest.RESTcalls_GET_BXPAPI();
            MessageDialog mymessage = new MessageDialog(myTest.aString);
            await mymessage.ShowAsync();
        }

        private async void myButton2_Tapped(object sender, TappedRoutedEventArgs e)
        {
            string[] nameString = name_TextBlock.Text.Split(' ');

          
            List<KeyValuePair<string, string>> contentList = new List<KeyValuePair<string, string>>();
            contentList.Add(new KeyValuePair<string, string>("strFunction", "login"));
            contentList.Add(new KeyValuePair<string, string>("strSystem", "client_allnone"));
            contentList.Add(new KeyValuePair<string, string>("strClient_Username", "Adam McGivern"));
            contentList.Add(new KeyValuePair<string, string>("strClient_Password", "luke1712"));


            HttpFormUrlEncodedContent a = new HttpFormUrlEncodedContent(contentList);
            string strFunction = "https://ww3.allnone.ie/client/client_allnone/cti/userAPP_Main.asp";
            await myTest.RESTcalls_POST_BXPAPI(strFunction, a);
            MessageDialog mymessage = new MessageDialog(myTest.aString);
            await mymessage.ShowAsync();
        }
    }
}
