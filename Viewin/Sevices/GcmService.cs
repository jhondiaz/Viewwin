using System;
using Android.Content;
using Gcm.Client;
using Android.Util;
using Android.App;
using System.Text;

namespace Viewin
{

	[BroadcastReceiver(Permission=Constants.PERMISSION_GCM_INTENTS)]
	[IntentFilter(new string[] { Constants.INTENT_FROM_GCM_MESSAGE }, Categories = new string[] { "@PACKAGE_NAME@" })]
	[IntentFilter(new string[] { Constants.INTENT_FROM_GCM_REGISTRATION_CALLBACK }, Categories = new string[] { "@PACKAGE_NAME@" })]
	[IntentFilter(new string[] { Constants.INTENT_FROM_GCM_LIBRARY_RETRY }, Categories = new string[] { "@PACKAGE_NAME@" })]
	public class GcmBroadcastReceiver : GcmBroadcastReceiverBase<GcmService>
	{
		public static string[] SENDER_IDS = new string[] {"196691218704"};
		public const string TAG = "VieWin-GCM";

	}


	public class GcmService:GcmServiceBase
	{
		public GcmService() : base(GcmBroadcastReceiver.SENDER_IDS) { }

		const string TAG = "GCM-VieWin";
		public Bussines _Bussines = Bussines.Instance;

		protected override void OnRegistered (Context context, string registrationId)
		{
			Log.Verbose(TAG, "GCM Registered: " + registrationId);
			createNotification("Viewin", "Bienvenido!");

			var prefs = GetSharedPreferences(Application.PackageName, FileCreationMode.Private);

			_Bussines.UpdateGCM (prefs.GetString("Email",null), registrationId);

			var edit = prefs.Edit();
			edit.PutString("Gcmid",registrationId);
			edit.Commit();





		}

		protected override void OnUnRegistered (Context context, string registrationId)
		{
			Log.Verbose(TAG, "GCM Unregistered: " + registrationId);

			createNotification("GCM Unregistered...", "The device has been unregistered, Tap to View!");

		

		}

		protected override void OnMessage (Context context, Intent intent)
		{
			Log.Info(TAG, "GCM Message Received!");

			var msg = new StringBuilder();

			if (intent != null && intent.Extras != null)
			{
				foreach (var key in intent.Extras.KeySet())
					msg.AppendLine(key + "=" + intent.Extras.Get(key).ToString());
			}


//			var prefs = GetSharedPreferences(context.PackageName, FileCreationMode.Private);
//			var edit = prefs.Edit();
//			edit.PutString("last_msg", msg.ToString());
//			edit.Commit();

			createNotification("GCM Sample", "Message Received for GCM Sample... Tap to View!");
		}

		protected override bool OnRecoverableError (Context context, string errorId)
		{
			Log.Warn(TAG, "Recoverable Error: " + errorId);
			return base.OnRecoverableError (context, errorId);
		}

		protected override void OnError (Context context, string errorId)
		{
			Log.Error(TAG, "GCM Error: " + errorId);
		}

		void createNotification(string title, string desc)
		{

			var notificationManager = GetSystemService(Context.NotificationService) as NotificationManager;
		
			var uiIntent = new Intent(this, typeof(MainActivity));

			var notification = new Notification(Android.Resource.Drawable.SymActionEmail, title);

			notification.Flags = NotificationFlags.AutoCancel;

			notification.SetLatestEventInfo(this, title, desc, PendingIntent.GetActivity(this, 0, uiIntent, 0));
		
			notificationManager.Notify(1, notification);
		}
	}
}

