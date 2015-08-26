using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Networking;
using BXP_MobileApp_WindowsPhone.Common;
using BXP_MobileApp_WindowsPhone.ViewModel;
using Windows.UI;
using BXP_MobileApp_WindowsPhone.Model;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace BXP_MobileApp_WindowsPhone.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        Login myLogin = Login.Instance;
        SettingsViewModel mySettings = new SettingsViewModel();
        StylingViewModel viewStyling = new StylingViewModel();
        private NavigationHelper navigationHelper;
        public HomePage(){
            this.InitializeComponent();
            LayoutRoot.Background = viewStyling.pbackgroundBrush;
            this.DataContext = viewStyling;
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
        
        }

        #region navigationMethods
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        private void OpenToDo(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ToDo));
        }

        private void OpenTicket(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Ticket));
        }

        private void OpenToday(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Today));
        }

        private void OpenTomorrow(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Tomorrow));
        }

        private void OpenContacts(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Contact));
        }

        private void OpenCritical(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Critical));
        }
        #endregion

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            if (e.PageState != null)
            {
                myLogin.propStrClient_SessionField = e.PageState["LoginSession"].ToString();
                myLogin.propIntClient_Id = Int32.Parse(e.PageState["LoginClient"].ToString());
                myLogin.propStrFunctionURL = e.PageState["LoginURL"].ToString();
                myLogin.propStrSystemUsed = e.PageState["loginSystem"].ToString();
                mySettings.propStrUsername = e.PageState["settingVM"].ToString();
                this.Greeting_Text_Block.Text = "Hi, " + mySettings.propStrUsername;
            }

        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
            e.PageState["LoginSession"] = myLogin.propStrClient_SessionField;
            e.PageState["LoginClient"] = myLogin.propIntClient_Id;
            e.PageState["LoginURL"] = myLogin.propStrFunctionURL;
            e.PageState["loginSystem"] = myLogin.propStrSystemUsed;
            e.PageState["settingVM"] = mySettings.propStrUsername;
        }

        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.Greeting_Text_Block.Text = "Hi, " +mySettings.propStrUsername;
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion


        // Launch the URI
        async void DefaultLaunch(Uri uri){
        // Launch the URI
        var success = await Windows.System.Launcher.LaunchUriAsync(uri);

        if (success){
            // URI launched
        }
        else
        {
      // URI launch failed
        }
        }

        #region NavigationFrames
        private void OpenBxp(object sender, RoutedEventArgs e)
        {
            string uriToLaunch = @"http://www.bxpsoftware.com/";

        var uri = new Uri(uriToLaunch);

            DefaultLaunch(uri);
        }

        private void OpenWixi(object sender, RoutedEventArgs e)
        {
            string uriToLaunch = @"http://www.bxpsoftware.com/wixi/";

            // Create a Uri object from a URI string 
            var uri = new Uri(uriToLaunch);

            DefaultLaunch(uri);
        }

        private void OpenSystem(object sender, RoutedEventArgs e)
        {
            string uriToLaunch = @"https://ww3.allnone.ie/client/client_allnone/main/main.asp";

            // Create a Uri object from a URI string 
            var uri = new Uri(uriToLaunch);

            DefaultLaunch(uri);
        }

        private void OpenSettings(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Settings));
        }
        #endregion
    }
}
