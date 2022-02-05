using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    private List<GameObject> EnemyPool;

    private readonly int _timeToSpawn = 7;

    private int _currentGeneration = 1;
    private float _rowHeight = 0;

    private void Start()
    {
        EnemyPool = new List<GameObject>();

        // Throwing the elements already added to the stage into the pool
        for (int i = 0; i < transform.childCount; i++)
        {
            EnemyPool.Add(transform.GetChild(i).gameObject);
        }

        // The height to which it will be necessary to shift opponents down
        _rowHeight = transform.GetChild(0).GetComponent<RectTransform>().rect.height;

        StartCoroutine(SpawnNewEnemy());
    }

    // Endlessly creating opponents
    private IEnumerator SpawnNewEnemy()
    {
        while (true)
        {
            // Movement to the side before the spawn of a new row

            yield return new WaitForSeconds(_timeToSpawn / 2);

            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<Animator>().SetBool("MoveBlockEnemy", true);
            }

            yield return new WaitForSeconds(_timeToSpawn / 2);

            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<Animator>().SetBool("MoveBlockEnemy", false);
            }

            yield return new WaitForSeconds(1);

            MakeObjectBelow();

            SpawnEnemy();

            yield return new WaitForSeconds(_timeToSpawn);
        }
    }

    // Lower the object below
    private void MakeObjectBelow()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).localPosition = new Vector2(transform.GetChild(i).localPosition.x,
                                                              transform.GetChild(i).localPosition.y - _rowHeight);
        }
    }

    // Spawn a new line
    private void SpawnEnemy()
    {
        bool findEnemy = false;
        int i = 0;

        // Looking for an inactive bullet
        while (!findEnemy && i < EnemyPool.Count)
        {
            if (!EnemyPool[i].activeInHierarchy)
            {
                EnemyPool[i].transform.localPosition = new Vector2(0, transform.parent.GetComponent<RectTransform>().rect.height / 2);
                EnemyPool[i].GetComponent<EnemyShootScript>().currentGeneration = _currentGeneration;

                for (int a = 0; a < 6; a++)
                {
                    EnemyPool[i].transform.GetChild(a).tag = "Enemy";

                    EnemyPool[i].transform.GetChild(a).GetComponent<BoxCollider2D>().isTrigger = true;

                    var alpha = EnemyPool[i].transform.GetChild(a).GetComponent<Image>().color;
                    alpha.a = 1;
                    EnemyPool[i].transform.GetChild(a).GetComponent<Image>().color = alpha;
                }

                EnemyPool[i].SetActive(true);

                findEnemy = true;
            }

            i++;
        }

        // If an inactive bullet was not found
        if (!findEnemy)
        {
            var enemy = Instantiate(Resources.Load<GameObject>("RowEnemiesObject"), transform);
            enemy.transform.localPosition = new Vector2(0, transform.parent.GetComponent<RectTransform>().rect.height / 2);
            enemy.GetComponent<EnemyShootScript>().currentGeneration = _currentGeneration;
            EnemyPool.Add(enemy);
        }

        _currentGeneration++;
    }
}