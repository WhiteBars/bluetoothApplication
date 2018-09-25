using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace BluetoothTest.Droid
{
    class CustomBroadcastReceiver : BroadcastReceiver
    {
        public CustomBroadcastReceiver() : base()
        {
        }

        public override void OnReceive(Context context, Intent intent)
        {
            string action = intent.Action;
            // Когда найдено новое устройство
            if (BluetoothDevice.ActionFound.Equals(action))
            {
                // Получаем объект BluetoothDevice из интента
                BluetoothDevice device = intent.GetParcelableExtra(BluetoothDevice.ExtraDevice) as BluetoothDevice;
                //Добавляем имя и адрес в array adapter, чтобы показвать в ListView
                BleClass.bluetoothDevices.Add(new BleDevice(device.Address, device.Name));
            }
        }       
    }
}