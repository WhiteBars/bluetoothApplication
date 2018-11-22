using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Input;
using Xamarin.Forms;

namespace BluetoothTest
{
    class WorkPageViewModel
    {
        private ListView RecievingData { get; set; }

        public ICommand StartRigCommand { get; set; }

        public ICommand StartPumpCommand { get; set; }

        public ICommand StartFanCommand { get; set; }

        public ICommand StopRigCommand { get; set; }

        private IBluetooth bluetooth;
        private readonly Thread recievingDataThread;

        public WorkPageViewModel(IWorkPage workPageInfo)
        {
            bluetooth = App.Bluetooth;
            StartRigCommand = new Command(OnStartRig);
            StartPumpCommand = new Command(OnStartPump);
            StartFanCommand = new Command(OnStartFan);
            StopRigCommand = new Command(OnStopRig);

            RecievingData = workPageInfo.List;
            RecievingData.ItemsSource = bluetooth.RecievingData;

            recievingDataThread = new Thread(bluetooth.RecieveData);
            StartSnifThread();
        }

        private void StartSnifThread()
        {
            var thread = new Thread(e =>
            {
                recievingDataThread.Start();
                while (bluetooth.IsConnected)
                {
                    var data = bluetooth.RecievingData[bluetooth.RecievingData.Count];
                    if (data == "33")
                        recievingDataThread.Abort();
                }
            });
        }

        private void OnStartRig()
        {
            if (bluetooth.IsConnected)
                bluetooth.SendData(53);
            else
                Application.Current
                    .MainPage
                    .DisplayAlert("Ошибка!", "Нет подключенных устройств", "ОК");            
        }

        private void OnStartPump()
        {
            if (bluetooth.IsConnected)
                bluetooth.SendData(54);
            else
                Application
                    .Current
                    .MainPage
                    .DisplayAlert("Ошибка!", "Нет подключенных устройств", "ОК");
        }

        private void OnStartFan()
        {
            if (bluetooth.IsConnected)
                bluetooth.SendData(55);
            else
                Application
                    .Current
                    .MainPage
                    .DisplayAlert("Ошибка!", "Нет подключенных устройств", "ОК");
        }

        private void OnStopRig()
        {
            if (bluetooth.IsConnected)
            {
                bluetooth.SendData(56);
            }
            else
                Application
                    .Current
                    .MainPage
                    .DisplayAlert("Ошибка!", "Нет подключенных устройств", "ОК");
        }
    }
}
