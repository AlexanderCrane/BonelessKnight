using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().enabled = false;
        
        Transform[] allChildren = GetComponentsInChildren<Transform>();  // collect entire sub-graph into array
        foreach(Transform t in allChildren) {
            SkinnedMeshRenderer renderer = t.GetComponent<SkinnedMeshRenderer>();
            if(renderer != null)
            {
                Debug.Log("Renderer is not null for " + t.gameObject.name);
                renderer.rootBone = null;
            }

            Rigidbody rb = t.gameObject.AddComponent<Rigidbody>();
            t.gameObject.AddComponent<SphereCollider>();
            rb.useGravity = true;
            t.DetachChildren();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
