using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Pool;
using UnityUtilities.Runtime;

namespace UnityUtilities
{
	public class GameObjectPool<T> where T : PoolableMonoBehaviour<T>
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
					if(parent != null) instance.transform.parent = parent.transform;
					instance.SetPool(this);
					instance.OnCreate(); 
					return instance;
				},
				actionOnGet: obj => obj.OnGet(),
				actionOnRelease: obj => obj.OnRelease(),
				actionOnDestroy: obj =>
				{
					UnityEngine.Object.Destroy(obj.gameObject);
				},
				defaultCapacity: defaultCapacity,
				maxSize: maxSize,
				collectionCheck: collectionCheck
			);
		}
		
		public Action<T> OnObjectGet = delegate { };
		public Action<T> OnObjectRelease = delegate { };
		
		public int CountAll => _objectPool.CountAll;
		public int CountActive => _objectPool.CountActive;
		public void Clear() => _objectPool.Clear();

		public T Get()
		{
			var obj =	_objectPool.Get();
			OnObjectGet.Invoke(obj);
			return obj;
		}

		public void Release(T obj)
		{
			if (!obj.IsReleased)
			{
				OnObjectRelease.Invoke(obj);
				_objectPool.Release(obj);
			}
		}
	}
}