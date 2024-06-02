using System.Timers;
using Microsoft.Maui.Dispatching;

namespace BlazorMauiApp.Services
{
    public class TimerService : IDisposable
    {
        private readonly System.Timers.Timer timer;
        private readonly IDispatcher dispatcher;
        public string CurrentTime { get; private set; }

        public event Action OnTick;

        public TimerService(IDispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
            CurrentTime = DateTime.Now.ToString("HH:mm:ss");
            timer = new System.Timers.Timer(1000); // 5 seconds
            timer.Elapsed += OnTimerElapsed;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            // Update the current time on the UI thread
            dispatcher.Dispatch(() =>
            {
                CurrentTime = DateTime.Now.ToString("HH:mm:ss");
                OnTick?.Invoke();
            });
        }

        public void Dispose()
        {
            timer?.Dispose();
        }
    }

}
