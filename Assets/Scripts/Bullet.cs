using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2[] direcciones = { new Vector2(0, 1), new Vector2(0, -1), new Vector2(-1, 0), new Vector2(1, 0) };
    public Rigidbody2D rb;

    public float damage = 1f;
    public float speed = 6f;
    public GameObject deathParticle;

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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Background" || LayerMask.LayerToName(collision.gameObject.layer) == "Obstacles")
        {
            Death();
        }
        else if (LayerMask.LayerToName(collision.gameObject.layer) == "Enemies")
        {
            collision.gameObject.GetComponent<Enemy>().Damage(damage);
            Death();
        }
    }

    private void Death()
    {
        Destroy(gameObject);
        Instantiate(deathParticle, transform.position, Quaternion.identity);
    }
}
