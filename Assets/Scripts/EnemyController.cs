using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player; // Cam AR 
    public float speed;

    void Update()
    {
        Vector3 direction = player.position - transform.position;
        transform.LookAt(player);
        transform.position += direction.normalized * speed * Time.deltaTime;
    }
}