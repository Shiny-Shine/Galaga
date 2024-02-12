using UnityEngine;

namespace HorzTools {
    public class HRepeat : MonoBehaviour {
        private BoxCollider2D box;
        private float hLength;

        public void setBoxCollider () {
            box = GetComponent<BoxCollider2D> ();
            hLength = box.size.x;
        }

        public void updateObject () {
            if (transform.position.x < -hLength) {
                Vector3 addPos = new Vector3 (2 * hLength, 0, 0);
                transform.position = transform.position + addPos;
            }
        }
    }
}