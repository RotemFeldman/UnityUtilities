using UnityEngine;

namespace UnityUtilities.Extensions
{
	public static class QuaternionExtensions
	{
		public static Quaternion LookAt2D(this Quaternion quaternion, Vector3 lookAtPosition, Vector3 currentPosition)
		{
			lookAtPosition.z = currentPosition.z;
			Vector3 dir = lookAtPosition - currentPosition;
			float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg -90f;
			return Quaternion.AngleAxis(angle, Vector3.forward);
		}
		
		public static Quaternion LookAtMouse2D(this Quaternion quaternion,Vector3 currentPosition, Camera camera = null)
		{
			Vector3 mousePos = camera ? camera.ScreenToWorldPoint(Input.mousePosition) : Camera.main.ScreenToWorldPoint(Input.mousePosition);
			return quaternion.LookAt2D(mousePos, currentPosition);
		}
	}
}