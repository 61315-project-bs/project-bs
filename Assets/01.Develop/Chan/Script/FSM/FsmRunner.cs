using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class FsmRunner
{
    private ReactiveProperty<IState> _currentState;
    public IState CurrentState { get => _currentState.Value; set => _currentState.Value = value; }

    public FsmRunner()
    {
        _currentState = new ReactiveProperty<IState>();
        _currentState
            .AsObservable()
            .Pairwise()
            .Subscribe(state =>
            {
                state.Previous?.Exit();
                state.Current?.Enter();
            });
    }
    public void Dispose() => _currentState.Dispose();
}
