using System;
using System.Diagnostics;
using UnityEngine;
using UnityUtilities;
using UnityUtilities.UnityUtilities;
using Debug = UnityEngine.Debug;
using Stopwatch = UnityUtilities.Stopwatch;

namespace DefaultNamespace
{
    public class TimerTesting : MonoBehaviour
    {
        private CountdownTimer timer;
        private Stopwatch stopwatch;

        private void OnEnable()
        {
            timer.OnTimerStart += PrintStart;
            timer.OnTimerComplete += PrintEnd;
        }

        private void OnDisable()
        {
            timer.OnTimerStart -= PrintStart;
            timer.OnTimerComplete -= PrintEnd;
        }

        private void Awake()
        {
            timer = new CountdownTimer(3f);
            stopwatch = new Stopwatch();
        }

        private void Start()
        {
            stopwatch.Start();
            timer.Start();
            Debug.Log(timer.IsRunning);
        }
        

        void PrintStart()
        {
            Debug.Log("Timer Started");
        }
        
        void PrintEnd()
        {
            Debug.Log(stopwatch.Time);
        }
    }
    
}