using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreensaverScript : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(LoadMainMenu());
    }

    // ����� 3 ������� ����� ����� � ���������� ������ �����
    private IEnumerator LoadMainMenu()
    {
        yield return new WaitForSeconds(3);

        SceneLoaderScript.sceneLoader.gameObject.SetActive(true);
        SceneLoaderScript.sceneLoader.LoadNewScene(1);
    }
}
