using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    public GameObject player;
    public GameObject bulletPrefab;
    public GameObject gunPos;
    public float speed = 3.5f;
    public float shotDelay = 1f;


    // Start is called before the first frame update
    void Start()
    {
        //player = FindObjectOfType<PlayerHats>();
        InvokeRepeating("AttemptShot", shotDelay, shotDelay);
    }

    // Update is called once per frame
    void Update()
    {
        


    }


    void AttemptShot()
    {
        // Máscara de capa, sólo busca colisión con las capas "Player", "Obstacles" y "Background", ignora las demás.
        int layerMask = (LayerMask.GetMask("Player", "Obstacles", "Background"));
        // Raycast, desde posición disparo, en dirección (player - posDisparo)
        RaycastHit2D hit = Physics2D.Raycast(gunPos.transform.position, player.transform.position - gunPos.transform.position, 200, layerMask);
        Debug.Log("Player pos: " + player.transform.position);
        Debug.DrawLine(gunPos.transform.position, player.gameObject.transform.position, Color.red, 1f);
        Debug.DrawLine(gunPos.transform.position, hit.point, Color.yellow, 1f);
        Debug.Log(hit.collider.gameObject.name);
        // Si el primer objeto con el que hemos chocado es el jugador (no había obstáculos en medio)
        if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // disparamos
            Shoot();
        }
    }

    void Shoot()
    {
        // disparo simple en dirección al jugador
        Instantiate(bulletPrefab, gunPos.transform.position, gunPos.transform.rotation).GetComponent<Rigidbody2D>().velocity 
            = (player.transform.position - gunPos.transform.position).normalized*speed;
    }
}
