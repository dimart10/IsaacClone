using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public interface IHat
{
    void Shoot();
    void AssignDirection(Dir dir);

    // Anchor positions for other hats
    Vector3 bottomAnchor { get; set; }
    Vector3 topAnchor { get; set;  }
    Dir hDirection { get; set; }
}
