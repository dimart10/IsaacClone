using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public interface IHat
{
    void Shoot();
    void AssignDirection(Dir dir);

    // Anchor positions for other hats
    GameObject bottomAnchor { get; set; }
    GameObject topAnchor { get; set;  }
    Dir hDirection { get; set; }

    GameObject gameObject { get; }
}
