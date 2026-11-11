using System;
using System.Threading;
using System.Threading.Tasks;

namespace CP.VPOS.Helpers
{
    /// <summary>
    /// Senkron API'lerin asenkron çekirdeği güvenli bir şekilde çalıştırabilmesi için kullanılır.
    /// İş, SynchronizationContext taşımayan varsayılan TaskScheduler üzerinde başlatıldığı için
    /// klasik ASP.NET, WinForms ve WPF gibi ortamlarda deadlock oluşmaz.
    /// </summary>
    internal static class AsyncHelper
    {
        private static readonly TaskFactory _taskFactory = new TaskFactory(
            CancellationToken.None,
            TaskCreationOptions.None,
            TaskContinuationOptions.None,
            TaskScheduler.Default);

        internal static TResult RunSync<TResult>(Func<Task<TResult>> func)
        {
            return _taskFactory
                .StartNew(func)
                .Unwrap()
                .GetAwaiter()
                .GetResult();
        }
    }
}
