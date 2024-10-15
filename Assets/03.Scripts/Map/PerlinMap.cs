using UnityEngine;

public class PerlinMap : MonoBehaviour
{
    [SerializeField] MapSettingSO _setting;

    void Awake()
    {
        GenerateTerrain(50, 50);
    }

    void GenerateTerrain(int width, int depth)
    {
        int seed = Random.Range(-100000, 100000);

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                float y = Mathf.PerlinNoise((x + seed) * _setting.Scale, (z + seed) * _setting.Scale) * _setting.HeightMultiplier;

                Vector3 position = new Vector3(x, y, z);
                Instantiate(_setting.GroundPrefab, position, Quaternion.identity);
            }
        }
    }
}
