using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    private Vector2[] direcciones = { new Vector2(0, 1), new Vector2(0, -1), new Vector2(-1, 0), new Vector2(1, 0) };

    public float speed = 6f;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void SetBulletDir(Dir dir)
    {
        rb.velocity = direcciones[(int)dir] * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Background")
        {
            Death();
        }
    }


    private void Death()
    {
        Destroy(this);
    }
}
