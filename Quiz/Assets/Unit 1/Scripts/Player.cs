using UnityEngine;

namespace Unit_1
{
    public class Player : MonoBehaviour
    {
        private Camera mainCamera;
        private Vector3 wishVelocity;

        public float speed = 22.5f;

        private void Start()
        {
            Debug.Log("Game Started");
            mainCamera = FindObjectOfType<Camera>();
        }


        private void FixedUpdate()
        {
            transform.Translate(speed * Time.fixedDeltaTime * wishVelocity);
        }

        private void Update()
        {
            wishVelocity.x = Input.GetAxis("Horizontal");
            wishVelocity.z = Input.GetAxis("Vertical");
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("coin"))
            {
                Debug.Log("Hit Coin");
                Destroy(collision.gameObject);
            }
            if (collision.gameObject.CompareTag("enemy"))
            {
                Debug.Log("Hit Enemy");
                Destroy(this.gameObject);
            }
        }
    }
}