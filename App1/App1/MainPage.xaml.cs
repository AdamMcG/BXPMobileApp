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
            contentList.Add(new KeyValuePair<string, string>("system", "formlogging"));
            contentList.Add(new KeyValuePair<string, string>("user_id", "adammcgivern"));
            contentList.Add(new KeyValuePair<string, string>("user_key", "password"));
            contentList.Add(new KeyValuePair<string, string>("campaignid", "751"));

            contentList.Add(new KeyValuePair<string, string>("strCDA_751_field_0_1", "1"));
            contentList.Add(new KeyValuePair<string, string>("strCDA_751_field_1_1", nameString[0]));
            contentList.Add(new KeyValuePair<string, string>("strCDA_751_field_2_1", "L"));
            contentList.Add(new KeyValuePair<string, string>("strCDA_751_field_3_1", nameString[1]));

            contentList.Add(new KeyValuePair<string, string>("Home", "22222"));
            contentList.Add(new KeyValuePair<string, string>("workphone", "12322"));
            contentList.Add(new KeyValuePair<string, string>("mobile", "0863222"));


            contentList.Add(new KeyValuePair<string, string>("strCDA_751_field_0_10", "appartment 5"));
            contentList.Add(new KeyValuePair<string, string>("strCDA_751_field_1_10", "Fake Road"));
            contentList.Add(new KeyValuePair<string, string>("strCDA_751_field_2_10", "Tallaght"));
            contentList.Add(new KeyValuePair<string, string>("strCDA_751_field_3_10", "25"));
            contentList.Add(new KeyValuePair<string, string>("strCDA_751_field_4_10", "1"));

            HttpFormUrlEncodedContent a = new HttpFormUrlEncodedContent(contentList);

            await myTest.RESTcalls_POST_BXPAPI(a);
            MessageDialog mymessage = new MessageDialog(myTest.aString);
            await mymessage.ShowAsync();
        }
    }
}
