using UnityEngine;

public class PerlinNoiseVisualizer : NoiseVisualizer
{
    public int offsetRate;
    [Range(0, 1)] public float threshold;
    [SerializeField] private float _noiseScale = 0.2f;

    private void Start()
    {
        GeneratePerlinNoise();
    }

    [ContextMenu("Generate Perlin Noise")]
    void GeneratePerlinNoise()
    {
        int offsetX = UnityEngine.Random.Range(0, offsetRate);
        int offsetY = UnityEngine.Random.Range(0, offsetRate);
        texture = NoiseGenerator.GeneratePerlinNoise(width, height, threshold, _noiseScale, offsetX,
            offsetY);

        // Assign to material
        UpdateTextureOnMaterial();
    }
}