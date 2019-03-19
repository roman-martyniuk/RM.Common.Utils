using System;
using System.Diagnostics;
using System.Threading;

namespace RM.Common.Utils
{
    /// <summary>
    /// Represents an auto reset modes for <see cref="Timer"/> class.
    /// </summary>
    public enum TimerAutoResetMode
    {
        /// <summary>
        /// Elapsed event will be raised at the exact amount of time specified in the <see cref="Timer.Interval"/> property.
        /// </summary>
        Exact,

        /// <summary>
        /// Elapsed event will be raised only after the amount of time specified in the <see cref="Timer.Interval"/> property will pass after the previous <see cref="Timer.Elapsed"/> event has finished execution.
        /// </summary>
        Relative
    }

    /// <summary>
    /// Provides a mechanism for executing a method on a thread pool thread at specified intervals.
    /// NOTE: Timer is thread-safe. Timer is "concurrent execution safe" (no "overlapped" <see cref="Elapsed"/> events will be triggered) in <see cref="TimerAutoResetMode.Relative"/> <see cref="AutoResetMode"/>.
    /// </summary>
    public class Timer : IDisposable
    {
        /// <summary>
        /// The amount of time to delay before <see cref="Elapsed"/> event is invoked.
        /// </summary>
        public TimeSpan StartDelay { get; private set; }

        /// <summary>
        /// Gets the time interval on which to raise events. Min value is represented by <see cref="MinInterval"/> (1 millisecond). Max value is represented by <see cref="MaxInterval"/>.
        /// The default value is 100ms.
        /// </summary>
        public TimeSpan Interval { get; private set; }

        /// <summary>
        /// Gets the AutoReset mode.
        /// </summary>
        public TimerAutoResetMode AutoResetMode { get; }

        /// <summary>
        /// Gets a value indicating whether the <see cref='Timer' /> is able to raise events at a defined interval.
        /// </summary>
        public bool Enabled { get; private set; }

        /// <summary>
        /// Occurs when the interval elapses.
        /// </summary>
        public event EventHandler<CancellationToken> Elapsed;

        /// <summary>
        /// The min value allowed for the <see cref="Interval"/> property.
        /// </summary>
        public static readonly TimeSpan MinInterval = TimeSpan.FromMilliseconds(1);

        /// <summary>
        /// The max value allowed for the <see cref="Interval"/> property.
        /// </summary>
        public static readonly TimeSpan MaxInterval = TimeSpan.FromMilliseconds(4294967294L);

        private System.Threading.Timer _timer;
        private CancellationTokenSource _cts;
        private readonly object _locker = new object();
        private const int DEFAULT_INTERVAL_IN_MILLISECONDS = 100;

        /// <summary>
        /// Initializes a new instance of the <see cref='Timer' /> class and sets <see cref="Interval"/> and <see cref="StartDelay"/> properties to the default values (100ms).
        /// </summary>
        /// <param name="handler">The <see cref="Elapsed"/> event handler.</param>
        /// <param name="autoResetMode">The auto reset mode.</param>
        public Timer(EventHandler<CancellationToken> handler = null, TimerAutoResetMode autoResetMode = TimerAutoResetMode.Exact)
            : this(TimeSpan.FromMilliseconds(DEFAULT_INTERVAL_IN_MILLISECONDS), handler, autoResetMode) { }

        /// <summary>
        /// Initializes a new instance of the <see cref='Timer' /> class and sets <see cref="Interval"/> and <see cref="StartDelay"/> properties to the <paramref name="interval"/> value.
        /// </summary>
        /// <param name="interval">The time interval on which to raise events. Min value is represented by <see cref="MinInterval"/> (1 millisecond). Max value is represented by <see cref="MaxInterval"/>.</param>
        /// <param name="handler">The <see cref="Elapsed"/> event handler.</param>
        /// <param name="autoResetMode">The auto reset mode.</param>
        public Timer(TimeSpan interval, EventHandler<CancellationToken> handler = null, TimerAutoResetMode autoResetMode = TimerAutoResetMode.Exact)
            : this(interval, interval, handler, autoResetMode) { }

