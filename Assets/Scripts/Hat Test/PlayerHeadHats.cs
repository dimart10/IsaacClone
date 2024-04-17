using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerHeadHats : MonoBehaviour
{
    [HideInInspector]
    public Animator anim;

    [HideInInspector]
    public bool shooting = false;
    public bool canShoot = true;

    public float shootDelay = 0.3f;

    private Player body;

    public Dir faceDir = Dir.DOWN;

    public GameObject[] gunPositions;
    public GameObject bullet;

    public IHat[] hats;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        body = GetComponentInParent<Player>();
        hats = GetComponentsInChildren<IHat>();
    }

    // Update is called once per frame
    void Update()
    {
        HeadInput();
    }

    void HeadInput()
    {
        // Set RigidBody velocity
        Vector2 dir = Vector2.zero;
        dir.x = Input.GetAxis("HorizontalShoot");
        dir.y = Input.GetAxis("VerticalShoot");


        // Set Animations
        if (dir.x > 0) // Right
        {
            anim.Play("ShootRight");
            faceDir = Dir.RIGHT;
            Shoot();
        }
        else if (dir.x < 0) // Left
        {
            anim.Play("ShootLeft");
            faceDir = Dir.LEFT;
            Shoot();
        }
        else if (dir.y > 0) // Up
        {
            anim.Play("ShootUp");
            faceDir = Dir.UP;
            Shoot();
        }
        else if (dir.y < 0) // Down
        {
            anim.Play("ShootDown");
            faceDir = Dir.DOWN;
            Shoot();
        }
        else
        {
            switch (faceDir)
            {
                case Dir.UP: anim.Play("IdleUp"); break;
                case Dir.DOWN: anim.Play("IdleDown"); break;
                case Dir.RIGHT: anim.Play("IdleRight"); break;
                case Dir.LEFT: anim.Play("IdleLeft"); break;
                //default: anim.Play("IdleDown"); break;
            }
        }
        shooting = dir.magnitude > 0;
    }

    public void Shoot()
    {
        if (canShoot)
        {
            Bullet b = Instantiate(bullet, gunPositions[(int)faceDir].transform.position, Quaternion.identity).GetComponent<Bullet>();
            b.SetBulletDir(faceDir);

            foreach (IHat hat in hats)
            {
                hat.AssignDirection(faceDir);
                hat.Shoot();
            }

            canShoot = false;
            Invoke("EnableShoot", shootDelay);
        }
    }

    public void EnableShoot()
    {
        canShoot = true;
    }
}
