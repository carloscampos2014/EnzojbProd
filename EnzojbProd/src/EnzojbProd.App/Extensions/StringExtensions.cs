using EnzojbProd.App.Enums;
using System.Reflection;

namespace EnzojbProd.App.Extensions
{
    public static class StringExtensions
    {
		public static string OnlyNumbers(this string parameter, bool comma)
		{
			string result = string.Empty;
			if (parameter != string.Empty)
			{
				for (int count = 0; count < parameter.Length; count++)
				{
					char letter = Convert.ToChar(parameter.Substring(count, 1));
					if (comma)
					{
						if (char.IsDigit(letter) || letter == ',')
						{
							result += letter.ToString();
						}
					}
					else
					{
						if (char.IsDigit(letter))
						{
							result += letter.ToString();
						}
					}
				}
			}
			return result;
		}
		
		public static string Description(this InventoryType type) =>
			type.GetEnumDescription();

		public static string Description(this InventoryStatus status) =>
			status.GetEnumDescription();

		internal static string GetEnumDescription<TEnum>(this TEnum @enum)
		{
			var field = typeof(TEnum).GetField(@enum.ToString());

			if (field == null)
			{
				return @enum.ToString();
			}

			var description = field.GetCustomAttribute<System.ComponentModel.DescriptionAttribute>();

			if (description == null)
			{
				return @enum.ToString();
			}

			return description.Description;
		}
	}
}
