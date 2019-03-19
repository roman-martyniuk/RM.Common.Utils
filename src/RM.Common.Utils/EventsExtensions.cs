using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace RM.Common.Utils
{
    /// <summary>
    /// Represents extension methods for events.
    /// </summary>
    public static class EventsExtensions
    {
        #region EventHandler events
        /// <summary>
        /// Raises an event synchronously.
        /// NOTE: All handlers invoked in try-catch block, so exeption in one of them will not prevent others from executing.
        /// </summary>
        public static void Raise(this EventHandler eventHandler, object sender)
        {
            if (eventHandler == null) return;

            foreach (var handler in eventHandler.GetInvocationList().OfType<EventHandler>()) handler.TryInvoke(sender);
        }

        /// <summary>
        /// Raises an event asynchronously.
        /// NOTE: All handlers invoked in try-catch block, so exeption in one of them will not prevent others from executing.
        /// </summary>
        public static void RaiseAsync(this EventHandler eventHandler, object sender)
        {
            if (eventHandler == null) return;

            ThreadPool.QueueUserWorkItem(_ => eventHandler.Raise(sender));
        }

        private static void TryInvoke(this EventHandler eventHandler, object sender)
        {
            try
            {
                eventHandler(sender, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
        #endregion

        #region EventHandler<T> events
        /// <summary>
        /// Raises an event synchronously.
        /// NOTE: All handlers invoked in try-catch block, so exeption in one of them will not prevent others from executing.
        /// </summary>
        public static void Raise<T>(this EventHandler<T> eventHandler, object sender, T e)
        {
            if (eventHandler == null) return;

            foreach (var handler in eventHandler.GetInvocationList().OfType<EventHandler<T>>()) handler.TryInvoke(sender, e);
        }

        /// <summary>
        /// Raises an event asynchronously.
        /// NOTE: All handlers invoked in try-catch block, so exeption in one of them will not prevent others from executing.
        /// </summary>
        public static void RaiseAsync<T>(this EventHandler<T> eventHandler, object sender, T e)
        {
            if (eventHandler == null) return;

            ThreadPool.QueueUserWorkItem(_ => eventHandler.Raise(sender, e));
        }

        private static void TryInvoke<T>(this EventHandler<T> eventHandler, object sender, T args)
        {
            try
            {
                eventHandler(sender, args);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
        #endregion
    }
}