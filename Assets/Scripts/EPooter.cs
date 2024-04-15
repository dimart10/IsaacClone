using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EPooter : MonoBehaviour
{

    public GameObject bulletPrefab;
    public float speed = 2f;
    public float shootDelay = 4f;
    public GameObject gunPosition;
    public GameObject gunPositionLeft;
    private bool shooting = false;
    private bool facingLeft = false;

    private Player player;
    private Rigidbody2D rb;
    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Invoke("StartShooting", shootDelay);
    }

    // Update is called once per frame
    void Update()
    {
        if (!shooting)
        {
            Chase();
        }
        facingLeft = player.transform.position.x < transform.position.x;
        anim.SetBool("facingLeft", facingLeft);
    }

    private void Chase()
    {
        rb.velocity = (player.transform.position - transform.position).normalized * speed;
    }

    private void StartShooting()
    {
        shooting = true;
        rb.velocity = Vector2.zero;
        anim.SetBool("Shooting", true);
    }

    private void EndShooting()
    {
        shooting = false;
        anim.SetBool("Shooting", false);
        Invoke("StartShooting", shootDelay);
    }

    private void Shoot()
    {
        if(facingLeft) Instantiate(bulletPrefab, gunPositionLeft.transform.position, gunPositionLeft.transform.rotation);
        else     Instantiate(bulletPrefab, gunPosition.transform.position, gunPosition.transform.rotation);
    }
    
}
