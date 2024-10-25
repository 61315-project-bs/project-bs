using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public abstract class BaseSingleStatHandler<T> : MonoBehaviour where T : IStat_Single<T>
{
    // 강화될 땐 CurreentStat에 직접 접근 해서 Add 하면 된다.
    [HideInInspector] public T CurrentStat { get; protected set; }
    [SerializeField] protected T _baseStat;
    protected List<T> _statModifiers = new List<T>();
    protected event Action OnStatChangedEvent;
    public string UniqueId;

    public void Init()
    {
        if (_baseStat == null) return;
        UpdateStat();
    }

    public virtual void UpdateStat()
    {
        CurrentStat = _baseStat.DeepCopy();
        
        _statModifiers.OrderBy(stat => stat.Type);
        
        foreach (T stat in _statModifiers)
        {
            switch (stat.Type)
            {
                case StatType.Add:
                    CurrentStat.Add(stat.Index, stat.Value);
                    break;
                case StatType.Multiple:
                    CurrentStat.Multiply(stat.Index, stat.Value);
                    break;
                case StatType.Override:
                    CurrentStat = stat.DeepCopy();
                    break;
        
            }
        }
        OnStatChanged();
    }
    public virtual void AddStat(T stat)
    {
        _statModifiers.Add(stat);
    }
    public virtual void RemoveStat(T stat)
    {
        _statModifiers.Remove(stat);
        stat.Remove(stat.Index);
    }
    public virtual void OnStatChanged()
    {
        OnStatChangedEvent?.Invoke();
    }
    public virtual void SetBaseStat(T stat)
    {
        _baseStat = stat.DeepCopy();
    }
    public void AddStat(T stat, StatType type)
    {
        stat.Type = type;
        AddStat(stat);
    }
}
