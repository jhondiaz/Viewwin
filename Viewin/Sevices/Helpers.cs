using System;
using System.Text.RegularExpressions;
using System.Text;

namespace Viewin
{
	public class Helpers
	{
		public Helpers ()
		{
			//e9fd06  144D67 2bbb9c
		}



		public const string ColorHeader = ("#0082FF");

		public static Boolean ValidaEmail (String email)
		{
			String expresion;
			expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
			if (Regex.IsMatch (email, expresion)) {
				if (Regex.Replace (email, expresion, String.Empty).Length == 0) {
					return true;
				} else {
					return false;
				}
			} else {
				return false;
			}
		}


		public static string EncriptarSHA1(string sCadena)
		{
			// Método para codificar
			System.Security.Cryptography.SHA1 sha1 = System.Security.Cryptography.SHA1Managed.Create();

			ASCIIEncoding encoding = new ASCIIEncoding();
			byte[] stream = null;
			StringBuilder codificada = new StringBuilder();
			stream = sha1.ComputeHash(encoding.GetBytes(sCadena));

			// Genera cadena codificada
			for (int i = 0; i < stream.Length; i++) codificada.AppendFormat("{0:x2}", stream[i]);


			return codificada.ToString();
		}

	}
}

