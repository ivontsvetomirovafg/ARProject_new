using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.UI;

public class GiroscopioController : MonoBehaviour
{
    [SerializeField]
    private Transform cam;
    public int life;

    [SerializeField]
    public GameObject gameOver;
    [SerializeField]
    public GameObject winPanel;
    [SerializeField]
    private GameObject[] heart;
    [SerializeField]
    private GameObject[] marcianito;

    [SerializeField]
    private float tiempoSpawn;
    private float timePass;
    [SerializeField]
    private float minimX, minimZ, maxX, maxZ;
    private bool canShoot = true;
    public bool isDead = false;

    [SerializeField]
    private AudioClip kill;
    [SerializeField]
    private AudioClip gameOverSFX;

    [SerializeField]
    private int killCount;
    [SerializeField]
    private Text killText;
    [SerializeField] 
    private GameObject particulasDisparo;

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
        if (isDead == true) 
        {
            return;
        }
        
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
        if (isDead == true) 
        {
            return;
        }

        if (canShoot == false)
        {
            return;
        }

        canShoot = false;
        Ray ray = Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f));
        RaycastHit hit;

        AudioManager.instance.PlaySFX(kill, transform.position);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("EnemyMuerto");          
                GameObject particulasDisp = Instantiate(particulasDisparo, hit.point, Quaternion.identity);
                Destroy(particulasDisp, 2f);
                
                Destroy(hit.transform.root.gameObject); //para eliminar el padre xd
                killCount++;
                killText.text = "x" + killCount;  
                              
                EnemyController enemy = hit.transform.GetComponentInParent<EnemyController>();
                if (enemy.marcianitoVida == true)
                {
                    Vida(1); 
                }
            }
        }
        StartCoroutine(ShootAgain());
    }
    public void Vida(int _vida)
    {
        if (life >= 4) 
        {
            return;
        }

        heart[life].SetActive(true);
        life++;
    }

    IEnumerator ShootAgain()
    {
        yield return new WaitForSeconds(0.5f);
        canShoot = true;
    }

    public void TakeDamage(int damage)
    {
        life -= damage;
        heart[life].SetActive(false);

        if (life <= 0)
        {
            AudioManager.instance.PlaySFX(gameOverSFX, transform.position);
            AudioManager.instance.StopMusic();
            isDead = true;
            gameOver.SetActive(true);
        }
    }

    public void Win()
    {
        if (killCount == 20)
        {
            isDead = true;
            winPanel.SetActive(true);
        }
    }
}
