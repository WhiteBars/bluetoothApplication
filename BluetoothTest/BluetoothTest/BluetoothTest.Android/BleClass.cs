using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Bluetooth;
using Android.Bluetooth.LE;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.IO;
using Java.Lang.Reflect;
using Plugin.SimpleAudioPlayer;
using Xamarin.Forms.Internals;

namespace BluetoothTest.Droid
{
    public class BleClass : IBluetooth
    {
        public static ObservableCollection<BleDevice> bluetoothDevices;

        BluetoothSocket socket;
        BluetoothAdapter bluetooth;
        BluetoothDevice device;
        CustomBroadcastReceiver mReceiver;
        MainActivity MainActivity { get; set; }
        /// <summary>
        /// Дает информацию, есть ли соединение с устройством
        /// </summary>
        public bool IsConnected { get; set; } = false;
        /// <summary>
        /// Список сопряженных устройств 
        /// </summary>
        public ObservableCollection<BleDevice> BondedDevices { get; set; }
        /// <summary>
        /// Список доступных к подключению устройств
        /// </summary>
        public ObservableCollection<BleDevice> AvailibleDevices => bluetoothDevices;

        public ObservableCollection<string> RecievingData { get; set; }

        /// <summary>
        /// Коструктор, который инициализирует MainActivity и необходимые для работы свойства и поля
        /// </summary>
        /// <param name="main">MainActivity</param>
        public BleClass(MainActivity main)
        {
            MainActivity = main;
            bluetoothDevices = new ObservableCollection<BleDevice>();
            RecievingData = new ObservableCollection<string>() { "ThreadIsStarted" };
            bluetooth = BluetoothAdapter.DefaultAdapter;
            GetPaired();
        }
        /// <summary>
        /// Проигрывает звук предупреждения
        /// </summary>
        public async Task PlaySound()
        {
            var player = CrossSimpleAudioPlayer.Current;
            player.Load("warning.mp3");
            player.Play();
        }
        /// <summary>
        /// Получает список сопряженных устройств 
        /// </summary>
        void GetPaired()
        {
            BondedDevices = new ObservableCollection<BleDevice>();
            var bondedDevices = bluetooth.BondedDevices;
            if (bondedDevices.Count > 0)
            {
                foreach (var e in bondedDevices) BondedDevices.Add(new BleDevice(e.Address, e.Name));
            }
        }
        /// <summary>
        /// Включение bluetooth, если он еще не включен. 
        /// Если же blutooth включен, то ничего не случится
        /// </summary>
        public void Enable()
        {
            string enableBT = BluetoothAdapter.ActionRequestEnable;
            MainActivity.StartActivityForResult(new Intent(enableBT), 0);
        }
        /// <summary>
        /// Поиск и получение списка доступных к подключению устройств
        /// </summary>
        public void Find()
        {
            bluetooth.StartDiscovery();

            IntentFilter filter = new IntentFilter();
            filter.AddAction(BluetoothDevice.ActionFound);
            mReceiver = new CustomBroadcastReceiver();
            MainActivity.RegisterReceiver(mReceiver, filter);
        }
        /// <summary>
        /// Соединение с выбранным устройством
        /// </summary>
        /// <param name="deviceAddr">MAC адрес устройства</param>
        public void Connect(string deviceAddr)
        {
            try
            {
                device = bluetooth.GetRemoteDevice(deviceAddr);
                //var method = device.Class.GetMethod("createSocket", new Java.Lang.Class[] { Java.Lang.Integer.Type });
                //socket = method.Invoke(device, 1) as BluetoothSocket;  
                socket = device.CreateInsecureRfcommSocketToServiceRecord(MainActivity.uuid);
                Toast.MakeText(MainActivity.ApplicationContext, $"{socket.RemoteDevice}", ToastLength.Long).Show();

                socket.Connect();
                IsConnected = true;
                Toast.MakeText(MainActivity.ApplicationContext, "ПОДКЛЮЧЕНО", ToastLength.Long).Show();
            }
            catch (Exception e)
            {
                Log.Warning("Bluetooth", e.Message);
                Toast.MakeText(MainActivity.ApplicationContext, $"ОШИБКА ПОДКЛЮЧЕНИЯ {e.Message}", ToastLength.Long).Show();
            }
        }
        /// <summary>
        /// Отправляет код сопряженному устройству
        /// </summary>
        /// <param name="data">Данные к отправке</param>
        public void SendData(int data)
        {
            try
            {
                var outStream = socket.OutputStream;
                var bytes = BitConverter.GetBytes(data);
                outStream.Write(bytes, 0, bytes.Length);
                outStream.Close();
            }
            catch (Exception e)
            {
                Log.Warning("Bluetooth", e.Message);
                Toast.MakeText(MainActivity.ApplicationContext, $"Ошибка при отправке данных. {e.Message}", ToastLength.Long).Show();
            }
            Toast.MakeText(MainActivity.ApplicationContext, "Данные успешно отправлены", ToastLength.Long).Show();
        }
        //
        public void RecieveData()
        {
            while (IsConnected)
            {
                var inStream = socket.InputStream;
                var bytes = BitConverter.GetBytes(1024);               
                RecievingData.Add(inStream.ReadByte().ToString());
            }
        }
        /// <summary>
        /// Завершает поиск устройств
        /// </summary>
        public void StopSearching()
        {
            bluetooth.CancelDiscovery();
        }
        /// <summary>
        /// Разрывает соединение
        /// </summary>
        public void Disconnect()
        {
            IsConnected = false;
            socket.Close();
        }
    }
}