using UnityEngine;

namespace HorzTools {
    public class HScroll : MonoBehaviour {
        private Rigidbody2D rb;

        public void setRigidbody (float speed) {
            rb = GetComponent<Rigidbody2D> ();
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.velocity = new Vector2 (-speed, 0f);
        }

        public void setStop () {
            rb.velocity = Vector2.zero;
        }
    }
}