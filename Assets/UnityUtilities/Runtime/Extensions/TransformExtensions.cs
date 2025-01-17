using System.Collections.Generic;
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
		
		public static bool InRangeOf(this Transform source, Transform target, float maxDistance, float maxAngle = 360f) 
		{
			Vector3 directionToTarget = (target.position - source.position).With(y: 0);
			return directionToTarget.magnitude <= maxDistance && Vector3.Angle(source.forward, directionToTarget) <= maxAngle / 2;
		}

		
		public static IEnumerable<Transform> Children(this Transform parent)
		{
			foreach (Transform child in parent) {
				yield return child;
			}
		}
		
		public static void ForEveryChild(this Transform parent, System.Action<Transform> action)
		{
			for (var i = parent.childCount - 1; i >= 0; i--) {
				action(parent.GetChild(i));
			}
		}
		
		public static void DisableChildren(this Transform parent)
			=> parent.ForEveryChild(child => child.gameObject.SetActive(false));
		
		
		public static void EnableChildren(this Transform parent) 
			=> parent.ForEveryChild(child => child.gameObject.SetActive(true));
		
		
		public static void DestroyChildren(this Transform parent)
			=> parent.ForEveryChild(child => Object.Destroy(child.gameObject));
		
		
		public static void DestroyChildrenImmediate(this Transform parent) 
			=> parent.ForEveryChild(child => Object.DestroyImmediate(child.gameObject));
		
	}
}