using System.Collections.Generic;
using System;

public class Presenter<T> where T : UIBase
{
    protected T _view;
    protected Queue<IDisposable> _disposables;
    public Presenter(T view)
    {
        _view = view;
        _disposables = new Queue<IDisposable>();
    }
    public void DisopseStreams()
    {
        while (_disposables.Count > 0)
            _disposables.Dequeue().Dispose();
    }
}