using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player; 
    [SerializeField]
    private float speed;
    [SerializeField]
    private float distDaño;
    public GiroscopioController playerLife;
    public bool marcianitoVida;

    [Header("Particulas")]
    [SerializeField] 
    private GameObject particulas;
    [SerializeField] 
    private GameObject particulasDaño;

    [SerializeField]
    private AudioClip damage;

    void Update()
    {
        if (playerLife.isDead == true)
        {
            return;
        }

        transform.LookAt(player);
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < distDaño)
        {   
            Debug.Log("Te ha golpeado el enemigo jaja");
            playerLife.TakeDamage(1);  
            
            GameObject particulasDañ = Instantiate(particulasDaño, transform.position, Quaternion.identity);
            Destroy(particulasDañ, 1f);
            AudioManager.instance.PlaySFX(damage, transform.position);

            Destroy(gameObject);
        }
    }
    void Start()
    {
        if (marcianitoVida == true)
        {
            Instantiate(particulas, transform.position, Quaternion.identity, transform);
        }
    }
}