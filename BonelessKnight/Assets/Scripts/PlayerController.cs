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
    private Vector3 cameraFacingWithoutX;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            Debug.Log("Quitting game");
            Application.Quit();
        }
    }

    private void FixedUpdate() 
    {
        cameraFacingWithoutX = Vector3.Normalize(new Vector3(Camera.main.transform.forward.x, Camera.main.transform.position.y, Camera.main.transform.forward.z));

        // transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(cameraFacingWithoutX), Time.deltaTime * 3);

        // hips.rotation = Vector3.Normalize(new Vector3(Camera.main.transform.forward.x, Camera.main.transform.position.y, Camera.main.transform.forward.z)); 

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
                hips.AddForce(cameraFacingWithoutX * (speed * 1.5f));

            }
            else
            {
                hips.AddForce(cameraFacingWithoutX * speed);    
            }
        }

        if(Input.GetKey(KeyCode.A))
        {
            hips.AddForce(hips.transform.forward * speed);
        }

        if(Input.GetKey(KeyCode.S))
        {
            hips.AddForce(-cameraFacingWithoutX * speed);
        }

        if(Input.GetKey(KeyCode.D))
        {
            hips.AddForce(-hips.transform.forward * speed);
        }

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Debug.Log("Jumping");
            hips.AddForce(new Vector3(0, jumpForce, 0));
            isGrounded = false;
        }
    }
}
