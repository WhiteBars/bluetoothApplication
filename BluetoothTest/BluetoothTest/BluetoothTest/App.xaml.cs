using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace BluetoothTest
{
	public partial class App : Application
	{
        /// <summary>
        /// Èםעונפויס הכÿ נאבמעû ס bluetooth
        /// </summary>
        public static IBluetooth Bluetooth { get; private set; }

		public App (IBluetooth bluetooth)
		{
			InitializeComponent();
            Bluetooth = bluetooth;
            //MainPage = new MainPage();
            MainPage = new WorkPage();
		}

		protected override void OnStart ()
		{
            // Handle when your app starts
            Bluetooth.Enable();
		}

		protected override void OnSleep ()
		{
            // Handle when your app sleeps
            //Bluetooth.Disconnect();
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}        
	}
}
