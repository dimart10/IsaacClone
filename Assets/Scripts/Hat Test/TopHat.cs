using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TopHat : MonoBehaviour, IHat
{
    public Vector3 bottomAnchor { get; set; }
    /*{
        get { return bottomAnchor; }
        set { bottomAnchor = value; }
    }*/
    public Vector3 topAnchor { get; set; }
    /*{
        get { return topAnchor; }
        set { topAnchor = value; }
    }*/
    public Dir hDirection { get; set; }
    /*{
        get { return hDirection; }
        set {  hDirection = value; }
    }*/

    public GameObject bulletPrefab;
    public GameObject[] gunPositions;

    public void Shoot()
    {
        Bullet b = Instantiate(bulletPrefab, gunPositions[(int)hDirection].transform.position, Quaternion.identity).GetComponent<Bullet>();
        b.SetBulletDir(hDirection);
    }

    public void AssignDirection(Dir dir)
    {
        hDirection = dir;
    }
}
