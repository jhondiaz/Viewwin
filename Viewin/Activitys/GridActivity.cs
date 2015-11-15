
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

namespace Viewin
{
	[Activity (Label = "Categorias")]			
	public class GridActivity : BaseActivity
	{
		
		GridView Gridview;
		List<Categorys> ListCategorys;
		protected async override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.GridActivity);
			ColorDrawable colorDrawable = new ColorDrawable (Color.ParseColor (Helpers.ColorHeader));
			ActionBar.SetBackgroundDrawable (colorDrawable); 


			Gridview = FindViewById<GridView> (Resource.Id.GridView);

			ListCategorys = await _Bussines.GetCategorys ();

			Gridview.Adapter = new GridAdapter (this, ListCategorys);
			Gridview.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs args) {


				Toast.MakeText (this, args.Position.ToString (), ToastLength.Short).Show ();

				var datosActivity = new Intent (this, typeof(PublicAcivity));

				if(ListCategorys!=null&& ListCategorys.Count!=0){
					
					datosActivity.PutExtra("IdCategory",ListCategorys[args.Position].Id);

				}
		


				StartActivity (datosActivity);


			};

		}
	}
}

