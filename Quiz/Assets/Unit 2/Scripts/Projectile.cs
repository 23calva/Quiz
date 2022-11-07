using UnityEngine;

namespace Unit_2
{
    public class Projectile : MonoBehaviour
    {
        public Character owner;

        public Vector3 wishDir;
        public float moveSpeed = 30;

        private void FixedUpdate()
        {
            transform.Translate(moveSpeed * Time.fixedDeltaTime * wishDir);
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.TryGetComponent(out Character C))
            {
                if (owner is Player && C is Enemy)
                {
                    Destroy(other.gameObject);
                    Destroy(gameObject);
                    Game.UpdateScore(1);
                }
                else if (owner is Enemy && C is Player)
                {
                    Destroy(other.gameObject);
                    Destroy(gameObject);
                }
            }
        }
    }
}