using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 데이터 로드해서 딕셔너리로 만들어주는 함수 갖고있는 로더 클래스
/// private 이지만 파일 이름과 변수명 동일하게 쓰려고 _안 붙힌다.
/// </summary>
public class DefaultDataLoader
{
  private static string _csvPath = "CSV";

  private static string MapData = "MapData";

  public static Dictionary<string, MapData> ParseMapData()
  {
    List<Dictionary<string, object>> _tempData = CSVReader.Read($"{_csvPath}/{MapData}");

    Dictionary<string, MapData> dic = new Dictionary<string, MapData>();

    for (int i = 0; i < _tempData.Count; i++)
    {
      MapData data = new MapData();

      data.Id = _tempData[i]["Id"].ToString();
      data.Name = _tempData[i]["Name"].ToString();
      data.Name = _tempData[i]["Description"].ToString();

      if (dic.ContainsKey(data.Id))
      {
        dic[data.Id] = data;
      }
      else
      {
        dic.Add(data.Id, data);
      }
    }

    if (dic == null)
    {
      Debug.LogError($"MapData");
    }

    return dic;
  }
}