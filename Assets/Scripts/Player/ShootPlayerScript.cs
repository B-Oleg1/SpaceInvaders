using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPlayerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _shootPosition;

    // Пул пуль, чтобы не создавать каждый раз новую пулю
    private List<GameObject> BulletPool;

    private void Start()
    {
        BulletPool = new List<GameObject>();

        StartCoroutine(Shoot());
    }

    // Бесконечные выстрелы
    private IEnumerator Shoot()
    {
        bool findBullet = false;
        int i = 0;

        while (true)
        {
            yield return new WaitForSeconds(1.5f);

            findBullet = false;
            i = 0;

            // Ищем неактивную пулю
            // while - тк неизвестно кол-во итераций
            while (!findBullet && i < BulletPool.Count)
            {
                if (!BulletPool[i].activeInHierarchy)
                {
                    BulletPool[i].transform.position = _shootPosition.transform.position;
                    BulletPool[i].SetActive(true);

                    findBullet = true;
                }

                i++;
            }

            // Если неактивную пулю не нашли
            if (!findBullet)
            {
                var bullet = Instantiate(Resources.Load<GameObject>("BulletImage"), transform);
                bullet.transform.position = _shootPosition.transform.position;
                BulletPool.Add(bullet);
            }
        }
    }
}