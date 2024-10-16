using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class FsmRunner
{
    private ReactiveProperty<IState> _currentState;
    public IState CurrentState { get => _currentState.Value; set => _currentState.Value = value; }
    private Queue<IDisposable> _disposables;
    public FsmRunner()
    {
        _disposables = new Queue<IDisposable>();
        _currentState = new ReactiveProperty<IState>();
        _currentState
            .AsObservable()
            .Pairwise()
            .Subscribe(state =>
            {
                state.Previous?.Exit();
                state.Current?.Enter();
            })
            .EnqueueDispose(_disposables);
        Observable
            .EveryUpdate()
            .Subscribe(_ =>
            {
                CurrentState?.Update();
            })
            .EnqueueDispose(_disposables);
        Observable
            .EveryFixedUpdate()
            .Subscribe(_ =>
            {
                CurrentState?.PhysicsUpdate();
            })
            .EnqueueDispose(_disposables);
    }

    /// <summary>
    /// FsmRunner 종료시 반드시 Dispose 해줘야 함.
    /// 활용하는 곳이 MonoBehaviour라면 OnDisable이나 OnDestroy에서 실행될 수 있게 해야함.
    /// </summary>
    public void Dispose()
    { 
        while (_disposables.Count > 0) 
            _disposables.Dequeue().Dispose(); 
    }

}
