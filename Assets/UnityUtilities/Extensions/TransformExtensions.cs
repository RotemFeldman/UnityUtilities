using UnityEngine;

namespace UnityUtilities.Extensions
{
	public static class TransformExtensions
	{
		public static void LookAt2D(this Transform transform, Vector3 lookAtPosition)
		{
			lookAtPosition.z = transform.position.z;
			Vector3 dir = lookAtPosition - transform.position;
			float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		}

		public static void LookAtMouse2D(this Transform transform, Camera camera = null)
		{
			Vector3 dir = camera ? camera.ScreenPointToRay(Input.mousePosition).direction : Camera.main.ScreenPointToRay(Input.mousePosition).direction;
			LookAt2D(transform, dir);
		}
	}
}