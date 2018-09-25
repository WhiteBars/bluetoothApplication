using System;
using System.Collections.Generic;
using System.Text;

namespace BluetoothTest
{
    public class BleDevice
    {
        public string Address { get; private set; }
        public string Name { get; private set; }

        public BleDevice()
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="addr">Адрес устройства</param>
        /// <param name="name">Имя устройства</param>
        public BleDevice(string addr, string name)
        {
            Address = addr;
            Name = name;
        }
    }
}
