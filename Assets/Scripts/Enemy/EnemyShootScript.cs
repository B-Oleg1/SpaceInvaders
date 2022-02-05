using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootScript : MonoBehaviour
{
    // ��������� �����, ����� ��� ���������� ����������� ��������
    public int currentGeneration = 0;

    private List<GameObject> BulletPool;

    private void Start()
    {
        BulletPool = new List<GameObject>();

        StartCoroutine(EnemyShoot());
    }

    private IEnumerator EnemyShoot()
    {
        bool findBullet = false;
        int i = 0;

        while (true)
        {
            // ������ 1.5 ������� �������� ������������ � ������ �����
            yield return new WaitForSeconds(1.5f);

            // ���� ��� �� ������ ���������
            if (currentGeneration > 0)
            {
                var chanceToShoot = Random.Range(0, 35 - currentGeneration);

                // � ����������� �� ��������� �����, �������� ������ �������
                // 0 = ������ �������, 5 = ������ �������
                if (chanceToShoot >= 0 && chanceToShoot <= 5 && transform.GetChild(chanceToShoot).tag == "Enemy")
                {
                    findBullet = false;
                    i = 0;

                    // ���� ���������� ����
                    // while - �� ���������� ���-�� ��������
                    while (!findBullet && i < BulletPool.Count)
                    {
                        if (!BulletPool[i].activeInHierarchy)
                        {
                            BulletPool[i].transform.position = transform.GetChild(chanceToShoot).position;
                            BulletPool[i].SetActive(true);

                            findBullet = true;
                        }

                        i++;
                    }

                    // ���� ���������� ���� �� �����
                    if (!findBullet)
                    {
                        var bullet = Instantiate(Resources.Load<GameObject>("EnemyBulletImage"), transform.GetChild(chanceToShoot));
                        bullet.transform.position = transform.GetChild(chanceToShoot).position;
                        BulletPool.Add(bullet);
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // ���� ������ �������� �����, �� �������, ��� �� ���� �� ���� ������� ������� �������
        // ���� ��� ����� - ������� ��� � ���������� ������
        if (collider.CompareTag("Bullet"))
        {
            bool _hasAlive = false;
            for (int i = 0; i < 6; i++)
            {
                if (transform.GetChild(i).gameObject.tag == "Enemy")
                {
                    _hasAlive = true;
                }
            }

            if (!_hasAlive)
            {
                gameObject.SetActive(false);
            }
        }

        if (collider.gameObject.tag == "PlayerField" && transform.gameObject.activeInHierarchy)
        {
            ControllGameScript.controllGameScript.EndGame();

            gameObject.SetActive(false);
        }
    }
}