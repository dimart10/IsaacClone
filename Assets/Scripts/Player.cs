using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /*public KeyCode upKey = KeyCode.W;
    public KeyCode downKey = KeyCode.D;
    public KeyCode leftKey = KeyCode.L;
    public KeyCode rightKey = KeyCode.R;*/

    public float speed = 3f;
    //public bool moving = false;

    private Rigidbody2D rb;
    private Animator anim;
    private PlayerHead head;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        head = GetComponentInChildren<PlayerHead>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
    }

    void PlayerInput()
    {
        if (Input.GetButtonDown("Bomb")) Debug.Log("Bomba");

        // Set RigidBody velocity
        Vector2 dir = Vector2.zero;
        dir.x = Input.GetAxis("Horizontal");
        dir.y = Input.GetAxis("Vertical");

        rb.velocity = dir.normalized * speed;
        //moving = dir.magnitude > 0;

        // Set Animations
        if (dir.x > 0) // Right
        {
            anim.Play("WalkRight");
            if (!head.shooting) head.faceDir = Dir.RIGHT;
        }
        else if (dir.x < 0) // Left
        {
            anim.Play("WalkLeft");
            if (!head.shooting) head.faceDir = Dir.LEFT;
        }
        else if (dir.y > 0) // Up
        {
            anim.Play("WalkUp");
            if (!head.shooting) head.faceDir = Dir.UP;
        }
        else if (dir.y < 0) // Down
        {
            anim.Play("WalkUp");
            if (!head.shooting) head.faceDir = Dir.DOWN;
        }
        else anim.Play("Idle");
    }
}
