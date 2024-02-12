using UnityEngine;

[RequireComponent (typeof(BoxCollider2D))]
public class Columns : MonoBehaviour {
    private BoxCollider2D box;

    void Start () {
        box = GetComponent<BoxCollider2D> ();
        box.isTrigger = true;
    }

    void OnTriggerEnter2D (Collider2D other) {
        if (other.gameObject.GetComponent<Bird> () != null) {
            FlappyManager.instFM.SetAddScore ();
        }
    }
}