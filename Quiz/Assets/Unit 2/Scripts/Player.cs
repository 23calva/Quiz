using UnityEngine;
using UnityEngine.UI;

namespace Unit_2
{
    public class Player : Character
    {
        private Vector3 wishDir;
        private Slider wpnRechargeProgress;

        private void Awake()
        {
            wpnRechargeProgress = GameObject.Find("WeaponRechargeProgress").GetComponent<Slider>();

            fireRate = 1f;
            moveSpeed = 10f;
        }
        private void FixedUpdate()
        {
            transform.Translate(moveSpeed * Time.fixedDeltaTime * wishDir);
            var xPos = Mathf.Clamp(transform.position.x, -Game.extents.x, Game.extents.x);
            var zPos = Mathf.Clamp(transform.position.z, -Game.extents.z, Game.extents.z);
            transform.position = new Vector3(xPos, transform.position.y, zPos);

            //wpnRechargeProgress.transform.position += 450 * Time.fixedDeltaTime * wishDir;
            var pos = Game.cam.WorldToScreenPoint(transform.position);
            pos.y = wpnRechargeProgress.transform.position.y;
            wpnRechargeProgress.transform.position = pos;
        }
        private void Update()
        {
            wishDir.x = Input.GetAxis("Horizontal");
            //wishDir.z = Input.GetAxis("Vertical");

            wpnRechargeProgress.value = timeSinceLastShot;

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
            if(other.gameObject.CompareTag("enemy"))
            {
                Destroy(gameObject);
                Destroy(wpnRechargeProgress.gameObject);
                Game.Restart(3f);
            }
        }
    }
}