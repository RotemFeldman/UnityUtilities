using System;

namespace UnityUtilities
{
	/// <summary>
	/// Represents an abstract base class for managing timer functionality such as
	/// starting, stopping, pausing, resuming, and tracking the status of a timer.
	/// </summary>
	public abstract class Timer
	{
		/// <summary>
		/// An event triggered when the timer is started.
		/// Assignable with a user-defined callback to execute custom behavior upon starting the timer.
		/// </summary>
		public Action OnTimerStart = delegate { };

		/// <summary>
		/// Action delegate that is invoked when the timer stops.
		/// </summary>
		/// <remarks>
		/// This delegate can be used to execute custom functionality or handle events upon stopping the timer.
		/// It is triggered whenever the <see cref="Timer.Stop()"/> method is called and the timer was previously running.
		/// The default value is an empty delegate to prevent null reference exceptions.
		/// </remarks>
		public Action OnTimerStop = delegate { };

		/// <summary>
		/// Indicates whether the timer is currently running.
		/// </summary>
		/// <remarks>
		/// This property reflects the state of the timer. It is set to true when the
		/// timer starts and false when it stops or is paused. Changes to this property
		/// are managed internally by the timer's Start, Stop, Pause, and Resume methods.
		/// </remarks>
		public bool IsRunning { get; protected set; }

		/// Represents an abstract base class for handling timer functionality.
		/// It provides basic operations to start, stop, pause, resume, and reset a timer.
		/// It also supports ticking functionality, which must be implemented by derived classes.
		/// A timer can optionally be registered to a TimerManager for centralized management.
		protected Timer(bool registerToManager = true)
		{
			if (registerToManager)
			{
				TimerManager.Instance.RegisterTimer(this);
			}
		}

		/// <summary>
		/// Represents an abstract timer that provides basic timer-related functionalities such as starting, stopping, resuming, pausing, and resetting the timer.
		/// </summary>
		~Timer()
		{
			TimerManager.Instance.UnregisterTimer(this);
		}

		/// <summary>
		/// Updates the timer's state and handles logic tied to the passage of time.
		/// </summary>
		/// <param name="deltaTime">The amount of time, in seconds, since the last update.</param>
		public abstract void Tick(float deltaTime);

		/// <summary>
		/// Starts the timer.
		/// </summary>
		/// <remarks>
		/// This method transitions the timer to a running state.
		/// It invokes the <see cref="OnTimerStart"/> action if the timer is not already running.
		/// </remarks>
		public virtual void Start()
		{
			if (!IsRunning)
			{
				IsRunning = true;
				OnTimerStart.Invoke();
			}
		}

		/// <summary>
		/// Stops the currently running timer.
		/// If the timer is active, this method sets the <c>IsRunning</c> property to false
		/// and invokes the <c>OnTimerStop</c> action to notify that the timer has been stopped.
		/// </summary>
		public virtual void Stop()
		{
			if (IsRunning)
			{
				IsRunning = false;
				OnTimerStop.Invoke();
			}
		}

		/// <summary>
		/// Resumes the timer by setting the IsRunning property to true.
		/// </summary>
		public void Resume() => IsRunning = true;

		/// Pauses the execution of the timer.
		/// This stops the timer from advancing while maintaining its current state
		/// so it may be resumed later without resetting or losing progress.
		/// Sets the IsRunning property to false.
		public void Pause() => IsRunning = false;

		/// <summary>
		/// Resets the state of the timer to its initial condition.
		/// Implementations of this method should restore the timer's values
		/// to their original state, effectively restarting the timer.
		/// </summary>
		public abstract void Reset();
	}

	/// <summary>
	/// A CountdownTimer is a timer that counts down from a specified time to zero.
	/// </summary>
	/// <remarks>
	/// This timer is designed to count down from an initial time value and invoke an
	/// event when it reaches zero. It inherits from the Timer class and includes additional
	/// features such as progress tracking and time remaining calculation.
	/// </remarks>
	/// <example>
	/// Initializes the timer with a given time and uses it to manage time-sensitive actions.
	/// </example>
	/// <events>
	/// <typeparamref name="OnTimerComplete"/> is invoked when the timer completes its countdown.
	/// Derived from Timer: <typeparamref name="OnTimerStart"/> and <typeparamref name="OnTimerStop"/> behaviors
	/// fire to **along registered user code successful validation further dev-spec complete callback
	/// =`` instance completes per Instruction Pragma stric...text.empty..
	public class CountdownTimer : Timer
	{
		/// <summary>
		/// An event triggered when the countdown timer reaches completion.
		/// </summary>
		/// <remarks>
		/// This action is invoked when the countdown timer has completely elapsed.
		/// It can be used to define behavior or execute logic at the point when the timer finishes.
		/// </remarks>
		public Action OnTimerComplete = delegate { };

