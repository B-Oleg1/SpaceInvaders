using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    private void Update()
    {
        // If the bullet flew off the screen
        if (transform.position.y < -50)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // If the bullet collided with the player
        if (collider.gameObject.tag == "Player")
        {
            // Scoring points
            ControllGameScript.controllGameScript.HitPlayer();

            gameObject.SetActive(false);
        }
    }
}