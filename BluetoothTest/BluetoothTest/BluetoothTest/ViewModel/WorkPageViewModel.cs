using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Input;
using Xamarin.Forms;

namespace BluetoothTest.ViewModel
{
    class WorkPageViewModel
    {
        ListView RecievingData { get; set; }

        public ICommand SendTestDataCommand { get; set; }

        IBluetooth bluetooth;
        readonly Thread recievingDataThread;

        public WorkPageViewModel(IWorkPage workPageInfo)
        {
            bluetooth = App.Bluetooth;
            SendTestDataCommand = new Command(SendTestData);

            RecievingData = workPageInfo.List;
            RecievingData.ItemsSource = bluetooth.RecievingData;

            recievingDataThread = new Thread(new ThreadStart(bluetooth.RecieveData));
        }

        void SendTestData()
        {
            //В качестве тестовых данных используется 55
            if (bluetooth.IsConnected) bluetooth.SendData(55);
            else Application.Current.MainPage.DisplayAlert("Алярма", "Нет подключенных устройств", "ОК");            
        }
    }
}
