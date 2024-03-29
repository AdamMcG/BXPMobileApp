﻿using BXP_MobileApp_WindowsPhone.Common;
using BXP_MobileApp_WindowsPhone.Model;
using BXP_MobileApp_WindowsPhone.ViewModel;
using BXP_MobileApp_WindowsPhone.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.Connectivity;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace BXP_MobileApp_WindowsPhone
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        RSSViewModel RSS = new RSSViewModel();
        private NavigationHelper navigationHelper;
        StylingViewModel myStyle = new StylingViewModel();
        Boolean check = false;
        SettingsViewModel viewSetting = new SettingsViewModel();
        public MainPage()
        {
            try
            {
                this.DataContext = RSS;
                this.InitializeComponent();
                check = fn_CheckForPriorLogin();
                this.navigationHelper = new NavigationHelper(this);
                this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
                this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
                ConnectionCheck();
                LayoutRoot.Background = myStyle.pbackgroundBrush;
            }
            catch (Exception)
            {

                throw;
            }

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

            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {


            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void Open_Login(object sender, RoutedEventArgs e)
        {
            if (check)
                Frame.Navigate(typeof(HomePage));
            else
                Frame.Navigate(typeof(LoginView));
        }

        private Boolean fn_CheckForPriorLogin()
        {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            if (localSettings.Values["loginStrfunctionURL"] != null)
                fnRetrieveStyling(localSettings);

            if (localSettings.Values["LoginClientID"] != null)
            {
                Login.Instance.propIntClient_Id = Int32.Parse(localSettings.Values["LoginClientID"].ToString());
                Login.Instance.propStrClient_SessionField = localSettings.Values["LoginClientSession"].ToString();
                return true;
            }

            return false;
        }

        private void fnRetrieveStyling(Windows.Storage.ApplicationDataContainer localSettings)
        {
            if (localSettings.Values["loginStrfunctionURL"].ToString() != null)
            {
                Login.Instance.propStrFunctionURL = localSettings.Values["LoginStrFunctionURL"].ToString();
                Login.Instance.propStrSystemUsed = "client_" +  localSettings.Values["LoginStrSystem"].ToString().ToLower();
                viewSetting.propStrUsername = localSettings.Values["SettingsUsername"].ToString();
                viewSetting.propStrSystem = Login.Instance.propStrSystemUsed;
                viewSetting.propStrUsername = localSettings.Values["SettingsUsername"].ToString();

                viewSetting.propObSetting.intSystemId = Int32.Parse(localSettings.Values["SettingsSystemId"].ToString());
                viewSetting.propObSetting.intInterfaceColumns = Int32.Parse(localSettings.Values["Settingcolumns"].ToString());
                viewSetting.propObSetting.RSSTitle = localSettings.Values["SettingsRSSTitle"].ToString();
                viewSetting.propObSetting.RSSFeed = localSettings.Values["SettingsRSSFeed"].ToString();
                viewSetting.propObSetting.boolInterfaceStoreUsername = Boolean.Parse(localSettings.Values["SettingsStoreUserName"].ToString());

                viewSetting.propObSetting.strImageLogoUrl = localSettings.Values["SettingsStoreLogoURL"].ToString();
                viewSetting.propObSetting.strImageBackground = localSettings.Values["SettingsImageBackgroundStr"].ToString();
                viewSetting.propObSetting.strFontColors = localSettings.Values["SettingsStoreFontColors"].ToString();
                viewSetting.propObSetting.strFontFaces = localSettings.Values["SettingsStoreFontFaces"].ToString();
                viewSetting.propObSetting.strFontSizes = localSettings.Values["SettingsStoreFontSizes"].ToString();
                viewSetting.propObSetting.keywords = localSettings.Values["SettingsStoreKeywords"].ToString();
                myStyle.assignToStyling(viewSetting.propObSetting);
            }
        }

        private void RSSTapped(object sender, TappedRoutedEventArgs e)
        {
            int index = RSS_Feed_ListView.SelectedIndex;
            Frame.Navigate(typeof(RSSFeedSpecific), RSS.propSynCollection.ElementAt(index));
        }

        public async void ConnectionCheck()
        {
            string strConnectionProfile = string.Empty;
            try
            {
                ConnectionProfile intProfile = NetworkInformation.GetInternetConnectionProfile();
                if (intProfile == null)
                {
                    MessageDialog myMessage = new MessageDialog("Unable to connect to the internet");
                    await myMessage.ShowAsync();
                    loginButton.IsEnabled = false;
                }
                else
                {
                    RSS.fnCallRetrievalFunction();
                }
            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
        }


    }
}
