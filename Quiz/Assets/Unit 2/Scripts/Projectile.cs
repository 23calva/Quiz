using UnityEngine;

namespace Unit_2
{
    public class Projectile : MonoBehaviour
    {
        public Character owner;

        public Vector3 wishDir;
        public float moveSpeed = 20;

        private void FixedUpdate()
        {
            transform.Translate(moveSpeed * Time.fixedDeltaTime * wishDir);
        }
    }
}