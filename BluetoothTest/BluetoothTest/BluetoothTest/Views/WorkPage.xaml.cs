using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BluetoothTest
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkPage : TabbedPage
    {
        public WorkPage ()
        {
            InitializeComponent();
        }
    }
}