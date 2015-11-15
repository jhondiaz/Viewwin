
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
using Android.Webkit;
using Android.Graphics.Drawables;
using Android.Graphics;
using Android.Telephony;

namespace Viewin
{
	[Activity (Label = "LOOKWIN")]			
	public class PublicAcivity : BaseActivity
	{

		public WebView WebView;
		public ProgressBar ProGWebview;
		public ProgressBar PrgBViews;
		private LinearLayout LnYNext;
		public TextView TxtCountViews;
		public TextView TxtPuntosR;
		public TextView TxtPuntosRR;
		private int mProgressStatus = 0;
		private static	System.Threading.Timer _TimerStartService;
		public int PuntosR=0;
		public int PuntosRR=0;

		ImageButton imageButton1;
		ImageButton imageButton2;
		ImageButton imageButton3;
		ImageButton imageButton4;
		ImageButton imageButton5;

		int[] ArrayViewsControl = {

			Resource.Drawable.img1,
			Resource.Drawable.img2,
			Resource.Drawable.img3,
			Resource.Drawable.img4,
			Resource.Drawable.img5

		};


		int IndexControl=0;
		int Intervalo = 2000;

		List<PublicViews> listViews;
		string Imei;
		Users DatoUser;
		PublicViews CurrentPublicView;

		public string StarUrl;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.PublicAcivity);

			ColorDrawable colorDrawable = new ColorDrawable (Color.ParseColor (Helpers.ColorHeader));
			ActionBar.SetBackgroundDrawable (colorDrawable); 

			WebView = FindViewById<WebView> (Resource.Id.Webview);

			ProGWebview = FindViewById<ProgressBar> (Resource.Id.ProGWebview);
			PrgBViews = FindViewById<ProgressBar> (Resource.Id.PrgBViews);
			LnYNext = FindViewById<LinearLayout> (Resource.Id.LnYNext);
			LnYNext.SetBackgroundColor (Color.ParseColor (Helpers.ColorHeader));


			imageButton1 = FindViewById<ImageButton> (Resource.Id.imageButton1);
			imageButton2 = FindViewById<ImageButton> (Resource.Id.imageButton2);
			imageButton3 = FindViewById<ImageButton> (Resource.Id.imageButton3);
			imageButton4 = FindViewById<ImageButton> (Resource.Id.imageButton4);
			imageButton5 = FindViewById<ImageButton> (Resource.Id.imageButton5);

			TxtPuntosR=FindViewById<TextView>(Resource.Id.TxtPuntosR);
			TxtPuntosRR=FindViewById<TextView>(Resource.Id.TxtPuntosRR);

			PrgBViews.Max = 6;

			WebView.Settings.JavaScriptEnabled = true;
			WebView.Settings.LoadsImagesAutomatically = true;
			WebView.SetWebViewClient (new BulletinWebViewClient (this, ProGWebview));

			imageButton1.Click+= (sender, e) => ValidaControl((ImageButton)sender);
			imageButton2.Click+= (sender, e) => ValidaControl((ImageButton)sender);
			imageButton3.Click+= (sender, e) => ValidaControl((ImageButton)sender);
			imageButton4.Click+= (sender, e) => ValidaControl((ImageButton)sender);
			imageButton5.Click+= (sender, e) => ValidaControl((ImageButton)sender);

			var telephonyManager = (TelephonyManager) GetSystemService(TelephonyService);

			Imei = telephonyManager.DeviceId;

