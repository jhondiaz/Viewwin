
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics.Drawables;
using Android.Graphics;
using Android.Accounts;


namespace Viewin
{
	[Activity (Label = "Login")]			
	public class LoginActivity : BaseActivity
	{

		private const string AppId = "1615275522040675";
		private const string ExtendedPermissions = "user_about_me,read_stream,publish_stream,email,user_likes,public_profile,user_friends";
		//FacebookClient fb;
		string accessToken;
		bool isLoggedIn;
		string lastMessageId;

		Button BtnFacebook;
		Button BtnRegister;
		Button BtnLogin;
		EditText TxtLogin;
		EditText TxtPwd;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.LoginActivitys);

			ColorDrawable colorDrawable = new ColorDrawable (Color.ParseColor (Helpers.ColorHeader));
			ActionBar.SetBackgroundDrawable (colorDrawable); 


			BtnFacebook = FindViewById<Button> (Resource.Id.BtnFacebook);
			BtnRegister = FindViewById<Button> (Resource.Id.BtnRegister);
			BtnLogin = FindViewById<Button> (Resource.Id.BtnLogin);

			TxtLogin = FindViewById<EditText> (Resource.Id.TxtLogin);
			TxtPwd = FindViewById<EditText> (Resource.Id.TxtPwd);
			BtnFacebook.Visibility = ViewStates.Gone;

			#if DEBUG

			TxtLogin.Text="systemaf5@hotmail.com";
			TxtPwd.Text="123456";

			#endif


			BtnRegister.Click += (sender, e) => {

				var datosActivity = new Intent (this, typeof(RegisterActivity));
				StartActivity (datosActivity);

			};


			BtnFacebook.Click += (sender, e) => {
				var webAuth = new Intent (this, typeof(FBWebViewAuthActivity));
				webAuth.PutExtra ("AppId", AppId);
				webAuth.PutExtra ("ExtendedPermissions", ExtendedPermissions);
				StartActivityForResult (webAuth, 0);
			};

	
			BtnLogin.Click += async (sender, e) => {

				if (TxtLogin.Text == string.Empty) {
					TxtLogin.SetError ("Digite su Email", null);
					TxtLogin.RequestFocus ();
					return;
				}

				if (TxtPwd.Text == string.Empty) {
					TxtPwd.SetError ("Digite su Contraseña", null);
					TxtPwd.RequestFocus ();
					return;
				}


				_ProgressDialog = ProgressDialog.Show (this, "Por favor espera...", "Procesando info...", true);	

				ResultMsg<Users> user=	await _Bussines.Login (TxtLogin.Text, TxtPwd.Text);

				_ProgressDialog.Dismiss();

				if(user!=null && user.Dato!=null){

					this.SetUserDatos(user.Dato);	
					var datosActivity = new Intent (this, typeof(GridActivity));
					StartActivity (datosActivity);
					Finish();

				}else{

					Toast.MakeText (ApplicationContext, "Error: Su Usuario o Contraseña es erradas", ToastLength.Long).Show ();
				}



			};



			try {
				Account[] accounts = AccountManager.Get (this).GetAccountsByType ("com.google");

				foreach (Account account in accounts) {

					if (!string.IsNullOrEmpty (account.Name)) {
						TxtLogin.Text = account.Name;
						return;
					}

					//accountsList.add(item);
				}
			} catch (Exception) {
				return;
			}
		}

		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult (requestCode, resultCode, data);

			switch (resultCode) {
			case Result.Ok:
//
//				accessToken = data.GetStringExtra ("AccessToken");
//				string userId = data.GetStringExtra ("UserId");
//				string error = data.GetStringExtra ("Exception");
//
//				fb = new FacebookClient (accessToken);
//
////				ImageView imgUser = FindViewById<ImageView> (Resource.Id.imgUser);
////				TextView txtvUserName = FindViewById<TextView> (Resource.Id.txtvUserName);
//
//				fb.GetTaskAsync ("me").ContinueWith (t => {
//					if (!t.IsFaulted) {
//
//						var result = (IDictionary<string, object>)t.Result;
//
//						var datosActivity = new Intent (this, typeof(RegisterActivity));
//
//						datosActivity.PutExtra ("Name", (string)result ["first_name"]);
//						datosActivity.PutExtra ("LastName", (string)result ["last_name"]);
//						datosActivity.PutExtra ("Email", (string)result ["email"]);
//						datosActivity.PutExtra ("Genero", (string)result ["gender"]);
//						datosActivity.PutExtra ("AccessToken", accessToken);
//
//						StartActivity (datosActivity);
//
//
//
////						// available picture types: square (50x50), small (50xvariable height), large (about 200x variable height) (all size in pixels)
////						// for more info visit http://developers.facebook.com/docs/reference/api
////						string profilePictureUrl = string.Format("https://graph.facebook.com/{0}/picture?type={1}&access_token={2}", userId, "square", accessToken);
////						var bm = BitmapFactory.DecodeStream (new Java.Net.URL(profilePictureUrl).OpenStream());
////						string profileName = (string)result["name"];
////
////						RunOnUiThread (()=> {
////							imgUser.SetImageBitmap (bm);
////							txtvUserName.Text = profileName;
////						});
//
//						isLoggedIn = true;
//					} else {
//						Alert ("Login con facebook", "Reason: " + error, false, (res) => {
//						});
//					}
//				});

				break;
			case Result.Canceled:
				Alert ("Login con facebook", "User Cancelled", false, (res) => {
				});
				break;
			default:
				break;
			}
		}

		public void Alert (string title, string message, bool CancelButton, Action<Result> callback)
		{
			AlertDialog.Builder builder = new AlertDialog.Builder (this);
			builder.SetTitle (title);
			builder.SetIcon (Resource.Drawable.ic_launcher);
			builder.SetMessage (message);

			builder.SetPositiveButton ("Ok", (sender, e) => {
				callback (Result.Ok);
			});

			if (CancelButton) {
				builder.SetNegativeButton ("Cancel", (sender, e) => {
					callback (Result.Canceled);
				});
			}

			builder.Show ();
		}

	}
}


