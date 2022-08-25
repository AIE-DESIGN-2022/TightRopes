using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideNSeek : MonoBehaviour
{
    // Detects manually if obj is being seen by the main camera

    public GameObject obj;
    public Collider objCollider;

    public Camera cam;
    public Plane[] planes;
    public bool hasSeen;
    void Start()
    {
        hasSeen = false;
        obj = this.gameObject;
        cam = Camera.main;
        objCollider = GetComponent<Collider>();

    }

    void Update()
    {
        planes = GeometryUtility.CalculateFrustumPlanes(cam);
        gameObject.SetActive(true);
        if (GeometryUtility.TestPlanesAABB(planes, objCollider.bounds))
        {
            hasSeen = true;
            Debug.Log(obj.name + " nice ain't it?");
        }
        else
        {
            if (hasSeen) { obj.SetActive(false); }
            Debug.Log("nothing to see here");
        }
    }
}
