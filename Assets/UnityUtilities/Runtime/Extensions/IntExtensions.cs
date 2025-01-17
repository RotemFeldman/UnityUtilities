namespace UnityUtilities.Extensions
{
	public static class IntExtensions
	{
		public static int Clamp(this int value, int min, int max)
			=> (value < min) ? min : (value > max) ? max : value;
		
    
		public static int ClampMin(this int value, int min)
			=> (value < min) ? min : value;
		
    
		public static int ClampMax(this int value, int max)
			=> (value > max) ? max : value;
		
            		
	}
}