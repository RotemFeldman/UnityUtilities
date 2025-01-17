using System.Collections.Generic;
using UnityEngine;

namespace UnityUtilities.Runtime
{
	public class TimerManager : MonoBehaviour
	{
		private static TimerManager _instance;
		private HashSet<Timer> _activeTimers = new();
		private List<Timer> _timersToAdd = new();
		private List<Timer> _timersToRemove = new();

		public static TimerManager Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = FindFirstObjectByType<TimerManager>();
					if (_instance == null)
					{
						GameObject go = new GameObject("TimerManager");
						_instance = go.AddComponent<TimerManager>();
						DontDestroyOnLoad(go);
					}
				}
				return _instance;
			}
		}
		
		public void RegisterTimer(Timer timer)
		{
			if (timer != null)
			{
				_timersToAdd.Add(timer);
			}
		}
		
		public void UnregisterTimer(Timer timer)
		{
			if (timer != null)
			{
				_timersToRemove.Remove(timer);
			}
		}

		private void Update()
		{
			float deltaTime = Time.deltaTime;
			foreach (var timer in _activeTimers)
			{
				timer.Tick(deltaTime);
			}

			foreach (var timer in _timersToAdd)
			{
				_activeTimers.Add(timer);
			}
			_timersToAdd.Clear();

			foreach (var timer in _timersToRemove)
			{
				_activeTimers.Remove(timer);
			}
			_timersToRemove.Clear();
		}
	}
}