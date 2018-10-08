using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace BluetoothTest
{
    class MainPageViewModel
    {
        public ObservableCollection<BleDevice> AvailableDevices { get; private set; }
        public ListView DevicesList { get; set; }

        readonly IBluetooth bluetooth;
        readonly INavigation navigaton;

        public ICommand SearchDevicesCommand { get; set; }

        public MainPageViewModel(IMainPage mainPageInfo)
        {
            bluetooth = App.Bluetooth;
            AvailableDevices = bluetooth.AvailibleDevices;
            SearchDevicesCommand = new Command(() => bluetooth.Find());
            navigaton = mainPageInfo.PageNavigation;
            DevicesList = mainPageInfo.List;
            DevicesList.ItemsSource = AvailableDevices;
            DevicesList.ItemSelected += DevicesList_ItemSelected;
        }

        private async void DevicesList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;
            var device = e.SelectedItem as BleDevice;
            var res = await Application.Current.MainPage.DisplayAlert("Подключение", "Подключиться к этому устройству?",
                "Да", "Нет");
            if (res)
            {
                bluetooth.Connect(device.Address);
            }            
        }
    }
}
