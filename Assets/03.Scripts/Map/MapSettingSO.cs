using UnityEngine;

[CreateAssetMenu(fileName = "NewMapSetting", menuName = "Map Setting", order = 51)]
public class MapSettingSO : ScriptableObject
{
  public GameObject GroundPrefab;
  public float Scale = 0.07f;  // 노이즈의 세부 조절을 위한 스케일
  public float HeightMultiplier = 4f;
}
