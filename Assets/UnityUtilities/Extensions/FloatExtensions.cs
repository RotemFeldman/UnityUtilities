namespace UnityUtilities.Extensions
{
	public static class FloatExtensions
	{
		
		public static float Clamp(this float value, float min, float max)
			=> (value < min) ? min : (value > max) ? max : value;
		

		public static float ClampMin(this float value, float min)
			=> (value < min) ? min : value;
		

		public static float ClampMax(this float value, float max)
			=> (value > max) ? max : value;
		

		public static float Clamp01(this float value)
			=> (value < 0.0f) ? 0.0f : (value > 1.0f) ? 1.0f : value;
		


		
		
	}
}