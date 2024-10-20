using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 딕셔너리로 만들어진 CSV 데이터들을 읽기 전용으로 관리하는 클래스
/// </summary>
public class DefaultData
{
  public Dictionary<string, MapData> MapData { get; private set; }

  public void Init()
  {
    LoadData();
  }

  private void LoadData()
  {
    try
    {
      // ----- CSV 데이터 로드 시작 -----
      MapData = DefaultDataLoader.ParseMapData();
    }
    catch (System.Exception ex)
    {
      string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
      Debug.LogError($"{ex.Message}, {ex.StackTrace}, {sceneName}");
    }
  }
}