using UnityEngine;


public class NoiseVisualizer : MonoBehaviour
{
    public int width = 256;
    public int height = 256;
    [HideInInspector] public Texture2D texture;

    protected void UpdateTextureOnMaterial()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.mainTexture = texture;
        }
    }
}