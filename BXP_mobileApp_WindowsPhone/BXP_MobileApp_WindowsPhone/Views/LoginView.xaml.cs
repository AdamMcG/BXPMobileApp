using BXP_MobileApp_WindowsPhone.Common;
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
using BXP_MobileApp_WindowsPhone.ViewModel;
using BXP_MobileApp_WindowsPhone.Model;
using Windows.UI.Popups;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556
namespace BXP_MobileApp_WindowsPhone.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginView : Page
    {
        StylingViewModel viewStyling = new StylingViewModel();
        SettingsViewModel viewSetting = new SettingsViewModel();
        private NavigationHelper navigationHelper;
        public LoginView()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
            this.systemTextBox.DataContext = viewSetting;
            this.UsernameTextBox.DataContext = viewSetting;
        }

        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
           
            get { return this.navigationHelper; }
        }

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
                viewSetting.propStrSystem = e.PageState["signInSystem"].ToString();
                viewSetting.propStrUsername = e.PageState["signInUsername"].ToString();
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
            e.PageState["signInUsername"] = viewSetting.propStrUsername;
            e.PageState["signInSystem"] = viewSetting.propStrSystem;
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
            if (e.Parameter == typeof(StylingViewModel))
            {
                this.viewStyling = (StylingViewModel)e.Parameter;
            }
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private async void fn_CheckconnectionBeforeLogin(object sender, RoutedEventArgs e)
        {
                MessageDialog mymessage = null;
                try
                {
                    string password = PasswordBox.Password;
                    string strSystem = "client_" + viewSetting.propStrSystem.ToLower();
                    Boolean boolCheck = await viewSetting.fn_retrieveLoginSession(password);
                    if (boolCheck == true)
                        navigateToMain();
                    else
                    {
                        string strErrorString = "Error: No valid Network Connection ";
                        mymessage = new MessageDialog(strErrorString);
                        await mymessage.ShowAsync();
                    }
                }
                catch (Exception reached){
                    throw reached;
                }
        }

        private async void navigateToMain()
        {
            MessageDialog mymessage = null;
           Login myLogin = Login.Instance;
            if (myLogin.propIntErrorId== 0)
            {
                string strSessionId = "Your session id for this session is:" + myLogin.propStrClient_SessionField;
                mymessage = new MessageDialog(strSessionId);
                await mymessage.ShowAsync();
                viewStyling.strUserName = viewSetting.propStrUsername;
                Frame.Navigate(typeof(HomePage));
            }
            else
            {
                string strErrorString = "Error \n" + myLogin.propIntErrorId + " : " + myLogin.propStrError;
                mymessage = new MessageDialog(strErrorString);
                await mymessage.ShowAsync();
            }
        }

        private void systemTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Windows.Storage.ApplicationDataContainer roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            roamingSettings.Values["strSystem"] = systemTextBox.Text;
        }

    }
}
