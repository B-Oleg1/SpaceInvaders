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
        // Если пуля улетела за экран
        if (transform.localPosition.y > Screen.height + 50)
        {
            gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        // Постоянное движение вверх с одинаковой скоростью
        _bulletRigidbody2D.velocity = new Vector2(0, _bulletSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Если пуля столкнулась с противником
        if (collider.gameObject.tag == "Enemy")
        {
            collider.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;

            collider.gameObject.tag = "EnemyKilled";

            var alpha = collider.gameObject.GetComponent<Image>().color;
            alpha.a = 0;
            collider.gameObject.GetComponent<Image>().color = alpha;

            // Начисление очков
            ControllGameScript.controllGameScript.KillEnemy(transform);

            gameObject.SetActive(false);
        }
    }
}