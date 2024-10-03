using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Map
{
    [SerializeField] private string _name;
    [SerializeField] private string _desc;
    [SerializeField] private Sprite _mapIcon;
    [SerializeField] private bool _isLock;
    public Sprite Icon 
    { 
        get 
        {
            if (_isLock)
                return ResourceManager.Instance.Load<Sprite>("Art/Sprite/Img_Lock");
            else
                return _mapIcon; 
        } 
    }
    public string Name 
    { 
        get 
        {
            if (_isLock)
                return "???";
            else
                return _name; 
        } 
    }
    public string Desc 
    { 
        get 
        {
            if (_isLock)
                return "아직 확인되지 않은 지역입니다.";
            else
                return _desc; 
        } 
    }
    public bool IsLock { get { return _isLock; } }
}



public class TempMapDataHandler : Singleton<TempMapDataHandler>
{
    #region Map
    [SerializeField] private Map[] _maps;
    private int _selectMapIdx = -1;
    public Map[] Maps { get { return _maps; } }
    /// <summary>
    /// -1은 맵을 선택하지 않은 상태.
    /// </summary>
    public int SelectMapIdx
    {
        get { return _selectMapIdx; }
        set
        {
            if (value > _maps.Length - 1)
                Debug.LogError($"[TempMapDataHandler]::::CurrMapIdx is invalid. -> {value}");
            else
                _selectMapIdx = value;
        }
    }
    #endregion~Map
    #region SetLevel
    private readonly string[] STR_LEVEL = { "easy", "medium", "hard" };
    private int _maxLevel; 
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
    #endregion~SetLevel

    private void Start()
    {
        Init();
    }
    private void Init()
    {
        _maxLevel = STR_LEVEL.Length - 1;
    }
    
}
