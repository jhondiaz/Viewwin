using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Gcm.Client;
using Android.Util;
using Android.Content.PM;

namespace Viewin
{
	[Activity (Theme = "@style/Theme.Splash",
		       MainLauncher = true, 
		       NoHistory = true,
		       ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
		       ScreenOrientation= ScreenOrientation.Portrait)]
	public class MainActivity : BaseActivity
	{


		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Main);
			GcmClient.CheckDevice(this);
			GcmClient.CheckManifest(this);
		}


		protected override void OnResume ()
		{
			base.OnResume ();
			updateView ();
			StartActivity(new Intent(this,typeof(LoginActivity)));

		}



		void updateView()
		{
			var registrationId = GcmClient.GetRegistrationId(this);

			if (string.IsNullOrEmpty(registrationId))
			{
				Log.Info("Viewin", "Registering...");
				GcmClient.Register(this, GcmBroadcastReceiver.SENDER_IDS);
			}
		}

	}
}


