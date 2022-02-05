using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootScript : MonoBehaviour
{
    // Generation of the enemy, it is necessary to increase the probability of a shot
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
            // Every 1.5 seconds we try to shoot from each line
            yield return new WaitForSeconds(1.5f);

            var chanceToShoot = Random.Range(0, 35 - currentGeneration);

            // Depending on the number that falls, the right ship shoots
            // 0 = first ship, 5 = sixth ship
            if (chanceToShoot >= 0 && chanceToShoot <= 5 && transform.GetChild(chanceToShoot).tag == "Enemy")
            {
                findBullet = false;
                i = 0;

                // Looking for an inactive bullet
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

                // If an inactive bullet was not found
                if (!findBullet)
                {
                    var bullet = Instantiate(Resources.Load<GameObject>("EnemyBulletImage"), transform.GetChild(chanceToShoot));
                    bullet.transform.position = transform.GetChild(chanceToShoot).position;
                    BulletPool.Add(bullet);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // If the cartridge flew nearby, then we look to see if at least one ship of the current line is alive
        // If everyone is killed, we prepare him for the next spawn
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