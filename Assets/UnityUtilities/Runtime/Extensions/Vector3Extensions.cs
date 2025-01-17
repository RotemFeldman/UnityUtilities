using UnityEngine;

namespace UnityUtilities.Extensions
{
	public static class Vector3Extensions
	{
		public static Vector3 Add(this Vector3 vector, float x = 0, float y = 0, float z = 0)
			=> new Vector3(vector.x + x, vector.y + y, vector.z + z);
		
		
		public static Vector3 With(this Vector3 vector, float? x = null, float? y = null, float? z = null) 
			=> new Vector3(x ?? vector.x, y ?? vector.y, z ?? vector.z);
		
		
		public static bool InRangeOf(this Vector3 current, Vector3 target, float range)
			=> (current - target).sqrMagnitude <= range * range;
		
		
		public static Vector3 ToVector3(this Vector2 v2) 
			=> new Vector3(v2.x, 0, v2.y);
		
		
	}
}