			DatoUser = GetUserDatos ();



		}


		protected  override void OnResume ()
		{
			base.OnResume ();

			SetCountViews (0);
		}


		protected async override void OnStart ()
		{
			base.OnStart ();

			using (var _Bussines = new Bussines ()) {
			
			//	listViews = await	_Bussines.GetAllPublicViews ();
				var IdCategory=this.Intent.GetStringExtra("IdCategory");

				CurrentPublicView = await	_Bussines.GetPublicViewsByIdUser(DatoUser.Id,IdCategory, Helpers.EncriptarSHA1 (Imei));

				if (CurrentPublicView != null) {

					StarUrl = CurrentPublicView.Url;

					WebView.LoadUrl ("http://www.viewin.co/viewquerys/q2.html"); //

				} else {
					
					WebView.LoadUrl ("http://www.viewin.co/viewpublics/aut.html");
				}

			}

		

//			if (listViews.Count != 0) {
//				var index =	GetCountViews ();
//
//				TxtPuntosR.Text = DatoUser.Point.ToString();
//
//				if (index < listViews.Count) {
//
//					var Pv = listViews [index];
//					WebView.LoadUrl (Pv.Url);
//					//SetCountViews (index + 1);
//
//				}else {
//
//					WebView.LoadUrl ("http://www.viewin.co/viewpublics/aut.html");
//
//				}
//
//			} 


		}

		private void StartLoad()
		{

			imageButton1.Background = new ColorDrawable (Color.ParseColor ("#00000000"));
			imageButton2.Background = new ColorDrawable (Color.ParseColor ("#00000000"));
			imageButton3.Background = new ColorDrawable (Color.ParseColor ("#00000000"));
			imageButton4.Background = new ColorDrawable (Color.ParseColor ("#00000000"));
			imageButton5.Background = new ColorDrawable (Color.ParseColor ("#00000000"));

			LnYNext.Visibility = ViewStates.Gone;
			IndexControl = 0;

			Random rnd = new Random();

			int index = rnd.Next (0, 5); 

			switch (index) {

			case 0:
				imageButton1.SetImageResource (ArrayViewsControl[4]);
				imageButton1.Tag = 4;
				imageButton2.SetImageResource (ArrayViewsControl[2]);
				imageButton2.Tag = 2;
				imageButton3.SetImageResource (ArrayViewsControl[0]);
				imageButton3.Tag = 0;
				imageButton4.SetImageResource (ArrayViewsControl[1]);
				imageButton4.Tag = 1;
				imageButton5.SetImageResource (ArrayViewsControl[3]);
				imageButton5.Tag = 3;
				break;
			case 1:
				imageButton1.SetImageResource (ArrayViewsControl[1]);
				imageButton1.Tag = 1;
				imageButton2.SetImageResource (ArrayViewsControl[4]);
				imageButton2.Tag = 4;
				imageButton3.SetImageResource (ArrayViewsControl[2]);
				imageButton3.Tag = 2;
				imageButton4.SetImageResource (ArrayViewsControl[0]);
				imageButton4.Tag = 0;
				imageButton5.SetImageResource (ArrayViewsControl[3]);
				imageButton5.Tag = 3;
				break;

			case 2:
				imageButton1.SetImageResource (ArrayViewsControl[1]);
				imageButton1.Tag = 1;
				imageButton2.SetImageResource (ArrayViewsControl[3]);
				imageButton2.Tag = 3;
				imageButton3.SetImageResource (ArrayViewsControl[2]);
				imageButton3.Tag = 2;
				imageButton4.SetImageResource (ArrayViewsControl[0]);
				imageButton4.Tag = 0;
				imageButton5.SetImageResource (ArrayViewsControl[4]);
				imageButton5.Tag = 4;
				break;

			case 3:
				imageButton1.SetImageResource (ArrayViewsControl[0]);
				imageButton1.Tag = 0;
				imageButton2.SetImageResource (ArrayViewsControl[3]);
				imageButton2.Tag = 3;
				imageButton3.SetImageResource (ArrayViewsControl[1]);
				imageButton3.Tag = 1;
				imageButton4.SetImageResource (ArrayViewsControl[4]);
				imageButton4.Tag = 4;
				imageButton5.SetImageResource (ArrayViewsControl[2]);
				imageButton5.Tag = 2;
				break;

			case 4:
				imageButton1.SetImageResource (ArrayViewsControl[1]);
				imageButton1.Tag = 1;
				imageButton2.SetImageResource (ArrayViewsControl[3]);
				imageButton2.Tag = 3;
				imageButton3.SetImageResource (ArrayViewsControl[0]);
				imageButton3.Tag = 0;
				imageButton4.SetImageResource (ArrayViewsControl[2]);
				imageButton4.Tag = 2;
				imageButton5.SetImageResource (ArrayViewsControl[4]);
				imageButton5.Tag = 4;
				break;

			case 5:
				imageButton1.SetImageResource (ArrayViewsControl[4]);
				imageButton1.Tag = 4;
				imageButton2.SetImageResource (ArrayViewsControl[1]);
				imageButton2.Tag = 1;
				imageButton3.SetImageResource (ArrayViewsControl[2]);
				imageButton3.Tag = 2;
				imageButton4.SetImageResource (ArrayViewsControl[0]);
				imageButton4.Tag = 0;
				imageButton5.SetImageResource (ArrayViewsControl[3]);
				imageButton5.Tag = 3;
				break;

			}



		}

		public async void ValidaControl(ImageButton valor)
		{
			valor.Background = new ColorDrawable (Color.ParseColor ("#b0ff00"));

			if (IndexControl==int.Parse(valor.Tag.ToString())) {

				IndexControl = IndexControl + 1;

				if (IndexControl == 5) {

				//	Toast.MakeText (this,"+20 pesos a tu cuanta", ToastLength.Short).Show ();

					using (var _Bussines = new Bussines ()) {

						//	listViews = await	_Bussines.GetAllPublicViews ();

						var IdCategory=this.Intent.GetStringExtra("IdCategory");

						CurrentPublicView = await	_Bussines.GetPublicViewsByIdUser(DatoUser.Id,IdCategory, Helpers.EncriptarSHA1 (Imei));

						if (CurrentPublicView != null) {

							Intervalo = CurrentPublicView.TimerOut;
							WebView.LoadUrl (CurrentPublicView.Url);
							PuntosR = PuntosR + CurrentPublicView.Point;

							TxtPuntosR.Text = PuntosR.ToString ();
							
							if (PuntosR >= 1000) {

								TxtPuntosRR.Text = (PuntosRR + int.Parse (TxtPuntosRR.Text.ToString ())).ToString ();

								PuntosR = 0;
							}

							if (DatoUser != null) {

								using (var bussines = new Bussines ()) {

									TxtPuntosR.Text= (await bussines.UpdatePoint (DatoUser.Id, CurrentPublicView.Id, Helpers.EncriptarSHA1 (Imei))).ToString();

								}

							}

						} else {

							WebView.LoadUrl ("http://www.viewin.co/viewpublics/aut.html");
						}

					}



//					if (GetCountViews () < listViews.Count) {
//
//						var Pv = listViews [GetCountViews ()];
//						Intervalo = Pv.TimerOut;
//						WebView.LoadUrl (Pv.Url);
//						var index =	GetCountViews () + 1;
//						SetCountViews (index);
////
////						PuntosR = PuntosR + Pv.Point;
////
////						TxtPuntosR.Text = PuntosR.ToString ();
//
//						if (PuntosR >= 1000) {
//
//							TxtPuntosRR.Text = (PuntosRR + int.Parse (TxtPuntosRR.Text.ToString ())).ToString ();
//
//							PuntosR = 0;
//						}
//
//
//						if (DatoUser != null) {
//							
//							using (var bussines = new Bussines ()) {
//
//								TxtPuntosR.Text= (await bussines.UpdatePoint (DatoUser.Id, Pv.Id, Helpers.EncriptarSHA1 (Imei))).ToString();
//
//							}
//
//						}
//
//					
//
//					} else {
//
//						WebView.LoadUrl ("http://www.viewin.co/viewpublics/aut.html");
//					}


				}

				return;
			}


			valor.Background = new ColorDrawable (Color.ParseColor ("#00000000"));
			Toast.MakeText (this, "por favor presinalas en el orden", ToastLength.Short).Show ();

		}


		public void StartViews ()
		{
			try {



				if (_TimerStartService != null) {
					_TimerStartService.Dispose ();
				}

				mProgressStatus=0;


				_TimerStartService = new System.Threading.Timer (delegate {

					PrgBViews.Progress = mProgressStatus;

					mProgressStatus = mProgressStatus + 1;

					if (mProgressStatus > 5) {

						_TimerStartService.Dispose ();

						PrgBViews.Progress = 0;

						this.RunOnUiThread (() => {

							LnYNext.Visibility = ViewStates.Visible;

						});


					};


				}, null, 0, Intervalo);

			} catch (Exception) {

				return;
			}

		}

		public	class BulletinWebViewClient : WebViewClient
		{
			PublicAcivity PublicAcivity;
			public ProgressBar ProGWebview;
			private bool IsStart= false;

			public BulletinWebViewClient (PublicAcivity PublicAcivity, ProgressBar ProGWebview)
			{

				this.PublicAcivity = PublicAcivity;
				this.ProGWebview = ProGWebview;
			}

			public override void OnPageStarted (WebView view, string url, Android.Graphics.Bitmap favicon)
			{
				base.OnPageStarted (view, url, favicon);
				ProGWebview.Visibility = ViewStates.Visible;
				PublicAcivity.StartLoad ();

			}

			public override void OnPageFinished (WebView view, string url)
			{
				base.OnPageFinished (view, url);

				if (IsStart) {
					
					ProGWebview.Visibility = ViewStates.Gone;
					PublicAcivity.StartViews ();
				
				}
			

			}

//			public override bool ShouldOverrideUrlLoading (WebView view, string url)
//			{
//				view.LoadUrl (url);
//				return true;
//			}



			public override bool ShouldOverrideUrlLoading (WebView view, string url)
			{

				if (IsStart)
					return true;

				// If the URL is not our own custom scheme, just let the webView load the URL as usual
				var scheme = "hybrid:";

				if (!url.StartsWith (scheme))
					return false;

				// This handler will treat everything between the protocol and "?"
				// as the method name.  The querystring has all of the parameters.
				var resources = url.Substring (scheme.Length).Split ('?');
				var method = resources [0];
				var parameters = System.Web.HttpUtility.ParseQueryString (resources [1]);

				if (method == "BtnNext") {

					var TypeQuery =parameters["TypeQuery"];

					switch (TypeQuery) {

					case "1":

						var TxtValor = parameters ["TxtValor"];
						if (string.IsNullOrEmpty (TxtValor))
							return true;
											

						break;

					case "2":

						var RadioButton = parameters ["RadioButton"];
						if (string.IsNullOrEmpty (RadioButton))
							return true;


						break;


					default:
						break;
					}



					IsStart = true;


					// Add some text to our string here so that we know something
					// happened on the native part of the round trip.
					//var prepended = string.Format ("C# says: {0}", textbox);

					// Build some javascript using the C#-modified result
					//var js = string.Format ("SetLabelText('{0}');", prepended);

					//view.LoadUrl ("javascript:" + js);
					view.LoadUrl (PublicAcivity.StarUrl);

				}

			
				return true;
			}

		}



	}




}