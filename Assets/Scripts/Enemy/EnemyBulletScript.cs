using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    private void Update()
    {
        // ���� ���� ������� �� �����
        if (transform.position.y < -50)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // ���� ���� ����������� � �������
        if (collider.gameObject.tag == "Player")
        {
            // ���������� �����
            ControllGameScript.controllGameScript.HitPlayer();

            gameObject.SetActive(false);
        }
    }
}