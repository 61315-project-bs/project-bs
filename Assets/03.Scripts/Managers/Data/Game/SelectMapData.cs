
using System;
using UniRx;
using UnityEngine;

[Serializable]
public class SelectMapData
{
  public IntReactiveProperty ReactSelectMapIdx { get; private set; } = new IntReactiveProperty(-1);
  public bool[] UnlockedMaps;
  private TempMap[] _maps;
  public TempMap[] Maps => _maps;
  public void Init()
  {

    if (UnlockedMaps == null || UnlockedMaps.Length != _maps.Length)
    {
      UnlockedMaps = new bool[_maps.Length];
      UnlockedMaps[0] = true;
    }
  }

  public void InitializeReactiveProperties(Action onChange)
  {
    ReactSelectMapIdx.Subscribe(_ => onChange());
  }

  public void SetSelectMapIdx(int idx)
  {
    if (idx >= _maps.Length || idx < 0)
    {
      Debug.LogError($"[MapData] Invalid map index -> {idx}");
      return;
    }

    if (!UnlockedMaps[idx])
    {
      Debug.LogWarning($"[MapData] Map {idx} is locked.");
      return;
    }

    ReactSelectMapIdx.Value = idx;
  }

  public void UnlockMap(int idx)
  {
    if (idx >= _maps.Length || idx < 0)
    {
      Debug.LogError($"[MapData] Invalid map index for unlocking -> {idx}");
      return;
    }

    UnlockedMaps[idx] = true;
  }

  public TempMap GetSelectedMap()
  {
    if (ReactSelectMapIdx.Value < 0 || ReactSelectMapIdx.Value >= _maps.Length)
      return null;

    return _maps[ReactSelectMapIdx.Value];
  }
}
