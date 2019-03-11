using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 6f;
    private Vector3 movement;
    private Animator animator;
    private Rigidbody playerRigidbody;
    private float RayLenght = 100f;
    private int floorMask;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        floorMask = LayerMask.GetMask("Floor");
    }

    void Turning()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorRaycastHit;
        if (Physics.Raycast(ray,out floorRaycastHit,RayLenght,floorMask))
        {
            Vector3 playerToMouse = floorRaycastHit.point - transform.position;
            playerToMouse.y = 0;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(newRotation);
        }
    }

    void Move(float h ,float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + movement);
    }

    void Animating(float h, float v)
    {
        bool walking = h != 0 || v != 0;
        animator.SetBool("isWalk", walking);
    }

    private void FixedUpdate()
    {    
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Move(h,v);
        Turning();
        Animating(h, v);
    }
}
