using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player; // cam
    public float speed;

    void Update()
    {
        transform.LookAt(player);
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }
}