using UnityEngine;

[CreateAssetMenu(fileName = "NewMapSetting", menuName = "Map Setting", order = 51)]
public class MapSettingSO : ScriptableObject
{
  public GameObject GroundPrefab;

  [Header("Purlin")]
  public float Scale = 0.07f;  // 노이즈의 세부 조절을 위한 스케일
  public float HeightMultiplier = 4f;

  [Header("Chunk")]
  public int ChunkSize = 8; // 청크 하나의 크기
  public int ViewDistance = 3; // 카메라 주변 청크 생성 거리
}
