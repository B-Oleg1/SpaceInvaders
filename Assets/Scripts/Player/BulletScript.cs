using UnityEngine;
using UnityEngine.UI;

public class BulletScript : MonoBehaviour
{
    private Rigidbody2D _bulletRigidbody2D;

    private float _bulletSpeed = 5.5f;

    private void Start()
    {
        _bulletRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // If the bullet flew off the screen
        if (transform.localPosition.y > Screen.height + 50)
        {
            gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        // Constant upward movement at the same speed
        _bulletRigidbody2D.velocity = new Vector2(0, _bulletSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // If the bullet collided with the enemy
        if (collider.gameObject.tag == "Enemy")
        {
            collider.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;

            collider.gameObject.tag = "EnemyKilled";

            var alpha = collider.gameObject.GetComponent<Image>().color;
            alpha.a = 0;
            collider.gameObject.GetComponent<Image>().color = alpha;

            // Scoring points
            ControllGameScript.controllGameScript.KillEnemy(transform);

            gameObject.SetActive(false);
        }
    }
}