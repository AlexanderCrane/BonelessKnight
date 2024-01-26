using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyMotion : MonoBehaviour
{
    public Transform targetLimb;
    ConfigurableJoint configurableJoint; 

    // Start is called before the first frame update
    void Start()
    {
        configurableJoint = GetComponent<ConfigurableJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        configurableJoint.targetRotation = targetLimb.rotation;
    }
}
