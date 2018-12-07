using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp.ViewModel
{
    public class MainGridViewModel : BindableBase
    {
        public MainGridViewModel()
        {
            Text = "Hello text";
        }

        private string _text;
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

    }
}
