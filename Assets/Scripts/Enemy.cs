using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxLife = 5;
    private float life;
    public GameObject[] drops;
    public int[] dropsAmounts;

    // Start is called before the first frame update
    void Start()
    {
        life = maxLife;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(float amount)
    {
        life -= amount;
        if (life < 0)
        {
            life = 0;
            Death();
        }
    }

    private void Death()
    {
        // Play death anim
        DropLoot();
        Destroy(gameObject);
    }

    private void DropLoot()
    {
        for(int i = 0; i < drops.Length; i++)
        {
            for(int j = 0; j < dropsAmounts[i]; j++)
            {
                Instantiate(drops[i], transform.position, Quaternion.identity);
            }
        }
    }
}
