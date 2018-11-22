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
    public partial class WorkPage : ContentPage, IWorkPage
    {
        public ListView List => throw new NotImplementedException();

        public WorkPage ()
        {
            InitializeComponent();
            BindingContext = new WorkPageViewModel(this);
        }
    }
}