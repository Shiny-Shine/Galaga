using HorzTools;
using UnityEngine;

[RequireComponent (typeof(BoxCollider2D))]
public class RepeatBackground : HRepeat {
    void Start () {
        setBoxCollider ();
    }

    void Update () {
        updateObject ();
    }
}