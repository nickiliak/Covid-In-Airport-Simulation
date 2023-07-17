using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdDensity : MonoBehaviour
{

    Material mMaterial;
    MeshRenderer mMeshRenderer;

    public float[] mPointsX;
    public float[] mPointsY;
    public float[] ValidPoints;

    int mHitCount = 0;
    int Counter = 0;
    bool limitReached = false;

    private void Start()
    {
        mMeshRenderer = GetComponent<MeshRenderer>();
        mMaterial = mMeshRenderer.material;

        mPointsX = new float[1000];
        mPointsY = new float[1000];
        ValidPoints = new float[1000];
        Array.Fill(ValidPoints, 1f);
    }

    private void OnCollisionStay(Collision collision)
    {
        AgentData collidedObjectData = collision.gameObject.GetComponent<AgentData>();

        if (collidedObjectData != null)
        {
            if(collidedObjectData.CrowdDensityPos == -1)
            {
                if (Counter > 999) { Counter = 0; limitReached = true; }
                collidedObjectData.CrowdDensityPos = Counter;
                ValidPoints[Counter] = 1f;
                Counter++;
                if (limitReached == false) mHitCount++;
            }

            ContactPoint cp = collision.GetContact(0);
            Vector3 StartOfRay = cp.point - cp.normal;
            Vector3 RayDir = cp.normal;

            Ray ray = new Ray(StartOfRay, RayDir);
            RaycastHit hit;

            bool hitit = Physics.Raycast(ray, out hit, 10f, LayerMask.GetMask("HeatMapLayer"));

            if (hitit)
                addHitPoint(hit.textureCoord.x * 4 - 2, hit.textureCoord.y * 4 - 2, collidedObjectData.CrowdDensityPos);
                
        }

    }

    public void addHitPoint(float xp, float yp, int HeatMapArrayPos)
    {
        mPointsX[HeatMapArrayPos] = xp;
        mPointsY[HeatMapArrayPos] = yp;
        mMaterial.SetFloatArray("_HitsX", mPointsX);
        mMaterial.SetFloatArray("_HitsY", mPointsY);
        mMaterial.SetFloatArray("_ValidHits", ValidPoints);
        mMaterial.SetInt("_HitCount", mHitCount);
    }
}
