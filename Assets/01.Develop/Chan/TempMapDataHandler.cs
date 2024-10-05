using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

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
    public IntReactiveProperty ReactSelectMapIdx { get; private set; } = new IntReactiveProperty(-1);
    public Map[] Maps { get { return _maps; } }
    /// <summary>
    /// -1은 맵을 선택하지 않은 상태.
    /// </summary>
    public int SelectMapIdx
    {
        get { return ReactSelectMapIdx.Value; }
        set
        {
            if (value > _maps.Length - 1)
                Debug.LogError($"[TempMapDataHandler]::::CurrMapIdx is invalid. -> {value}");
            else
                ReactSelectMapIdx.Value = value;
        }
    }
    #endregion~Map
    #region SetLevel
    public readonly string[] STR_LEVEL = { "easy", "medium", "hard" };
    private int _maxLevel;
    //private int _currLevel = 0;
    public IntReactiveProperty ReactCurrLevel { get; private set; } = new IntReactiveProperty(0);
    public int MaxLevel { get { return _maxLevel; } }
    /// <summary>
    /// 데이터 저장 시 해당 프로퍼티로 저장하면 될 것 같음.
    /// </summary>
    public int CurrLevel { get { return ReactCurrLevel.Value; } set{ ReactCurrLevel.Value = value; } }
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
