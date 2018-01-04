using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace timeDateCalculator
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

			MainPage = new timeDateCalculator.MainPage();
		}

		protected override void OnStart ()
		{
            AppCenter.Start("android=25636ee3-00b9-4879-b160-443dcc4507d2;",
                    typeof(Analytics), typeof(Crashes));
        }

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
