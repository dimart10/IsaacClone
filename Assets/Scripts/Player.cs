using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public KeyCode upKey = KeyCode.W;
    public KeyCode downKey = KeyCode.D;
    public KeyCode leftKey = KeyCode.L;
    public KeyCode rightKey = KeyCode.R;

    public float speed = 3f;

    private Rigidbody2D rb;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
    }

    void PlayerInput()
    {
        // Set RigidBody velocity
        Vector2 dir = Vector2.zero;
        dir.x = Input.GetAxis("Horizontal");
        dir.y = Input.GetAxis("Vertical");

        rb.velocity = dir.normalized * speed;

        // Set Animations
        if (dir.x > 0) // Right
        {
            anim.Play("WalkRight");
        }
        else if (dir.x < 0) // Left
        {
            anim.Play("WalkLeft");
        }
        else if (dir.y > 0) // Up
        {
            anim.Play("WalkUp");
        }
        else if (dir.y < 0) // Down
        {
            anim.Play("WalkUp");
        }
        else anim.Play("Idle");
    }
}
