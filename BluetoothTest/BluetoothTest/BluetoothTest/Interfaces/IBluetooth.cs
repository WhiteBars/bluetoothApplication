using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace BluetoothTest
{
    /// <summary>
    /// Интерфейс для работы с bluetooth модулем
    /// </summary>
    public interface IBluetooth
    {
        void RecieveData();

        ObservableCollection<string> RecievingData { get; }
        /// <summary>
        /// Проигрывает звук предупреждения
        /// </summary>
        void PlaySound(string soundName);
        /// <summary>
        /// Разрывает соединение
        /// </summary>
        void Disconnect();
        /// <summary>
        /// Завершает поиск устройств
        /// </summary>
        void StopSearching();
        /// <summary>
        /// Отправляет код сопряженному устройству
        /// </summary>
        /// <param name="data">Данные к отправке</param>
        void SendData(int data);
        /// <summary>
        /// Включение bluetooth, если он еще не включен. 
        /// Если же blutooth включен, то ничего не случится
        /// </summary>
        void Enable();
        /// <summary>
        /// Поиск и получение списка доступных к подключению устройств
        /// </summary>
        void Find();
        /// <summary>
        /// Соединение с выбранным устройством
        /// </summary>
        /// <param name="deviceAddr">MAC адрес устройства</param>
        void Connect(string deviceAddr);
        /// <summary>
        /// Список сопряженных устройств 
        /// </summary>
        ObservableCollection<BleDevice> BondedDevices { get; }
        /// <summary>
        /// Список доступных к подключению устройств
        /// </summary>
        ObservableCollection<BleDevice> AvailibleDevices { get; }
        /// <summary>
        /// Дает информацию, есть ли соединение с устройством
        /// </summary>
        bool IsConnected { get; }
    }
}
