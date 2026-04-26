using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player; // cam
    [SerializeField]
    private float speed;
    [SerializeField]
    private float distDaño;
    public GiroscopioController playerLife;

    [SerializeField]
    private AudioClip damage;

    void Update()
    {
        transform.LookAt(player);
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < distDaño)
        {   
            Debug.Log("Te ha golpeado el enemigo jaja");
            
            AudioManager.instance.PlaySFX(damage, transform.position);
            Destroy(gameObject);
        }
    }
}