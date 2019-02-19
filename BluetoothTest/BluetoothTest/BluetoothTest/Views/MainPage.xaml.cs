using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace BluetoothTest
{
    public partial class MainPage : ContentPage, IMainPage
    {
        /// <summary>
        /// Потихоньку надо перевести это в MVVM
        /// </summary>

        public ListView List => list;
        public INavigation PageNavigation => Navigation;

        public MainPage()
        {
            InitializeComponent();
            //list.ItemsSource = App.Bluetooth.BondedDevices;
            //search.Clicked += (a,b) => App.Bluetooth.Find();
            BindingContext = new MainPageViewModel(this);/*
            list.ItemsSource = App.Bluetooth.AvailibleDevices;*/
            //var a = list.Resources;
            //bluetooth = App.Bluetooth;
            //list.ItemsSource = bluetooth.BondedDevices;
            //listDevices.ItemsSource = bluetooth.AvailibleDevices; ;
            //label.Text = "♠ HELLO COMRADE!!! ♠";
            //testList.ItemsSource = bluetooth.RecievingData;
            //search.Clicked += Search_Clicked;
            //list.ItemTapped += List_ItemTapped;
            //listDevices.ItemTapped += List_ItemTapped;
            //bttn.Clicked += Bttn_Clicked;
        }

        //private void Bttn_Clicked(object sender, EventArgs e)
        //{
        //    if (bluetooth.IsConnected) bluetooth.SendData(55);
        //    else DisplayAlert("Алярма", "Нет подключенных устройств", "ОК");
        //}

        //private async void List_ItemTapped(object sender, ItemTappedEventArgs e)
        //{
        //    if (e.Item == null) return;
        //    var device = e.Item as BleDevice;
        //    var res = await DisplayAlert("Подключение", "Подключиться к этому устройству?",
        //        "Да", "Нет");
        //    if (res)
        //    {
        //        bluetooth.Connect(device.Address);
        //        testLabel.Text = bluetooth.IsConnected.ToString();
        //        new Thread(() => bluetooth.RecieveData()).Start();
        //    }
        //}

        //private void Search_Clicked(object sender, EventArgs e)
        //{
        //    delThis.IsVisible = true;
        //    listDevices.IsVisible = true;
        //    bluetooth.Find();
        //}
    }
}
