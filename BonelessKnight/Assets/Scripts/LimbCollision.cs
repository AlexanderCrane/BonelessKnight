using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbCollision : MonoBehaviour
{
    public PlayerController playerController;

    private void OnCollisionEnter(Collision other) 
    {        
        if(other.gameObject.tag == "ground")
        {
            playerController.isGrounded = true;
        }
    }
}
