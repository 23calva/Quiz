using UnityEngine;

namespace Unit_2
{
    public class Enemy : Character
    {
        private void FixedUpdate()
        {
            transform.Translate(moveSpeed * Time.fixedDeltaTime * Vector3.back);
        }
        private void Update()
        {
            // 0 FireRate = No Bullets.
            if(fireRate > 0f)
            {
                if(timeSinceLastShot >= fireRate)
                {
                    Game.Spawn<Projectile>("Orange", transform.position, this);
                    timeSinceLastShot = 0f;
                }
                else timeSinceLastShot += Time.deltaTime;
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            bool isProj = other.gameObject.TryGetComponent<Projectile>(out Projectile proj);
            if (isProj && proj.owner.gameObject.CompareTag("Player"))
            {
                Game.UpdateScore(1);
                Destroy(gameObject);
                Destroy(proj.gameObject);
            }
        }
    }
}