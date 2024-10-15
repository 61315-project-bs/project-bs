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

    /// <summary>
    /// FsmRunner 종료시 반드시 Dispose 해줘야 함.
    /// 활용하는 곳이 MonoBehaviour라면 OnDisable이나 OnDestroy에서 실행될 수 있게 해야함.
    /// </summary>
    public void Dispose() => _currentState.Dispose();
}
