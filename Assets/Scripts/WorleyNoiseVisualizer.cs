using UnityEngine;

public class WorleyNoiseVisualizer : NoiseVisualizer
{
    public int Points = 20;
    [Range(0, 5)] public float colorBooster = 1;
    [Range(0, 5)] public float pointDistanceMultiplier = 1;

    void Start()
    {
        GenerateWorleyNoise();
    }

    [ContextMenu("Generate Worley Noise")]
    void GenerateWorleyNoise()
    {
        texture = NoiseGenerator.GenerateWorleyNoise(width, height, Points, colorBooster,
            pointDistanceMultiplier);

        // Assign to material
        UpdateTextureOnMaterial();
    }
}