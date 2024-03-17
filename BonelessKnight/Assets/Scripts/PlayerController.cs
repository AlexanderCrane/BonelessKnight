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
    private int health = 100;

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
        // cameraFacingConvertedToY = Vector3.Normalize(new Vector3(0, Camera.main.transform.rotation.eulerAngles.y, 0));
        
        Vector3 controlDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 flattenedVector = Vector3.ProjectOnPlane(controlDirection, Vector3.up);
        Vector3 actualDirection = Camera.main.transform.TransformDirection(flattenedVector);

        Vector3 leftDirection = Quaternion.AngleAxis(90, Vector3.up) * actualDirection;
        // Debug.Log("rotated 90 degrees left is " + actualDirection);

        // Vector3 rightDirection = Quaternion.AngleAxis(90, Vector3.up) * actualDirection;

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
        if(Mathf.Abs(Input.GetAxis("Horizontal")) > 0 || Mathf.Abs(Input.GetAxis("Vertical")) > 0)
        {
            if(Input.GetButton("Run"))
            {
                hips.AddForce(actualDirection * (speed * 1.5f));

            }
            else
            {
                hips.AddForce(actualDirection * speed);    
            }
        }

        // if(Input.GetKey(KeyCode.A))
        // {
        //     hips.AddForce(actualDirection * speed);
        // }

        // if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        // {
        //     hips.AddForce(actualDirection * speed);
        // }

        // if(Input.GetKey(KeyCode.D))
        // {
        //     hips.AddForce(-actualDirection * speed);
        // }

        if((Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1")) && isGrounded)
        {
            Debug.Log("Jumping");
            hips.AddForce(new Vector3(0, jumpForce, 0));
            isGrounded = false;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