        /// <summary>
        /// Initializes a new instance of the <see cref='Timer' /> class.
        /// </summary>
        /// <param name="startDelay">The amount of time to delay before <see cref="Elapsed"/> event is invoked. Specify <see cref="TimeSpan.Zero"/> to start the timer immediately.</param>
        /// <param name="interval">The time interval on which to raise events. Min value is represented by <see cref="MinInterval"/> (1 millisecond). Max value is represented by <see cref="MaxInterval"/>.</param>
        /// <param name="handler">The <see cref="Elapsed"/> event handler.</param>
        /// <param name="autoResetMode">The auto reset mode.</param>
        public Timer(TimeSpan startDelay, TimeSpan interval, EventHandler<CancellationToken> handler = null, TimerAutoResetMode autoResetMode = TimerAutoResetMode.Exact)
        {
            Ensure.IsGreaterThanOrEqualToZero(startDelay, nameof(startDelay));
            Ensure.IsBetween(interval, MinInterval, MaxInterval, nameof(interval));

            StartDelay = startDelay;
            Interval = interval;
            AutoResetMode = autoResetMode;

            _timer = new System.Threading.Timer(TimerCallback, null, Timeout.Infinite, Timeout.Infinite);

            if (handler != null) Elapsed += handler;
        }

        /// <summary>
        /// Changes an <see cref="Interval"/> and <see cref="StartDelay"/> and restarts timer (if already started).
        /// </summary>
        /// <param name="startDelay">The start delay to change.</param>
        /// <param name="interval">The interval to change.</param>
        public void Change(TimeSpan startDelay, TimeSpan interval)
        {
            EnsureNotDisposed();
            Ensure.IsGreaterThanOrEqualToZero(startDelay, nameof(startDelay));
            Ensure.IsBetween(interval, MinInterval, MaxInterval, nameof(interval));

            lock (_locker)
            {
                StartDelay = startDelay;
                Interval = interval;

                if (!Enabled) return;

                _timer.Change(StartDelay, AutoResetMode == TimerAutoResetMode.Exact ? Interval : Timeout.InfiniteTimeSpan);
            }
        }

        /// <summary>
        /// Starts the timing. Does nothing if Timer has been already started.
        /// </summary>
        public void Start()
        {
            EnsureNotDisposed();

            lock (_locker)
            {
                if (Enabled) return;

                Enabled = true;
                if (_cts == null || _cts.IsCancellationRequested) _cts = new CancellationTokenSource();

                _timer.Change(StartDelay, AutoResetMode == TimerAutoResetMode.Exact ? Interval : Timeout.InfiniteTimeSpan);
            }
        }

        /// <summary>
        /// Stops the timing. Does nothing if Timer has been already stopped.
        /// </summary>
        public void Stop()
        {
            EnsureNotDisposed();

            lock (_locker)
            {
                if (!Enabled) return;

                Enabled = false;
                _timer.Change(Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);

                _cts.Cancel();
            }
        }

        private void TimerCallback(object state)
        {
            CancellationTokenSource cts;
            lock (_locker)
            {
                // Timer will not cancel the work item queued before the timer is stopped.
                // We don't want to handle the callback after a timer is stopped.
                if (!Enabled) return;

                cts = _cts;
            }

            try
            {
                // in try block because "cts.Token" can throw ObjectDisposedException
                Elapsed.Raise(this, cts.Token);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            lock (_locker)
            {
                if (cts.IsCancellationRequested) cts.Dispose();
                if (!Enabled) return;

                if (AutoResetMode == TimerAutoResetMode.Relative) _timer.Change(Interval, Timeout.InfiniteTimeSpan);
            }
        }

        #region Disposing
        private bool _disposed;

        private void EnsureNotDisposed()
        {
            if (_disposed) throw new ObjectDisposedException(GetType().Name);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose(bool disposing) executes in two distinct scenarios.
        /// If disposing equals true, the method has been called directly or indirectly by a user's code. Managed and unmanaged resources can be disposed.
        /// If disposing equals false, the method has been called by the runtime from inside the finalizer and you should not reference other objects. Only unmanaged resources can be disposed. 
        /// </summary>
        /// <param name="disposing">Indicates whether method has been called directly or indirectly by a user's code or from inside the finalizer.</param>
        /// <example>
        /// Derived class implementation example:
        /// <code>
        /// private bool _disposed = false;
        /// protected override void Dispose(bool disposing)
        /// {
        ///    if (_disposed) return;
        ///
        ///    if (disposing)
        ///    {
        ///       // Dispose managed resources.
        ///    }
        ///
        ///    // Dispose unmanaged resources.
        ///    _disposed = true;
        ///
        ///    base.Dispose(disposing);
        /// }
        /// </code>
        /// </example>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                // Dispose managed resources.
                Stop();
                Elapsed = null;
                _timer.Dispose();
                _timer = null;

                _cts?.Dispose();
                _cts = null;
            }

            // Dispose unmanaged resources.
            _disposed = true;
        }

        /// <summary>
        /// The <see cref="Timer"/> class finalizer.
        /// </summary>
        ~Timer()
        {
            Dispose(false);
        }
        #endregion
    }
}