using System;
using System.Threading.Tasks;
using Android.Util;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace Viewin
{
	public class Bussines:IDisposable
	{

		static readonly Bussines _instance = new Bussines();
		public static Bussines Instance
		{
			get
			{
				return _instance;
			}
		}


		public Bussines ()
		{
		}


		public void Dispose()
		{ 

			GC.SuppressFinalize(this);           
		}

		public async Task<ResultMsg<Users>> SetUser( Users user)
		{
		
			try {

				using (var Client = new  ApiClient ()) {

					var entity = JsonConvert.SerializeObject (user);

					var Result = await Client.PostAsync ("User/Register", new StringContent (entity, UnicodeEncoding.UTF8, "application/json"));

					var ResultJson = Result.Content.ReadAsStringAsync ().Result;

					var dato = JsonConvert.DeserializeObject<ResultMsg<Users>> (ResultJson);

					Client.Dispose ();

					return dato;

				}

			} catch (Exception) {

				return null;

			}


		}

		public async Task<bool> ValidaEmail(string email)
		{

			try {
				using (var Client = new  ApiClient ()) {


					var Result = await Client.GetAsync ("User/ValidaEmail?email="+email);

					var ResultJson = Result.Content.ReadAsStringAsync ().Result;

					var dato = JsonConvert.DeserializeObject<bool> (ResultJson);

					return dato;

				}
			} catch (Exception) {

				return true;
			}
		}

		public async Task<ResultMsg<Users>> Login(string email,string pwd)
		{

			try {
				using (var Client = new  ApiClient ()) {


					var Result = await Client.GetAsync ("User/Login?email="+email+"&pwd="+pwd);

					var ResultJson = Result.Content.ReadAsStringAsync ().Result;

					var dato = JsonConvert.DeserializeObject<ResultMsg<Users>> (ResultJson);

					return dato;

				}
			} catch (Exception) {

				return null;
			}
		}



		public async void UpdateGCM(string email ,string RegisterID)
		{

			try {
				using (var Client = new  ApiClient ()) {

					await Client.GetAsync ("User/UpdateGcm?email="+email+"&gcmid="+RegisterID);
				}
			} catch (Exception) {

				return;
			}
		}


		public async Task<int>   UpdatePoint(string id ,string idp, string key)
		{

			try {
				using (var Client = new  ApiClient ()) {
					
					var Result = await Client.GetAsync ("User/UpdatePoint?id="+id+"&idp="+idp+"&key="+key);

					var ResultJson = Result.Content.ReadAsStringAsync ().Result;

					var dato = JsonConvert.DeserializeObject<int> (ResultJson);

					return dato;

				}
			} catch (Exception) {

				return 0;
			}
		}


		public async Task<List<Categorys>> GetCategorys()
		{

			try {
				using (var Client = new  ApiClient ()) {

					var Result = await Client.GetAsync ("User/GetAllCategorys");

					var ResultJson = Result.Content.ReadAsStringAsync ().Result;

					var dato = JsonConvert.DeserializeObject<List<Categorys>> (ResultJson);

					return dato;

				}
			} catch (Exception) {

				return null;
			}
		}



		public async Task<List<PublicViews>> GetAllPublicViews()
		{
			try {
				using (var Client = new  ApiClient ()) {


					var Result = await Client.GetAsync ("User/GetAllPublicViews");

					var ResultJson = Result.Content.ReadAsStringAsync ().Result;

					var dato = JsonConvert.DeserializeObject<List<PublicViews>> (ResultJson);

					return dato;

				}
			} catch (Exception) {

				return null;
			}
		}


		public async Task<List<Grills>> GetAllGrills(string id)
		{
			try {
				using (var Client = new  ApiClient ()) {


					var Result = await Client.GetAsync ("Grill/GetAllGrills?idCategory="+id);

					var ResultJson = Result.Content.ReadAsStringAsync ().Result;

					var dato = JsonConvert.DeserializeObject<List<Grills>> (ResultJson);

					return dato;

				}
			} catch (Exception) {

				return null;
			}
		}


		public async Task<PublicViews> GetPublicViews(string id)
		{
			try {
				using (var Client = new  ApiClient ()) {


					var Result = await Client.GetAsync ("Grill/GetPublicViews?id="+id);

					var ResultJson = Result.Content.ReadAsStringAsync ().Result;

					var dato = JsonConvert.DeserializeObject<PublicViews> (ResultJson);

					return dato;

				}
			} catch (Exception) {

				return null;
			}
		}


		public async Task<PublicViews> GetPublicViewsByIdUser(string id,string IdCategory,string key)
		{
			try {
				using (var Client = new  ApiClient ()) {


					var Result = await Client.GetAsync ("Grill/GetPublicViewsByIdUser?id="+id+"&idc="+IdCategory+"&key="+key);

					var ResultJson = Result.Content.ReadAsStringAsync ().Result;

					var dato = JsonConvert.DeserializeObject<PublicViews> (ResultJson);

					return dato;

				}
			} catch (Exception) {

				return null;
			}
		}



	}

	public  class ApiClient:HttpClient
	{
		public ApiClient ()
		{
			//			var encoder = Encoding.GetEncoding ("ISO-8859-1");
			//			var credentials = Convert.ToBase64String (encoder.GetBytes (string.Format ("{0}:{1}", "Vonline", "Vonline")));

			#if DEBUG

			this.BaseAddress = new Uri ("http://viewin.azurewebsites.net/api/");
			this.BaseAddress = new Uri ("http://192.168.0.28/viewin/api/");
			#else
			this.BaseAddress = new Uri ("http://viewin.azurewebsites.net/api/");
			#endif

			//			this.DefaultRequestHeaders.Add ("User-Agent", "Vonline 1.0");
			//			this.DefaultRequestHeaders.Accept.Clear ();
			//			this.DefaultRequestHeaders.Accept.Add (new MediaTypeWithQualityHeaderValue ("application/json"));
			//			this.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue ("Basic", credentials);
		}

	}

}

