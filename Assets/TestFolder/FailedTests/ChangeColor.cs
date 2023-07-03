using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public GameObject quadObject;
    public Color newColor = Color.red;
    public Vector2 uvCoordinates = new Vector2(0.5f, 0.5f); // Adjust the UV coordinate as needed

    void Start()
    {
        Renderer quadRenderer = quadObject.GetComponent<Renderer>();
        Material quadMaterial = quadRenderer.material;
        Texture2D quadTexture = (Texture2D)quadMaterial.mainTexture;

        // Convert the texture to a supported format
        Texture2D convertedTexture = new Texture2D(quadTexture.width, quadTexture.height, TextureFormat.RGBA32, false);
        Graphics.ConvertTexture(quadTexture, convertedTexture);

        int x = (int)(uvCoordinates.x * convertedTexture.width);
        int y = (int)(uvCoordinates.y * convertedTexture.height);

        convertedTexture.SetPixel(x, y, newColor);

        convertedTexture.Apply();

        quadMaterial.mainTexture = convertedTexture;
    }
}