		/// <summary>
		/// Gets the remaining time for the countdown timer to reach zero.
		/// </summary>
		/// <remarks>
		/// This property provides the current remaining time of the countdown timer.
		/// The value decreases as the timer progresses, and it reaches zero when the timer completes.
		/// </remarks>
		/// <value>
		/// A float representing the current remaining time of the countdown timer.
		/// </value>
		public float TimeRemaining => CurrentTime;

		/// Represents the progress of the countdown timer as a value between 0 and 1.
		/// The value is calculated based on the ratio of time elapsed to the total initial time.
		/// A value of 0 indicates the timer has just started, whereas a value of 1 indicates the timer has completed.
		public float Progress => 1 - (CurrentTime / InitialTime);

		/// <summary>
		/// Represents the initial duration of the countdown timer in seconds.
		/// This value is set upon initialization and remains constant throughout the lifecycle of the timer.
		/// It is used as a baseline for calculations, such as determining the timer's progress or resetting the timer.
		/// </summary>
		protected float InitialTime;

		/// <summary>
		/// Represents the current elapsed time or remaining time for a timer.
		/// This value is used to track the progress of the CountdownTimer object,
		/// decreasing over time until reaching zero.
		/// </summary>
		protected float CurrentTime;

		/// <summary>
		/// Represents a countdown timer that counts down from a specified time to zero.
		/// Extends the abstract Timer class to provide countdown functionality.
		/// </summary>
		public CountdownTimer(float initialTime, bool registerToManager = true) : base(registerToManager)
		{
			InitialTime = initialTime;
			CurrentTime = initialTime;
		}

		/// <summary>
		/// Updates the timer's state based on the elapsed time since the last update.
		/// </summary>
		/// <param name="deltaTime">The time in seconds that has passed since the last tick.</param>
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

		/// <summary>
		/// Resets the timer to its initial state.
		/// </summary>
		/// <remarks>
		/// For a CountdownTimer, this will reset the remaining time to the initial value.
		/// For a Stopwatch, this will reset the elapsed time to zero.
		/// The timer's running state is not affected by this method; it will not start or stop the timer.
		/// </remarks>
		public override void Reset()
		{
			CurrentTime = InitialTime;
			Stop();
		}

		/// Represents an abstract base class for a timer, providing basic functionalities such as starting, stopping,
		/// pausing, resuming, and resetting. It also defines an interface for ticking and behavior specific to derived timers.
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

	/// <summary>
	/// A class representing a stopwatch that measures elapsed time.
	/// </summary>
	/// <remarks>
	/// The stopwatch provides functionality to start, stop, resume, pause, and reset the elapsed time.
	/// It can incrementally track elapsed time while running. The stopwatch integrates with a manager for centralized control.
	/// </remarks>
	public class Stopwatch : Timer
	{
		/// <summary>
		/// Represents a stopwatch timer that can be used to track elapsed time.
		/// Inherits from the Timer class and manages time by continuously increasing
		/// its own value as it ticks.
		/// </summary>
		public Stopwatch(bool registerToManager = true) : base(registerToManager) {}

		/// <summary>
		/// Gets the current elapsed time recorded by the Stopwatch.
		/// </summary>
		/// <remarks>
		/// The Time property represents the accumulated time the Stopwatch has been running.
		/// It provides the total elapsed time as a floating-point value in seconds.
		/// </remarks>
		public float Time => _value;

		/// <summary>
		/// Tracks the elapsed time for the Stopwatch instance, accumulating time while the timer is running.
		/// This variable is incremented during each tick when the timer is active.
		/// </summary>
		/// <remarks>
		/// The value starts at 0 and increases based on the delta time provided during the Tick method.
		/// Resets to 0 whenever the Reset method is called.
		/// </remarks>
		private float _value = 0f;

		/// Called each frame to update the timer's state.
		/// <param name="deltaTime">The time increment to update the timer by, typically the time elapsed since the last frame.</param>
		public override void Tick(float deltaTime)
		{
			if (IsRunning)
			{
				_value += deltaTime;
			}
		}

		/// <summary>
		/// Resets the timer to its initial state.
		/// For CountdownTimer, this sets the remaining time back to the initial time.
		/// For Stopwatch, this resets the elapsed time to zero.
		/// </summary>
		public override void Reset() => _value = 0f;
	}

	
}