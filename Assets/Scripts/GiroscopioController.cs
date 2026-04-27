using UnityEngine;
using UnityEngine.InputSystem;

public class GiroscopioController : MonoBehaviour
{
    [SerializeField]
    private Transform cam;
    public int life;
    [SerializeField]
    public GameObject gameOver;
    [SerializeField]
    private GameObject[] heart;
    [SerializeField]
    private float tiempoSpawn;
    private float timePass;
    [SerializeField]
    private float minimX, minimZ, maxX, maxZ;
    [SerializeField]
    private GameObject[] marcianito;

    [SerializeField]
    private AudioClip kill;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Input.gyro.enabled = true;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (SystemInfo.supportsGyroscope == true) // nos devuelve la rot del dispositivo
        {
            Quaternion inputGyro = Input.gyro.attitude;

            //invertimos el eje z y w del quaternion para que la rotacion del giroscopio encaje con la de la cam en coordenadas de Unity.
            //cam.rotation = new Quaternion(inputGyro.x, inputGyro.y, - inputGyro.z, -inputGyro.w);

            Quaternion correcionGiro = Quaternion.Euler (90, 0, 0);
            cam.rotation = correcionGiro * new Quaternion(inputGyro.x, inputGyro.y, -inputGyro.z, -inputGyro.w);
        }
    }
    private void Update()
    {
        timePass += Time.deltaTime;
        if (timePass >= tiempoSpawn)
        {
            timePass = 0;
            float x = Random.Range(minimX, maxX);
            float z = Random.Range(minimZ, maxZ);

            int marcianitoCogido = Random.Range (0, marcianito.Length);
            GameObject enemigo = Instantiate(marcianito[marcianitoCogido], new Vector3 (x, 0, z), Quaternion.identity);
            enemigo.GetComponent<EnemyController>().player = cam;

            EnemyController enemyScript = enemigo.GetComponent<EnemyController>();
            enemyScript.playerLife = GetComponent<GiroscopioController>();     
        }
    }

    public void TouchScreen(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            //AudioManager.instance.PlaySFX(kill, transform.position);
            Debug.Log("Disparas");

            if (hit.transform.CompareTag("Enemy"))
            {               
                Destroy(hit.transform.gameObject);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        life -= damage;
        heart[life].SetActive(false);

        if (life <= 0)
        {
            gameOver.SetActive(true);
        }
    }
}
