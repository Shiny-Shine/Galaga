using UnityEngine;

namespace HorzTools
{
    public class HRepeat : MonoBehaviour
    {
        private BoxCollider2D _box;
        private float _horizontalLength;

        public void setBoxCollider()
        {
            _box = GetComponent<BoxCollider2D>();
            _horizontalLength = _box.size.x;
        }

        public void updateObject()
        {
            if (transform.position.x < -_horizontalLength)
                ResetPosition();
        }

        private void ResetPosition()
        {
            Vector2 addPos = new Vector2(2 * _horizontalLength, 0f);
            transform.position = (Vector2)transform.position + addPos;
        }
    }
}
