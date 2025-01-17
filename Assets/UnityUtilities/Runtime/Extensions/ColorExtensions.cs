using UnityEngine;

namespace UnityUtilities.Extensions
{
	public static class ColorExtensions
	{
		public static Color SetAlpha(this Color color, float alpha)
			=> new(color.r, color.g, color.b, alpha);
		
		public static Color Add(this Color thisColor, Color otherColor)
			=> (thisColor + otherColor).Clamp01();
		
		public static Color Subtract(this Color thisColor, Color otherColor)
			=> (thisColor - otherColor).Clamp01();
		
		static Color Clamp01(this Color color) 
		{
			return new Color {
				r = Mathf.Clamp01(color.r),
				g = Mathf.Clamp01(color.g),
				b = Mathf.Clamp01(color.b),
				a = Mathf.Clamp01(color.a)
			};
		}
		
		public static Color Blend(this Color color1, Color color2, float ratio)
		{
			ratio = Mathf.Clamp01(ratio);
			return new Color(
				color1.r * (1 - ratio) + color2.r * ratio,
				color1.g * (1 - ratio) + color2.g * ratio,
				color1.b * (1 - ratio) + color2.b * ratio,
				color1.a * (1 - ratio) + color2.a * ratio
			);
		}
		
		public static Color Invert(this Color color)
			=> new(1 - color.r, 1 - color.g, 1 - color.b, color.a);
	}
}