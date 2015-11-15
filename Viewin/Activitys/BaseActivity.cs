using System;
using Android.App;
using Android.Graphics.Drawables;
using Android.Content;
using Newtonsoft.Json;

namespace Viewin
{
	public class BaseActivity:Activity
	{
		public Bussines _Bussines = Bussines.Instance;

		public ProgressDialog _ProgressDialog;

		public  string Name { get; set; }

		public  string LastName { get; set; }

		public  string Email { get; set; }

		public  string Genero { get; set; }

		public  string AccessToken { get; set; }



		public BaseActivity ()
		{
		}




		public void SetUserDatos (Users datos)
		{

			var prefs = GetSharedPreferences (Application.PackageName, FileCreationMode.Private);
			var Edit = prefs.Edit ();
		
			Edit.PutString ("Datos_User", JsonConvert.SerializeObject(datos));	
		


			Edit.Commit ();

		}


		public Users GetUserDatos ()
		{

			var prefs = GetSharedPreferences (this.PackageName, FileCreationMode.Private);
			return JsonConvert.DeserializeObject<Users> (prefs.GetString ("Datos_User", ""));
		}



		public void SetCountViews (int nViews)
		{


			var prefs = GetSharedPreferences (Application.PackageName, FileCreationMode.Private);
			var Edit = prefs.Edit ();

			Edit.PutInt ("CountViews",nViews);	
			Edit.Commit ();

		}


		public int GetCountViews ()
		{

			var prefs = GetSharedPreferences (this.PackageName, FileCreationMode.Private);
			return prefs.GetInt  ("CountViews", 0);



		}


	}
}

