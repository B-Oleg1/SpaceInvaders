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

    // Загрузка определенной сцены
    private IEnumerator LoadingNewScene(int sceneId)
    {
        gameObject.SetActive(true);

        var slider = transform.GetChild(1).GetChild(0).GetComponent<Slider>();

        // С помощью ширины слайдера узнаем, где должен остановиться указатель загрузки
        var sliderWidth = slider.GetComponent<RectTransform>().rect.width;
        var pointer = transform.GetChild(1).GetChild(1).GetComponent<RectTransform>();

        // Показываем процент загрузки, пока сцена грузиться асинхронно
        var load = SceneManager.LoadSceneAsync(sceneId);
        while (!load.isDone)
        {
            // Обновляем процент загрузки новой сцены
            var progress = load.progress / .9f;
            slider.value = progress;

            // Двигаем указатель
            pointer.localPosition = new Vector2(progress * sliderWidth, 0);

            yield return null;
        }
    }
}