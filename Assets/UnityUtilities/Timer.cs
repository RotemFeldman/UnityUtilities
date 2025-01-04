using System;

namespace UnityUtilities
{
	public abstract class Timer
	{
		public Action OnTimerStart = delegate { };
		public Action OnTimerStop = delegate { };
		
		public bool IsRunning { get; protected set; }

		protected Timer(bool registerToManager = true)
		{
			if (registerToManager)
			{
				TimerManager.Instance.RegisterTimer(this);
			}
		}

		~Timer()
		{
			TimerManager.Instance.UnregisterTimer(this);
		}

		public abstract void Tick(float deltaTime);

		public virtual void Start()
		{
			if (!IsRunning)
			{
				IsRunning = true;
				OnTimerStart.Invoke();
			}
		}

		public virtual void Stop()
		{
			if (IsRunning)
			{
				IsRunning = false;
				OnTimerStop.Invoke();
			}
		}
		
		public void Resume() => IsRunning = true;
		public void Pause() => IsRunning = false;

		public abstract void Reset();
	}

	public class CountdownTimer : Timer
	{
		public Action OnTimerComplete = delegate { };
		public float TimeRemaining => _currentTime;
		public float Progress => 1 - (_currentTime / _initialTime);
		
		private float _initialTime;
		private float _currentTime;

		public CountdownTimer(float initialTime, bool registerToManager = true) : base(registerToManager)
		{
			_initialTime = initialTime;
			_currentTime = initialTime;
		}
		
		public override void Tick(float deltaTime)
		{
			if (IsRunning)
			{
				_currentTime -= deltaTime;
				if (_currentTime <= 0)
				{
					Complete();
				}
			}
		}

		public override void Reset()
		{
			_currentTime = _initialTime;
		}

		private void Complete()
		{
			_currentTime = 0;
			IsRunning = false;
			OnTimerComplete.Invoke();
		}
	}

	public class Stopwatch : Timer
	{
		
		public Stopwatch(bool registerToManager = true) : base(registerToManager) {}
		
		public float Time => _value;
		private float _value = 0f;
		
		public override void Tick(float deltaTime)
		{
			if (IsRunning)
			{
				_value += deltaTime;
			}
		}

		public override void Reset() => _value = 0f;
	}

	
}