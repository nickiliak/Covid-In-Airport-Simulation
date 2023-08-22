using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollTextureAnim : MonoBehaviour
{

    public float Scrollx = 0.5f;
    public float Scrolly = 0.5f;

    private void Update()
    {
        float offsetx = Time.time * Scrollx;
        float offsety = Time.time * Scrolly;
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(offsety, offsetx);
    }
}
