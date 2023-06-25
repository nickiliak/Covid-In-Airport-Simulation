using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadScript : MonoBehaviour
{

    Material mMaterial;
    MeshRenderer mMeshRenderer;
    public GameObject go;

    Vector2 CurrPos = new Vector3(0, 0);
    float[] mPoints;
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

        mPoints = new float[500 * 2];
        lastEntryTime = Time.time;
    }

    private void Update()
    {
        /*mDelay -= Time.deltaTime;
        if (mDelay <= 0)
        {
            GameObject mgo = Instantiate(go);
            mgo.transform.position = new Vector3(Random.Range(-1f,1f), Random.Range(-1f,1f), Random.Range(1f,1f));

            mDelay = 0.5f;
        }*/
    }

    private void OnCollisionStay(Collision collision)
    {

        if (Time.time - lastEntryTime >= 0.1f)
        {
            lastEntryTime = Time.time;
            foreach (ContactPoint cp in collision.contacts)
            {
                Debug.Log("Contact with object" + cp.otherCollider.gameObject.name);
                Vector3 StartOfRay = cp.point - cp.normal;
                Vector3 RayDir = cp.normal;

                Ray ray = new Ray(StartOfRay, RayDir);
                RaycastHit hit;

                bool hitit = Physics.Raycast(ray, out hit, 10f, LayerMask.GetMask("HeatMapLayer"));

                if (hitit)
                {
                    Debug.Log("Hit Object " + hit.collider.gameObject.name);
                    Debug.Log("Hit Texture coords = " + hit.textureCoord.x + "," + hit.textureCoord.y);
                    addHitPoint(hit.textureCoord.x * 4 - 2, hit.textureCoord.y * 4 - 2);
                }

                //Destroy(cp.otherCollider.gameObject);
            }
        }

    }

    public void addHitPoint(float xp, float yp)
    {
        Debug.Log(Counter);

        if (Counter > 10) { Counter = 0; limitReached = true; }
        else Counter = Counter + 2;

        mPoints[Counter] = xp;
        mPoints[Counter + 1] = yp;
        if(limitReached == false) mHitCount = mHitCount + 2;

        mMaterial.SetFloatArray("_Hits", mPoints);
        mMaterial.SetInt("_HitCount", mHitCount); 
    }
}
