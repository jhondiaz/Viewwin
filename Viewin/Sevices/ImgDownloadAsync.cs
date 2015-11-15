using System;
using System.Net;
using System.Threading.Tasks;
using Android.Util;
using System.IO;
using Android.Graphics;
using Android.Widget;

namespace Viewin
{
	public class ImgDownloadAsync
	{
		WebClient webClient;
		string documentsPath;
		string localPath;
		Bitmap teamBitmap;

		public async Task<Bitmap> downloadAsync (string uri)
		{
		 
			if (uri == null)
				return null;

			int index = uri.LastIndexOf ("/");
			string localFilename = uri.Substring (index + 1);
			webClient = new WebClient ();
			var url = new Uri (uri);
			byte[] bytes = null;


			try {

				bytes = await webClient.DownloadDataTaskAsync (url);
			} catch (TaskCanceledException Ex) {
				Log.Error ("downloadAsync:", Ex.Message);
				return null;
			} catch (Exception Ex) {
			
				Log.Error ("downloadAsync:", Ex.Message);

				return null;
			}

			string documentsPath = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal);	

			string localPath = System.IO.Path.Combine (documentsPath, localFilename);
		

			//Sive the Image using writeAsync
			FileStream fs = new FileStream (localPath, FileMode.OpenOrCreate);
			await fs.WriteAsync (bytes, 0, bytes.Length);

			//Console.WriteLine("localPath:"+localPath);
			fs.Close ();


			BitmapFactory.Options options = new BitmapFactory.Options ();
			options.InJustDecodeBounds = true;
			await BitmapFactory.DecodeFileAsync (localPath, options);

			//	options.InSampleSize = options.OutWidth > options.OutHeight ? options.OutHeight / imageview.Height : options.OutWidth / imageview.Width;
			options.InJustDecodeBounds = false;

			Bitmap bitmap = await BitmapFactory.DecodeFileAsync (localPath, options);



			return (bitmap);

		
		}



		public 	async void DownloadAsyncBitmap (string url, ImageView imageView)
		{

			if (url == null)
				return;


			try {	

				int index = url.LastIndexOf ("/");
				string localFilename = url.Substring (index + 1);
				var webClient = new WebClient ();
				var uri = new Uri (url);



				documentsPath = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal);	

				localPath = System.IO.Path.Combine (documentsPath, localFilename);


				var localImage = new Java.IO.File (localPath);

				if (localImage.Exists ()) {

					teamBitmap = await BitmapFactory.DecodeFileAsync (localImage.AbsolutePath);
					imageView.SetImageBitmap (teamBitmap);

				} else {

					byte[] bytes = null;		
					bytes = await webClient.DownloadDataTaskAsync (uri);

					FileStream fs = new FileStream (localPath, FileMode.OpenOrCreate);
					await fs.WriteAsync (bytes, 0, bytes.Length);
					fs.Close ();	

					teamBitmap = await BitmapFactory.DecodeByteArrayAsync (bytes, 0, bytes.Length);
					imageView.SetImageBitmap (teamBitmap);

				}




			} catch (TaskCanceledException Ex) {
				Log.Error ("DownloadAsyncBitmap:", Ex.Message);
				return;
			} catch (Exception Ex) {
				Log.Error ("DownloadAsyncBitmap:", Ex.Message);
				return;
			}



		}




		public 	async void DownloadHistory (string url, ImageView imageView,ProgressBar Pbar)
		{

			if (url == null)
				return;


			try {	

				int index = url.LastIndexOf ("/");
				string localFilename = url.Substring (index + 1);
				var webClient = new WebClient ();
				var uri = new Uri (url);
				webClient.DownloadProgressChanged +=   (sender, e) => Pbar.Progress=e.ProgressPercentage;


				documentsPath = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal);	

				localPath = System.IO.Path.Combine (documentsPath, localFilename);


				byte[] bytes = null;		
				bytes = await webClient.DownloadDataTaskAsync (uri);

				FileStream fs = new FileStream (localPath, FileMode.OpenOrCreate);
				await fs.WriteAsync (bytes, 0, bytes.Length);
				fs.Close ();	

				BitmapFactory.Options options = new BitmapFactory.Options ();
				options.InJustDecodeBounds = false;
			

				teamBitmap = await BitmapFactory.DecodeFileAsync (localPath, options);


				imageView.SetImageBitmap (teamBitmap);

			



			} catch (TaskCanceledException Ex) {
				Log.Error ("DownloadAsyncBitmap:", Ex.Message);
				return;
			} catch (Exception Ex) {
				Log.Error ("DownloadAsyncBitmap:", Ex.Message);
				return;
			}



		}

		void HandleDownloadProgressChanged (object sender, DownloadProgressChangedEventArgs e)
		{
			
		}
	}
}

