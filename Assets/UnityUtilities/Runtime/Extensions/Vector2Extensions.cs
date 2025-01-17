using UnityEngine;

namespace UnityUtilities.Extensions
{
	public static class Vector2Extensions
	{
		public static Vector2 Add(this Vector2 v1, float x = 0, float y = 0)
			=> new Vector2(v1.x + x, v1.y + y);
		
		
		public static Vector2 Add(this Vector2 v1, Vector2 v2)
         	=> new Vector2(v1.x + v2.x, v1.y + v2.y);
        

		public static Vector2 With(this Vector2 vector, float? x = null, float? y = null)
			=> new Vector2(x ?? vector.x, y ?? vector.y);
		
		
		public static bool InRangeOf(this Vector2 current, Vector2 target, float range)
			=> (current - target).sqrMagnitude <= range * range;
		
	}
}