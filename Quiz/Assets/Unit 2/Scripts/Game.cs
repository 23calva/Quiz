using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Unit_2
{
    public class Game : MonoBehaviour
    {
        public static Vector3 extents = new Vector3(9, 0, 9);

        private static TextMeshProUGUI scoreHud;
        private static TextMeshProUGUI timerHud;
        private static int score;

        private static float timeSinceLastEnemy;
        private static float spawnRate = 1f;

        private static float endTime;
        private static int lastTime;
        private static bool reset;

        private void Awake()
        {
            scoreHud = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
            timerHud = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();
            UpdateScore();
        }
        private void Update()
        {
            // Restart Game.
            if(reset)
            {
                UpdateTimer(true);
                if(endTime <= Time.realtimeSinceStartup)
                {
                    reset = false;
                    SceneManager.LoadScene("Unit 2");
                    Time.timeScale = 1;
                    UpdateScore(-score);
                }
            }
            else UpdateTimer(false);

            // Spawn Random Enemy at an Interval.
            if(timeSinceLastEnemy >= spawnRate)
            {
                timeSinceLastEnemy = 0f;
                switch (Random.Range(0, 2))
                {
                    case 0: Spawn<Enemy>("Black", RandomSpawnPosition()); break;
                    case 1: Spawn<Enemy>("Blue", RandomSpawnPosition()); break;
                }
            }
            else timeSinceLastEnemy += Time.deltaTime;

            // Destroy Objects Out of Bounds.
            foreach(var obj in FindObjectsOfType<GameObject>())
            {
                if(obj.name == "Blue" || obj.name == "Black" || obj.name == "Orange" || obj.name == "Metal")
                {
                    if (obj.transform.position.x < -extents.x || obj.transform.position.x > extents.x || obj.transform.position.z < -extents.z || obj.transform.position.z > extents.z)
                    {
                        Destroy(obj.gameObject);
                    }
                }
            }
        }
        public static GameObject Spawn<T>(string name, Vector3 pos, Character owner = null) where T : MonoBehaviour
        {
            // Create Object & Components.
            var obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            obj.GetComponent<MeshRenderer>().material = Resources.Load<Material>($"Materials/{name}");
            obj.GetComponent<Collider>().isTrigger = true;
            obj.AddComponent<Rigidbody>().isKinematic = true;
            var component = obj.AddComponent<T>();

            // Initialize Values.
            obj.name = name;
            obj.transform.position = pos;
            if(component is Enemy enemy)
            {
                enemy.tag = "enemy";
                if(name == "Black")
                {
                    enemy.fireRate = 1.5f;
                    enemy.moveSpeed = 1;
                }
                else if(name == "Blue")
                {
                    enemy.fireRate = 0;
                    enemy.moveSpeed = 1;
                }
            }
            else if(component is Projectile proj)
            {
                proj.transform.position += obj.transform.forward * 1;
                if(name == "Orange")
                {
                    proj.transform.localScale = Vector3.one / 2;
                    proj.transform.eulerAngles = Vector3.back;
                    proj.wishDir = Vector3.back;
                    proj.owner = owner;
                }
                else if(name == "Metal")
                {
                    proj.wishDir = Vector3.forward;
                    proj.owner = owner;
                }
            }
            return obj;
        }
        public Vector3 RandomSpawnPosition() => new Vector3(Random.Range(-extents.x, extents.x), 1, extents.z);
        public static void UpdateTimer(bool active)
        {
            if(!active)
            {
                timerHud.text = "";
                lastTime = -1;
                return;
            }

            int num = (int)(endTime - Time.realtimeSinceStartup);

            if(lastTime != num)
            {
                lastTime = num;
                timerHud.transform.localScale = Vector3.one * 3;
                timerHud.text = (num + 1).ToString();
            }

            // Resize text.
            if(Vector3.Distance(timerHud.transform.localScale, Vector3.one) >= 1)
            {
                timerHud.transform.localScale += Vector3.one * -0.1f;
            }
            else timerHud.transform.localScale = Vector3.one;
        }
        public static void UpdateScore(int amt = 0)
        {
            scoreHud.text = $"Score: {score += amt}";
        }
        public static void Restart(float delay)
        {
            Time.timeScale = 0;
            endTime = Time.realtimeSinceStartup + delay;
            reset = true;
        }
    }
}