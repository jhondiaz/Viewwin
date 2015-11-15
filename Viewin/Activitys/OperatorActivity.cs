
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
using Android.Telephony;

namespace Viewin
{
	[Activity (Label = "Operadores")]			
	public class OperatorActivity : BaseActivity
	{

		Button BtnOperator;
		Button BntContinuar;
		public	TextView TxtOperator;
		public	TextView TxtMobil;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.OperatorActivity);

			ColorDrawable colorDrawable = new ColorDrawable (Color.ParseColor (Helpers.ColorHeader));
			ActionBar.SetBackgroundDrawable (colorDrawable); 

			BtnOperator = FindViewById<Button> (Resource.Id.BtnOperator);
			TxtOperator = FindViewById<TextView> (Resource.Id.TxtOperator);
			BntContinuar = FindViewById<Button> (Resource.Id.BntContinuar);

			TxtMobil= FindViewById<TextView> (Resource.Id.TxtMobil);

			BntContinuar.Click += ContinuarClick;
			BntContinuar.Enabled= false;
			BtnOperator.Click += (sender, e) => {


				List<string> Item = new List<string> ();


				Item.Add ("CLARO");
				Item.Add ("TIGO");
				Item.Add ("MOVISTAR");
				Item.Add ("UFF!");
				Item.Add ("AVANTEL");
				Item.Add ("VIRGIN");

				AlertDialog.Builder builder = new AlertDialog.Builder (this);
				builder.SetTitle ("LISTA  DE OPERADORES");
				builder.SetSingleChoiceItems (Item.ToArray (), -1, delegate(object  Sender, DialogClickEventArgs  E) {

					TxtOperator.Text = Item [E.Which];
					BntContinuar.Enabled= true;

				});

				builder.SetPositiveButton ("Ok", delegate { 

				

				});
				builder.Show ();


			};

		


		}


		async	void ContinuarClick (object sender, EventArgs e)
		{
			try{

				BntContinuar.Enabled= false;

				var telephonyManager = (TelephonyManager) GetSystemService(TelephonyService);
				var Imei = telephonyManager.DeviceId;

				if (TxtMobil.Text == string.Empty) {
					TxtMobil.SetError ("Digite tu Numero de Celular", null);
					TxtMobil.RequestFocus ();
					BntContinuar.Enabled= true;
					return;
				}

				if (TxtMobil.Text.Length != 10) {
					
					TxtMobil.SetError ("Digite Un Numero de Celular Valido", null);
					TxtMobil.RequestFocus ();
					BntContinuar.Enabled= true;
					return;
				}


				if (TxtOperator.Text == "Operador") {

					Toast.MakeText (ApplicationContext, "Seleccione su Operador", ToastLength.Long).Show ();
					TxtOperator.RequestFocus ();
					BntContinuar.Enabled= true;
					return;
				}



				var prefs = GetSharedPreferences (Application.PackageName, FileCreationMode.Private);

				var Gcmid = prefs.GetString ("Gcmid", null);

				_ProgressDialog = ProgressDialog.Show (this, "Por favor espera...", "Procesando info...", true);

				await	_Bussines.SetUser (new Users {
					Id=Guid.NewGuid().ToString(),
					Name = this.Intent.GetStringExtra ("Name").ToUpper(),
					LastName = this.Intent.GetStringExtra ("LastName").ToUpper(),
					Email = this.Intent.GetStringExtra ("Email"),
					Pwd = this.Intent.GetStringExtra ("Password"),
					Age=int.Parse(this.Intent.GetStringExtra ("Edad")),
					Gender=this.Intent.GetStringExtra ("Gender"),
					Gcmid = Gcmid,
					Operator=this.TxtOperator.Text,
					mobil =this.TxtMobil.Text,
					Imei=Imei

				});


				_ProgressDialog.Dismiss ();



				var datosActivity= new Intent(this,typeof(GridActivity));
				StartActivity(datosActivity);

				BntContinuar.Enabled= true;

			}catch(Exception){
				BntContinuar.Enabled= true;
				return;
			}

		}


	}





}

