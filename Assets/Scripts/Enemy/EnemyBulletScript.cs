using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    private void Update()
    {
        // Если пуля улетела за экран
        if (transform.position.y < -50)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Если пуля столкнулась с игроком
        if (collider.gameObject.tag == "Player")
        {
            // Начисление очков
            ControllGameScript.controllGameScript.HitPlayer();

            gameObject.SetActive(false);
        }
    }
}