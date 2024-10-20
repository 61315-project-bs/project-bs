using System;
using UnityEngine;

/// <summary>
/// 유저 저장 데이터 로드
/// </summary>
public class SaveData
{

  public void Init()
  {
    LoadData();
  }

  private void LoadData()
  {
    try
    {
    }
    catch (Exception ex)
    {
      string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
      Debug.LogError($"{ex.Message}, {ex.StackTrace}, {sceneName}");
    }
  }
}