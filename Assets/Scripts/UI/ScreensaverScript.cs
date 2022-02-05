using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreensaverScript : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(LoadMainMenu());
    }

    // Через 3 секунды после входа в приложение меняем сцену
    private IEnumerator LoadMainMenu()
    {
        yield return new WaitForSeconds(3);

        SceneLoaderScript.sceneLoader.gameObject.SetActive(true);
        SceneLoaderScript.sceneLoader.LoadNewScene(1);
    }
}
