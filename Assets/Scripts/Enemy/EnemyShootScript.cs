using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootScript : MonoBehaviour
{
    // ѕоколение врага, нужно дл€ увеличени€ веро€тности выстрела
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
            //  аждые 1.5 секунды пытаемс€ выстрельнуть с каждой линии
            yield return new WaitForSeconds(1.5f);

            // ≈сли это не первое поколение
            if (currentGeneration > 0)
            {
                var chanceToShoot = Random.Range(0, 35 - currentGeneration);

                // ¬ зависимости от выпавшего числа, стрел€ет нужный корабль
                // 0 = первый корабль, 5 = шестой корабль
                if (chanceToShoot >= 0 && chanceToShoot <= 5 && transform.GetChild(chanceToShoot).tag == "Enemy")
                {
                    findBullet = false;
                    i = 0;

                    // »щем неактивную пулю
                    // while - тк неизвестно кол-во итераций
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

                    // ≈сли неактивную пулю не нашли
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
        // ≈сли патрон пролетел р€дом, то смотрим, жив ли хот€ бы один корабль текущей строчки
        // ≈сли все убиты - готовим его к следующему спавну
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