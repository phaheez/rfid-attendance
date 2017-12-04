using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArduinoRFID.DataModel
{
    public class Navigation : BindableBase
    {
        private string _navigate;
        public string Navigate
        {
            get { return _navigate; }
            set
            {
                if ( value != _navigate )
                {
                    _navigate = value;
                    OnPropertyChanged("Navigate");
                }
            }
        }

        public Navigation(string url)
        {
            Navigate = url;
        }
    }
}
