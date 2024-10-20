using System.IO;
using UnityEngine;

public static class JsonUtilityHelper
{
  private static string GetFilePath<T>()
  {
    return Application.persistentDataPath + $"/{typeof(T).ToString()}.json";
  }

  public static void SaveJsonData<T>(T dataClass)
  {
    string path = GetFilePath<T>();
    File.WriteAllText(path, JsonUtility.ToJson(dataClass));
  }

  public static T LoadJsonData<T>() where T : new()
  {
    string path = GetFilePath<T>();

    if (!File.Exists(path))
    {
      return new T();
    }

    string jsonData = File.ReadAllText(path);
    return JsonUtility.FromJson<T>(jsonData);
  }
}
