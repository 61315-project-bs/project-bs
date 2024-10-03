using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//[Serializable]
//public class Map
//{
//    [SerializeField] private string _name;
//    [SerializeField] private string _desc;
//    [SerializeField] private Sprite _mapIcon;
//    [SerializeField] private bool _isLock;
//    public Sprite Icon { get { return _mapIcon; } }
//    public string Name { get { return _name; } }
//    public string Desc { get { return _desc; } }
//    public bool IsLock { get { return _isLock; } }
//}



public class TempMapDataHandler : Singleton<TempMapDataHandler>
{
    private readonly string[] STR_LEVEL = { "easy", "medium", "hard" };

    private int _maxLevel; 
    //[SerializeField] private Map[] _maps;

    //private int _selectMapIdx;
    private int _currLevel = 0;
    
    public string CurrLevelStr { get { return STR_LEVEL[_currLevel]; } }
    public int MaxLevel { get { return _maxLevel; } }
    public int CurrLevel { get { return _currLevel; } 
        set
        {
            // 0~2 범위에서만 설정할 수 있게 적용
            if (value > _maxLevel)
                _currLevel = _maxLevel;
            else if (value < 0)
                _currLevel = 0;
            else
                _currLevel = value;
        }
    }
    //public int SelectMapIdx { get { return _selectMapIdx; } 
    //    set
    //    {
    //        if (value > _maps.Length - 1)
    //            Debug.LogError($"[TempMapDataHandler]::::CurrMapIdx is invalid. -> {value}");
    //        else
    //            _selectMapIdx = value;
    //    }
    //}

    private void Start()
    {
        Init();
    }
    private void Init()
    {
        _maxLevel = STR_LEVEL.Length - 1;
    }
    
}
