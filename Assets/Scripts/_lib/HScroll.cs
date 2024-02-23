using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace HorzTools
{
    public class HScroll : MonoBehaviour
    {
        private Rigidbody2D _rb2d;

        public void setRigidBody(float speed)
        {
            _rb2d = GetComponent<Rigidbody2D>();
            _rb2d.bodyType = RigidbodyType2D.Kinematic;
            _rb2d.velocity = new Vector2(-speed, 0f);
        }

        public void setStop()
        {
            _rb2d.velocity = Vector2.zero;
        }
    }
}
