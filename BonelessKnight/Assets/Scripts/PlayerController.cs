using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float strafeSpeed;
    public float jumpForce;
    public Rigidbody hips;
    public bool isGrounded = true;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void FixedUpdate() 
    {

        if(Mathf.Abs(hips.velocity.magnitude) > 1)
        {
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }

        // Note: ALL of the transform directions (transform.up, etc) are "incorrect" because the rig is set up this way and we aren't taking the time to fix it at the moment

        if(Input.GetKey(KeyCode.W))
        {
            if(Input.GetKey(KeyCode.LeftShift))
            {
                Vector3 cameraFacingWithoutX = Vector3.Normalize(new Vector3(Camera.main.transform.forward.x, Camera.main.transform.position.y, Camera.main.transform.forward.z));
                hips.AddForce(cameraFacingWithoutX * (speed * 1.5f));

            }
            else
            {
                Vector3 cameraFacingWithoutX = Vector3.Normalize(new Vector3(Camera.main.transform.forward.x, Camera.main.transform.position.y, Camera.main.transform.forward.z));
                hips.AddForce(cameraFacingWithoutX * speed);    
            }
        }

        if(Input.GetKey(KeyCode.A))
        {
            hips.AddForce(hips.transform.forward * speed);
        }

        if(Input.GetKey(KeyCode.S))
        {
            Vector3 cameraFacingWithoutX = Vector3.Normalize(new Vector3(Camera.main.transform.forward.x, Camera.main.transform.position.y, Camera.main.transform.forward.z));
            hips.AddForce(-cameraFacingWithoutX * speed);
        }

        if(Input.GetKey(KeyCode.D))
        {
            hips.AddForce(-hips.transform.forward * speed);
        }

        if(Input.GetAxis("Jump") > 0 && isGrounded)
        {
            Debug.Log("Jumping");
            hips.AddForce(new Vector3(0, jumpForce, 0));
            isGrounded = false;
        }
    }
}
