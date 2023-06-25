using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadScript1 : MonoBehaviour
{

    Material mMaterial;
    MeshRenderer mMeshRenderer;
    public GameObject go;

    Vector2 CurrPos = new Vector3(0, 0);
    float[] mPointsX;
    float[] mPointsY;

    int mHitCount = 0;
    int Counter = 0;
    bool limitReached = false;

    float mDelay;
    private float lastEntryTime;

    private void Start()
    {
        mDelay = 3;
        mMeshRenderer = GetComponent<MeshRenderer>();
        mMaterial = mMeshRenderer.material;

        mPointsX = new float[1000];
        mPointsY= new float[1000];
        lastEntryTime = Time.time;
    }

    private void OnCollisionStay(Collision collision)
    {
        GenerateAgentAttributes collidedObjectData = collision.gameObject.GetComponent<GenerateAgentAttributes>();

        if (Time.time - collidedObjectData.HeatMapTimer >= 0.1f)
        {
            collidedObjectData.HeatMapTimer = Time.time; 
            if(collidedObjectData.HeatMapArrayPos == -1)
            {
                Debug.Log(Counter);

                if (Counter > 999) { Counter = 0; limitReached = true; }
                collidedObjectData.HeatMapArrayPos = Counter;
                Counter++;

                if (limitReached == false) mHitCount++;
            }
            foreach (ContactPoint cp in collision.contacts)
            {
                //Debug.Log("Contact with object" + cp.otherCollider.gameObject.name);
                Vector3 StartOfRay = cp.point - cp.normal;
                Vector3 RayDir = cp.normal;

                Ray ray = new Ray(StartOfRay, RayDir);
                RaycastHit hit;

                bool hitit = Physics.Raycast(ray, out hit, 10f, LayerMask.GetMask("HeatMapLayer"));

                if (hitit)
                {
                    //Debug.Log("Hit" + collision.gameObject.name + hit.collider.gameObject.name);
                    //Debug.Log("Hit Texture coords = " + hit.textureCoord.x + "," + hit.textureCoord.y);
                    addHitPoint(hit.textureCoord.x * 4 - 2, hit.textureCoord.y * 4 - 2, collidedObjectData.HeatMapArrayPos);
                }
            }
        }

    }

    public void addHitPoint(float xp, float yp, int HeatMapArrayPos)
    {
        mPointsX[HeatMapArrayPos] = xp;
        mPointsY[HeatMapArrayPos] = yp;
        mMaterial.SetFloatArray("_HitsX", mPointsX);
        mMaterial.SetFloatArray("_HitsY", mPointsY);
        mMaterial.SetInt("_HitCount", mHitCount);
    }
}
