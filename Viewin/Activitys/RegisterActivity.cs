
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
	[Activity (Label = "Nuevo Usuario")]			
	public class RegisterActivity : BaseActivity
	{
		EditText TxtName;
		EditText TxtLastName;
		EditText TxtEmail;
		EditText TxtEdad;
		EditText TxtPassword;
		Button BntContinuar;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.RegisterActivity);
			ColorDrawable colorDrawable = new ColorDrawable (Color.ParseColor (Helpers.ColorHeader));
			ActionBar.SetBackgroundDrawable (colorDrawable); 

			TxtName = FindViewById<EditText> (Resource.Id.TxtName);
			TxtLastName = FindViewById<EditText> (Resource.Id.TxtLastName);
			TxtEmail = FindViewById<EditText> (Resource.Id.TxtEmail);
			TxtEdad = FindViewById<EditText> (Resource.Id.TxtEdad);
			TxtPassword = FindViewById<EditText> (Resource.Id.TxtPassword);

			BntContinuar = FindViewById<Button> (Resource.Id.BntContinuar);
			BntContinuar.Click+= CuntinuarClick;

			try {
				Account[] accounts = AccountManager.Get (this).GetAccountsByType ("com.google");

				foreach (Account account in accounts) {

					if (!string.IsNullOrEmpty (account.Name)) {
						TxtEmail.Text = account.Name;
						return;
					}

					//accountsList.add(item);
				}
			} catch (Exception) {
				return;
			}

		}

		async	void CuntinuarClick (object sender, EventArgs e)
		{


			if (TxtName.Text == string.Empty) {
				TxtName.SetError ("Digite su Nombre", null);
				TxtName.RequestFocus ();
				return;
			}

			if (TxtLastName.Text == string.Empty) {
				TxtLastName.SetError ("Digite su Apellido", null);
				TxtLastName.RequestFocus ();
				return;
			}


			if (TxtEmail.Text == string.Empty) {
				TxtEmail.SetError ("Digite su Correo", null);
				TxtEmail.RequestFocus ();
				return;
			}

			if (!Helpers.ValidaEmail(TxtEmail.Text)) {
				TxtEmail.SetError ("Digite un Correo valido!", null);
				TxtEmail.RequestFocus ();
				return;
			}



			if (TxtEdad.Text == string.Empty) {
				TxtEdad.SetError ("Digite su Edad", null);
				TxtEdad.RequestFocus ();
				return;
			}

			if (TxtPassword.Text == string.Empty) {
				TxtPassword.SetError ("Digite su Contraseña", null);
				TxtPassword.RequestFocus ();
				return;
			}

			_ProgressDialog = ProgressDialog.Show (this, "Por favor espera...", "Procesando info...", true);	

			var result= await _Bussines.ValidaEmail (this.TxtEmail.Text);

			_ProgressDialog.Dismiss ();

			if (result) {
				Toast.MakeText (ApplicationContext, "el Correo "+this.TxtEmail.Text+ " ya esta registrado", ToastLength.Long).Show ();
				return;
			}


			var datosActivity= new Intent(this,typeof(OperatorActivity));

			datosActivity.PutExtra("Name",TxtName.Text);
			datosActivity.PutExtra("LastName",TxtLastName.Text);
			datosActivity.PutExtra("Email",TxtEmail.Text);
			datosActivity.PutExtra("Edad",TxtEdad.Text);
			datosActivity.PutExtra("Password",TxtPassword.Text);
			datosActivity.PutExtra("Gender",this.Genero);
			datosActivity.PutExtra("AccessToken",this.AccessToken);

			StartActivity(datosActivity);

		}




		protected override void OnStart ()
		{
			base.OnStart ();

			this.Name =	this.Intent.GetStringExtra ("Name");
			this.LastName =	this.Intent.GetStringExtra ("LastName");
			this.Email =	this.Intent.GetStringExtra ("Email");
			this.Genero =	this.Intent.GetStringExtra ("Genero");
			this.AccessToken =	this.Intent.GetStringExtra ("AccessToken");

			if (!string.IsNullOrEmpty (this.Name))
				TxtName.Text = this.Name;

			if (!string.IsNullOrEmpty (this.LastName))
				TxtLastName.Text = this.LastName;

			if (!string.IsNullOrEmpty (this.Email))
				TxtEmail.Text = this.Email;

		}




	}
}

