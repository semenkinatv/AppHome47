using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppHome47
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AlarmPage();  // ClimatePage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
