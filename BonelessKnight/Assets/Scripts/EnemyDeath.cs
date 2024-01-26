using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDeath : MonoBehaviour
{    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().enabled = false;
        
        Transform[] allChildren = GetComponentsInChildren<Transform>(true);  // collect entire sub-graph into array
        foreach(Transform t in allChildren) {
            if(t.gameObject.name != "Skeleton")
            {
                t.gameObject.SetActive(!t.gameObject.activeInHierarchy);
            }
            else
            {
                t.GetComponent<Animator>().enabled = false;
                t.GetComponent<CapsuleCollider>().enabled = false;
                t.GetComponent<NavMeshAgent>().enabled = false;
                t.GetComponent<Animator>().enabled = false;
                
            }
        }

    }
}
