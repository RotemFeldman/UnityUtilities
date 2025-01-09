using System;
using UnityEngine;
using UnityEngine.Pool;

namespace UnityUtilities
{
	public class GameObjectPool<T> where T : MonoBehaviour, IGameObjectPoolCallbackReceiver<T>
	{
		private readonly ObjectPool<T> _objectPool;
		private readonly Transform _parent;

		public GameObjectPool(T prefab,Transform parent = null, int defaultCapacity = 10, int maxSize = 10000, bool collectionCheck = true)
		{
			if (prefab == null)
				throw new ArgumentNullException(nameof(prefab));

			_objectPool = new ObjectPool<T>(
				createFunc: () =>
				{
					T instance = UnityEngine.Object.Instantiate(prefab);
					instance.OnCreate(); 
					instance.SetPool(this);
					if(parent != null) instance.transform.parent = parent;
					
					return instance;
				},
				actionOnGet: obj => obj.OnGet(),
				actionOnRelease: obj => obj.OnRelease(),
				actionOnDestroy: obj =>
				{
					obj.OnDestroy();
					UnityEngine.Object.Destroy(obj.gameObject);
				},
				defaultCapacity: defaultCapacity,
				maxSize: maxSize,
				collectionCheck: collectionCheck
			);
			
			
		}
		
		public int CountAll => _objectPool.CountAll;
		public int CountActive => _objectPool.CountActive;

		public T Get() => _objectPool.Get();
		public void Release(T obj) => _objectPool.Release(obj);
		public void Dispose() => _objectPool.Dispose();
	}

	public interface IGameObjectPoolCallbackReceiver<T> where T : MonoBehaviour, IGameObjectPoolCallbackReceiver<T>
	{
		
		void SetPool(GameObjectPool<T> pool);
		void OnCreate(); 
		void OnGet();
		void OnRelease();
		void OnDestroy();
		

	}
}