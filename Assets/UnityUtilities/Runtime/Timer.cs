using System;

namespace UnityUtilities.Runtime
{
	public abstract class Timer : IDisposable
	{
		public Action OnTimerStart = delegate { };
		public Action OnTimerStop = delegate { };
		public bool IsRunning { get; protected set; }

		private bool _registered;
		protected Timer(bool registerToManager = true)
		{
			_registered = registerToManager;
		}
		~Timer()
		{
			TimerManager.Instance?.UnregisterTimer(this);
		}

		public void Dispose()
		{
			TimerManager.Instance?.UnregisterTimer(this);
		}
		
		public abstract void Tick(float deltaTime);
		
		public virtual void Start()
		{
			if (!IsRunning)
			{
				IsRunning = true;
				OnTimerStart.Invoke();
				
				if(_registered) TimerManager.Instance.RegisterTimer(this);
			}
		}
		
		public virtual void Stop()
		{
			if (IsRunning)
			{
				IsRunning = false;
				OnTimerStop.Invoke();
				
				if(_registered) TimerManager.Instance.UnregisterTimer(this);
			}
		}
		
		public void Resume() => IsRunning = true;
		public void Pause() => IsRunning = false;
		
		public abstract void Reset();
	}

	
	public class CountdownTimer : Timer
	{
		public Action OnTimerComplete = delegate { };
		public float TimeRemaining => CurrentTime;
		public float Progress => 1 - (CurrentTime / InitialTime);
		
		protected float InitialTime;
		protected float CurrentTime;
		
		public CountdownTimer(float initialTime, bool registerToManager = true) : base(registerToManager)
		{
			InitialTime = initialTime;
			CurrentTime = initialTime;
		}
		
		public override void Tick(float deltaTime)
		{
			if (IsRunning)
			{
				CurrentTime -= deltaTime;
				if (CurrentTime <= 0)
				{
					Complete();
				}
			}
		}
		
		public override void Reset()
		{
			CurrentTime = InitialTime;
			Stop();
		}
		
		protected virtual void Complete()
		{
			CurrentTime = 0;
			IsRunning = false;
			OnTimerComplete.Invoke();
		}
	}

	public class LoopingCountdownTimer : CountdownTimer
	{
		public LoopingCountdownTimer(float initialTime, bool registerToManager = true) : base(initialTime, registerToManager)
		{
		}

		public void SetNewTime(float newTime, bool restartCountdownTimer = false)
		{
			InitialTime = newTime;
			if (restartCountdownTimer)
			{
				CurrentTime = newTime;
			}
		}

		protected override void Complete()
		{
			CurrentTime = InitialTime;
			OnTimerComplete.Invoke();
			Start();
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