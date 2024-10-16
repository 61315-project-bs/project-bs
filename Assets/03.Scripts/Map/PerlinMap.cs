using System.Collections.Generic;
using UnityEngine;

public class PerlinMap : MonoBehaviour
{
    [SerializeField] MapSettingSO _setting;
    [SerializeField] Transform _camera; // 카메라 또는 플레이어 Transform

    private Dictionary<Vector2Int, GameObject> _chunks = new Dictionary<Vector2Int, GameObject>(); // 생성된 청크 관리
    private Vector2Int _previousChunk; // 이전 프레임에서의 카메라가 속한 청크 좌표
    private GameObject _chunkParent; // 청크의 부모 오브젝트

    void Start()
    {
        // 청크들을 관리할 부모 오브젝트 생성
        _chunkParent = new GameObject("ChunksParent");

        // 초기 청크 생성
        _previousChunk = GetCameraChunkPosition();
        UpdateChunks();
    }

    void Update()
    {
        Vector2Int currentChunk = GetCameraChunkPosition();
        if (currentChunk != _previousChunk)
        {
            // 카메라 위치가 변경된 경우에만 청크 업데이트
            UpdateChunks();
            _previousChunk = currentChunk; // 새로운 청크 위치 저장
        }
    }

    // 카메라가 위치한 청크 좌표 계산
    Vector2Int GetCameraChunkPosition()
    {
        return new Vector2Int(
            Mathf.FloorToInt(_camera.position.x / _setting.ChunkSize),
            Mathf.FloorToInt(_camera.position.z / _setting.ChunkSize)
        );
    }

    void UpdateChunks()
    {
        Vector2Int currentChunk = GetCameraChunkPosition();

        // 카메라 주변 청크를 사각형 범위로 생성
        for (int x = -_setting.ViewDistance; x <= _setting.ViewDistance; x++)
        {
            for (int z = -_setting.ViewDistance; z <= _setting.ViewDistance; z++)
            {
                Vector2Int chunkPos = new Vector2Int(currentChunk.x + x, currentChunk.y + z);

                if (!_chunks.ContainsKey(chunkPos)) // 청크가 없으면 새로 생성
                {
                    GenerateChunk(chunkPos);
                }
            }
        }

        // 멀어진 청크 삭제 (옵션)
        List<Vector2Int> chunksToRemove = new List<Vector2Int>();
        foreach (var chunk in _chunks)
        {
            // 카메라에서 일정 거리 이상 떨어진 청크는 제거
            if (Mathf.Abs(chunk.Key.x - currentChunk.x) > _setting.ViewDistance || Mathf.Abs(chunk.Key.y - currentChunk.y) > _setting.ViewDistance)
            {
                Destroy(chunk.Value);
                chunksToRemove.Add(chunk.Key);
            }
        }

        // 청크 삭제 처리
        foreach (var chunkPos in chunksToRemove)
        {
            _chunks.Remove(chunkPos);
        }
    }

    void GenerateChunk(Vector2Int chunkPos)
    {
        GameObject chunk = new GameObject($"Chunk_{chunkPos.x}_{chunkPos.y}");

        // 청크 위치 계산 수정
        chunk.transform.position = new Vector3(chunkPos.x * _setting.ChunkSize, 0, chunkPos.y * _setting.ChunkSize);
        chunk.isStatic = true;
        chunk.transform.SetParent(_chunkParent.transform);

        List<CombineInstance> combineInstances = new List<CombineInstance>();

        // 청크 내 타일 생성 및 메쉬 정보 수집
        for (int x = 0; x < _setting.ChunkSize; x++)
        {
            for (int z = 0; z < _setting.ChunkSize; z++)
            {
                float y = Mathf.PerlinNoise((x + chunkPos.x * _setting.ChunkSize) * _setting.Scale, (z + chunkPos.y * _setting.ChunkSize) * _setting.Scale) * _setting.HeightMultiplier;

                Vector3 position = new Vector3(x, y, z);  // 로컬 좌표로 설정
                PoolAble tile = PoolManager.Instance.GetGameObject("Tile").GetComponent<PoolAble>();
                tile.transform.position = position;

                MeshFilter meshFilter = tile.GetComponent<MeshFilter>();
                if (meshFilter != null)
                {
                    CombineInstance combineInstance = new CombineInstance();
                    combineInstance.mesh = meshFilter.sharedMesh;
                    combineInstance.transform = tile.transform.localToWorldMatrix; // 타일의 로컬 좌표를 월드 좌표로 변환
                    combineInstances.Add(combineInstance);
                }

                // Destroy(tile); // 개별 타일은 병합 후 삭제
                tile.ReleaseObject();
            }
        }

        // 메쉬 병합
        Mesh combinedMesh = new Mesh();
        combinedMesh.CombineMeshes(combineInstances.ToArray(), true, true);

        // 병합된 메쉬를 청크에 적용
        MeshFilter chunkMeshFilter = chunk.AddComponent<MeshFilter>();
        chunkMeshFilter.mesh = combinedMesh;

        MeshRenderer chunkMeshRenderer = chunk.AddComponent<MeshRenderer>();
        chunkMeshRenderer.sharedMaterial = _setting.GroundPrefab.GetComponent<MeshRenderer>().sharedMaterial;

        // 충돌 처리를 위한 MeshCollider 추가
        MeshCollider chunkMeshCollider = chunk.AddComponent<MeshCollider>();
        chunkMeshCollider.sharedMesh = combinedMesh;

        _chunks.Add(chunkPos, chunk); // 생성된 청크를 딕셔너리에 저장
    }
}
