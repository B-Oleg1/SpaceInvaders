using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPlayerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _shootPosition;

    // A pool of bullets, so as not to create a new bullet every time
    private List<GameObject> BulletPool;

    private void Start()
    {
        BulletPool = new List<GameObject>();

        StartCoroutine(Shoot());
    }

    // Endless shots
    private IEnumerator Shoot()
    {
        bool findBullet = false;
        int i = 0;

        while (true)
        {
            yield return new WaitForSeconds(1.5f);

            findBullet = false;
            i = 0;

            // Looking for an inactive bullet
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

            // If an inactive bullet was not found
            if (!findBullet)
            {
                var bullet = Instantiate(Resources.Load<GameObject>("BulletImage"), transform);
                bullet.transform.position = _shootPosition.transform.position;
                BulletPool.Add(bullet);
            }
        }
    }
}