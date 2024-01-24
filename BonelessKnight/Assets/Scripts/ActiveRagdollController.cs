using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ActiveRagdollController : MonoBehaviour
{
    public List<Transform> animatedTransforms;
    public List<Transform> physicalJoints;
    public List<Quaternion> originalRotations;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform t in physicalJoints)
        {
            // ConfigurableJoint c = t.GetComponent<ConfigurableJoint>();
            if(t.name != "Bip001")
            {
                originalRotations.Add(t.rotation);
            }
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (var (joint, index) in physicalJoints.WithIndex())
        {
            if(joint.name == animatedTransforms[index].name)
            {
                joint.rotation = animatedTransforms[index].rotation;
            }
        }

        // for(int i = 0; i < 17; i++)
        // {
            // physicalJoints[i].rotation = animatedTransforms[i].rotation;
        // }
    }
}
    
public static class IEnumerableExtensions {
    public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> self)       
    => self.Select((item, index) => (item, index));
}

