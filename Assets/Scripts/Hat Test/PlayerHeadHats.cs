using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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

    public List<IHat> hats;

    private bool addedHatThisFrame = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        body = GetComponentInParent<Player>();
        hats = GetComponentsInChildren<IHat>().ToList(); ;
        PlaceHats();
    }

    // Update is called once per frame
    void Update()
    {
        addedHatThisFrame = false;
        HeadInput();
    }

    void PlaceHats()
    {
        for (int i = 0; i < hats.Count; i++)
        {
            hats[i].gameObject.GetComponent<SpriteRenderer>().sortingOrder = i;
            hats[i].gameObject.transform.position = gameObject.transform.position + new Vector3(0, 1*i+1, 0);
        }
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
            foreach (IHat hat in hats)
            {
                hat.AssignDirection(faceDir);
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
            if (faceDir == Dir.UP)
            {
                b.GetComponent<SpriteRenderer>().sortingLayerID = SortingLayer.NameToID("Objects");
            }
            foreach (IHat hat in hats)
            {
                hat.AssignDirection(faceDir);
                hat.Shoot();
            }

            canShoot = false;
            Invoke("EnableShoot", shootDelay);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Sombrero")
        {
            AddHat(collision.gameObject);
        }
    }

    public void AddHat(GameObject hat)
    {
        if (!addedHatThisFrame)
        {
            IHat h = hat.GetComponent<IHat>();
            Destroy(hat.GetComponent<BoxCollider2D>());
            hat.transform.SetParent(gameObject.transform);

            hats.Add(h);
            PlaceHats();
            addedHatThisFrame = true;
        }
    }

    public void EnableShoot()
    {
        canShoot = true;
    }
}
