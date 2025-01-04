using System.Collections.Generic;
using UnityEngine;

namespace UnityUtilities
{
    public class TimerManager : MonoBehaviour
    {
        private static TimerManager _instance;
        private List<Timer> _timers = new();

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
            if (!_timers.Contains(timer))
            {
                _timers.Add(timer);
            }
        }
		
        public void UnregisterTimer(Timer timer)
        {
            _timers.Remove(timer);
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;
            foreach (var timer in _timers)
            {
                timer.Tick(deltaTime);
            }
        }
    }
}