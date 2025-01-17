using UnityEngine;

namespace UnityUtilities.Runtime
{
	public class PoolableMonoBehaviour<T> : MonoBehaviour  where T : PoolableMonoBehaviour<T>
	{
		public bool IsReleased => isReleased;
		
		protected bool isReleased;
		protected GameObjectPool<T> pool;
		public virtual void SetPool(GameObjectPool<T> pool)
		{
			this.pool = pool;
		}

		public virtual void OnCreate()
		{
			isReleased = true;
			gameObject.SetActive(false);
		}

		public virtual void OnGet()
		{
			gameObject.SetActive(true);
			isReleased = false;
		}

		public virtual void OnRelease()
		{
			isReleased = true;
			gameObject.SetActive(false);
		}
	}
}