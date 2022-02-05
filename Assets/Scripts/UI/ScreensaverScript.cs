using System.Collections;
using UnityEngine;

public class ScreensaverScript : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(LoadMainMenu());
    }

    // 3 seconds after entering the application, change the scene
    private IEnumerator LoadMainMenu()
    {
        yield return new WaitForSeconds(3);

        SceneLoaderScript.sceneLoader.gameObject.SetActive(true);
        SceneLoaderScript.sceneLoader.LoadNewScene(1);
    }
}