using UnityEngine;

public class GenerateGrid : MonoBehaviour
{
    [SerializeField] private GameObject blockGameObject;
    private const int worldSizeX = 20;
    private const int worldSizeZ = 20;
    private const int noiseHeight = 3;
    private const float gridOffSet = 1.1f;

    private void Start()
    {
        for (int x = 0; x < worldSizeX; x++)
            for (int z = 0; z < worldSizeZ; z++)
            {
                Vector3 position = new Vector3(
                    x * gridOffSet,
                    GenerateNoise(x, z, 8f) * noiseHeight,
                    z * gridOffSet);
                GameObject block = Instantiate(blockGameObject, position, Quaternion.identity) as GameObject;
                block.transform.SetParent(transform);
            }
    }

    private float GenerateNoise(int x, int z, float detailScale)
    {
        float xNoise = (x + this.transform.position.x) / detailScale;
        float zNoise = (z + this.transform.position.y) / detailScale;

        return Mathf.PerlinNoise(xNoise, zNoise);
    }
}
