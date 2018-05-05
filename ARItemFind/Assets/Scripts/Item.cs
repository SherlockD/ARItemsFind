using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Design/New Item")]
public class Item : ScriptableObject {

    public string Itemname;
    public GameObject Prefub;
    public Vector3 LocalScale;
    public Vector3 Rotation;

}
