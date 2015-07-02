using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace BXP_MobileApp_WindowsPhone.ViewModel
{
    class HTTPRestViewModelcs
    {
        public HTTPRestViewModelcs()
        {

        }


        public async void myHTTP()
        {
            try
            {
                HttpClient myClient = new HttpClient();

                var uri = new Uri("https://msdn.microsoft.com/en-us/library/windows/apps/xaml/windows.web.http.aspx");

                var response = await myClient.GetAsync(uri);

                var statusCode = response.StatusCode;

                response.EnsureSuccessStatusCode();
                var responseText = await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {

                e.Message.ToString();
                e.Source.ToString();
                e.HelpLink.ToString();
            }
        }
        
    }
}
