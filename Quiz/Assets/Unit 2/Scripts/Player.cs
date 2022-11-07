using UnityEngine;

namespace Unit_2
{
    public class Player : Character
    {
        private Vector3 wishDir;

        private void Awake()
        {
            fireRate = 1f;
            moveSpeed = 10f;
        }
        private void FixedUpdate()
        {
            transform.Translate(moveSpeed * Time.fixedDeltaTime * wishDir);
            var xPos = Mathf.Clamp(transform.position.x, -Game.extents.x, Game.extents.x);
            var zPos = Mathf.Clamp(transform.position.z, -Game.extents.z, Game.extents.z);
            transform.position = new Vector3(xPos, transform.position.y, zPos);
        }
        private void Update()
        {
            wishDir.x = Input.GetAxis("Horizontal");
            //wishDir.z = Input.GetAxis("Vertical");

            if (timeSinceLastShot >= fireRate)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Game.Spawn<Projectile>("Metal", transform.position, this);
                    timeSinceLastShot = 0f;
                }
            }
            else timeSinceLastShot += Time.deltaTime;
        }
        private void OnTriggerEnter(Collider other)
        {
            other.gameObject.TryGetComponent<Projectile>(out Projectile proj);
            if(other.gameObject.CompareTag("enemy") || proj.owner.gameObject.CompareTag("enemy"))
            {
                Destroy(gameObject);
                Game.Restart(3f);
            }
        }
    }
}