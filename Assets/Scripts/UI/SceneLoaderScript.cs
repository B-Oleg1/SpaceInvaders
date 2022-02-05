using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoaderScript : MonoBehaviour
{
    public static SceneLoaderScript sceneLoader;

    private void Start()
    {
        sceneLoader = this;

        gameObject.SetActive(false);
    }

    public void LoadNewScene(int sceneId)
    {
        StartCoroutine(LoadingNewScene(sceneId));
    }

    // Loading a specific scene
    private IEnumerator LoadingNewScene(int sceneId)
    {
        gameObject.SetActive(true);

        var slider = transform.GetChild(1).GetChild(0).GetComponent<Slider>();

        // Using the slider width, we find out where the loading pointer should stop
        var sliderWidth = slider.GetComponent<RectTransform>().rect.width;
        var pointer = transform.GetChild(1).GetChild(1).GetComponent<RectTransform>();

        // Showing the loading percentage while the scene loads asynchronously
        var load = SceneManager.LoadSceneAsync(sceneId);
        while (!load.isDone)
        {
            // Updating the loading percentage of a new scene
            var progress = load.progress / .9f;
            slider.value = progress;

            // Moving the pointer
            pointer.localPosition = new Vector2(progress * sliderWidth, 0);

            yield return null;
        }
    }
}