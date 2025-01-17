using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityUtilities;
using UnityUtilities.Runtime;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
	public class Cube : MonoBehaviour , IGameObjectPoolCallbackReceiver<Cube>
	{
		public Action AboutToDestroy; 
		
		private GameObjectPool<Cube> _pool;
		private CountdownTimer _countdownTimer;
		private Vector3 size = new Vector3(20f, 20f, 20f);
		private Rigidbody rb;
		
		public void SetPool(GameObjectPool<Cube> pool)
		{
			_pool = pool;
		}

		public void OnCreate()
		{
			rb = GetComponent<Rigidbody>();
			var renderer = GetComponent<MeshRenderer>();
			renderer.material = new Material(renderer.material);
			renderer.material.color = new Color(Random.value, Random.value, Random.value);
			gameObject.SetActive(false);
			
		}
		
		public Vector3 GenerateRandomPointInCube(Vector3 center, Vector3 size)
		{
			float x = Random.Range(-size.x / 2, size.x / 2);
			float y = Random.Range(-size.y / 2, size.y / 2);
			float z = Random.Range(-size.z / 2, size.z / 2);

			return center + new Vector3(x, y, z);
		}

		public void OnGet()
		{
			gameObject.SetActive(true);
			transform.position = GenerateRandomPointInCube(Vector3.zero, size);
			rb.linearVelocity = Vector3.zero;
			
			transform.Rotate(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));
			
			_countdownTimer = new CountdownTimer(Random.Range(0.5f,2f));
			_countdownTimer.OnTimerComplete += () => { _pool.Release(this); };
			_countdownTimer.Start();
		}

		public void OnRelease()
		{
			gameObject.SetActive(false);
		}

		public void Init()
		{
			var renderer = GetComponent<MeshRenderer>();
			renderer.material = new Material(renderer.material);
			renderer.material.color = new Color(Random.value, Random.value, Random.value);
			
			transform.position = GenerateRandomPointInCube(Vector3.zero, size);
			
			transform.Rotate(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));
			
			_countdownTimer = new CountdownTimer(Random.Range(0.5f,2f));
			_countdownTimer.OnTimerComplete += () => { Destroy(gameObject); };
			_countdownTimer.Start();
		}

		public void OnDestroy()
		{
			AboutToDestroy?.Invoke();
		}
		
		
		
		
	}
}