using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 맵 SubItem UI에서 마우스 오버 시 출력되는 UI
/// </summary>

public class UISubItem_MapDetail : UISubItem
{
    [SerializeField] private Text _Name;
    [SerializeField] private Text _Desc;
    [SerializeField] private Image _Icon;

    public void SetData(Map map)
    {
        _Name.text = map.Name;
        _Desc.text = map.Desc;
        _Icon.sprite = map.Icon;
    }
}
