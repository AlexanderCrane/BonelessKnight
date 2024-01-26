using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Vector3 offset;
    public Transform player;
    [Space]
    [Range(0f, 10f)]
    public float turnSpeed = 5f;
    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 0.5f;

    private void Start()
    {
        offset = transform.position - player.transform.position;
    }
    private void Update()
    {
        // if(Input.GetAxis("Joystick X") != 0.0f ||
        //     Input.GetAxis("Joystick Y") != 0.0f )
        // {
        //     Debug.Log("Changing rotation speed for controller");
        //     turnSpeed = 30f;
        // }
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * turnSpeed, Vector3.right) * offset;
        Vector3 newPos = player.transform.position + offset;
        transform.position = Vector3.Lerp(transform.position, newPos, SmoothFactor);
        // transform.position = player.position + offset;
        transform.LookAt(player.position);
    }
}
