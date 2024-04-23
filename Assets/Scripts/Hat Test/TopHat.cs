using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TopHat : MonoBehaviour, IHat
{
    public GameObject _bottomAnchor;
    public GameObject bottomAnchor 
    {
        get { return _bottomAnchor; }
        set { _bottomAnchor = value; }
    }
    // esto de la implementación de las variables lo he visto en el chatGPT, 
    // me lo tengo que mirar más para ver como va el tema de la variable segunda

    public GameObject _topAnchor;
    public GameObject topAnchor 
    {
        get { return _topAnchor; }
        set { _topAnchor = value; }
    }

    public Dir _hDirection;
    public Dir hDirection
    {
        get { return _hDirection; }
        set { _hDirection = value; }
    }

    public GameObject bulletPrefab;
    public GameObject[] gunPositions;
    private Animator anim;

    public void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Shoot()
    {
        Bullet b = Instantiate(bulletPrefab, gunPositions[(int)hDirection].transform.position, Quaternion.identity).GetComponent<Bullet>();
        b.SetBulletDir(hDirection);
        anim = GetComponent<Animator>();
    }

    public void AssignDirection(Dir dir)
    {
        hDirection = dir;
        switch(dir)
            {
                case Dir.UP: anim.Play("Up"); break;
                case Dir.DOWN: anim.Play("Down"); break;
                case Dir.RIGHT: anim.Play("Right"); break;
                case Dir.LEFT: anim.Play("Left"); break;
                //default: anim.Play("IdleDown"); break;
            }
        
    }
}
