using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml.Media;


namespace BXP_MobileApp_WindowsPhone.ViewModel
{
    class StylingViewModel
    {
        //Uses Solid color brush for coloring. 
        public Brush myColor;

        public Brush _color
        {
            get { return myColor; }
            set { myColor = value; }
        }

        public FontFamily myFont;

        public FontFamily _font
        {
            set { myFont = value; }
            get { return myFont; }
        }
    }
}
