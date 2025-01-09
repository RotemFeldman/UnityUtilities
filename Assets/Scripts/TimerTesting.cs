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
        private LoopingCountdownTimer timer;

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
            timer = new LoopingCountdownTimer(3f);
            
        }

        private void Start()
        {
            
            timer.Start();
            
        }
        

        void PrintStart()
        {
            Debug.Log("Timer Started");
        }
        
        void PrintEnd()
        {
            Debug.Log(Time.time);
            timer.SetNewTime(Time.time);
        }
    }
    
}