using System.Collections.Generic;
using System;

public static class UniRXExtendedMethod
{
    public static void EnqueueDispose(this IDisposable disposable, Queue<IDisposable> queue)
    {
        queue.Enqueue(disposable);
    }
}