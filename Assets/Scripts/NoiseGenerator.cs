using System.Collections.Generic;
using UnityEngine;

public static class NoiseGenerator
{
    // Method for generating Worley noise
    public static Texture2D GenerateWorleyNoise(int width, int height, int numFeaturePoints, float colorBooster,
        float pointDistanceMultiplier)
    {
        Texture2D texture = new Texture2D(width, height);
        texture.filterMode = FilterMode.Bilinear;

        // Dynamically calculate grid size based on numFeaturePoints
        int gridSize = Mathf.CeilToInt(Mathf.Sqrt((width * height) / (float)numFeaturePoints));
        List<Vector2Int> featurePoints = new List<Vector2Int>();

        // Generate feature points within each grid cell
        for (int gy = 0; gy < height; gy += gridSize)
        {
            for (int gx = 0; gx < width; gx += gridSize)
            {
                if (featurePoints.Count >= numFeaturePoints) break; // Stop if we've placed enough points

                int randX = gx + Random.Range(0, gridSize);
                int randY = gy + Random.Range(0, gridSize);
                randX = Mathf.Clamp(randX, 0, width - 1);
                randY = Mathf.Clamp(randY, 0, height - 1);

                featurePoints.Add(new Vector2Int(randX, randY));
            }
        }

        // Generate noise
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float minDist = float.MaxValue;

                // Find the nearest feature point
                foreach (Vector2 point in featurePoints)
                {
                    float dist = Vector2.Distance(new Vector2(x, y), point);
                    if (dist < minDist)
                    {
                        minDist = dist;
                    }
                }

                float maxPossibleDist = Mathf.Sqrt(width * width + height * height);
                float value = minDist / (maxPossibleDist * pointDistanceMultiplier);
                value = Mathf.Clamp01(value);
                value *= colorBooster;

                texture.SetPixel(x, y, new Color(value, value, value));
            }
        }

        // // Mark feature points with red color
        // foreach (var item in featurePoints)
        // {
        //     texture.SetPixel(item.x, item.y, new Color(1, 0, 0));
        // }

        texture.Apply();
        return texture;
    }

    public static Texture2D GeneratePerlinNoise(int width, int height, float walkableThreshold, float noiseScale, float offsetX, float offsetY)
    {
        Texture2D texture = new Texture2D(width, height);
        texture.filterMode = FilterMode.Point;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                bool data = PerlinNoise(x, y, walkableThreshold, noiseScale, offsetX, offsetY);
                texture.SetPixel(x, y, new Color(data ? 1 : 0, data ? 1 : 0, data ? 1 : 0));
            }
        }

        texture.Apply();
        return texture;
    }

    private static bool PerlinNoise(int x, int y, float threshold, float scale, float offsetX, float offsetY)
    {
        float noiseValue = Mathf.PerlinNoise((x + offsetX) * scale, (y + offsetY) * scale);
        return noiseValue > threshold;
    }
}