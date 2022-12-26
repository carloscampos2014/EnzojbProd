namespace EnzojbProd.App.Extensions
{
    public static class StringExtensions
    {
		public static string SoNumeros(this string parametro, bool virgula)
		{
			string resultado = string.Empty;
			if (parametro != string.Empty)
			{
				for (int intContador = 0; intContador < parametro.Length; intContador++)
				{
					char letra = Convert.ToChar(parametro.Substring(intContador, 1));
					if (virgula)
					{
						if (char.IsDigit(letra) || letra == ',')
						{
							resultado += letra.ToString();
						}
					}
					else
					{
						if (char.IsDigit(letra))
						{
							resultado += letra.ToString();
						}
					}
				}
			}
			return resultado;
		}
	}
}
