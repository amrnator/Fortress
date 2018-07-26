using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Resource", menuName = "Resources")]
public class Resource : ScriptableObject {

    public string resourceName;

    //3d model of object when dropped
    public GameObject model;
}