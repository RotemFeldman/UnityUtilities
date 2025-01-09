using System;
using UnityEngine;
using UnityUtilities.Extensions;

public class LookAt : MonoBehaviour
{
	private Camera cam;

	private void Start()
	{
		cam = Camera.main;
	}

	private void Update()
	{
		var lookAt = transform.rotation.LookAtMouse2D(transform.position,cam);
		transform.rotation = Quaternion.Lerp(transform.rotation,lookAt,Time.deltaTime * 5f);
		
	}

	
}
