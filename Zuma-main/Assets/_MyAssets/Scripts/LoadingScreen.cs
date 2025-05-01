using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoadingScreen : MonoBehaviour
{
    [Header("UI References")]
    public GameObject loadingPanel;
    public Image progressBar;  
    public Text progressText;

    [Header("Scene to Load")]
    public string sceneToLoad = "Menu"; 

    private void Start()
    {
        Debug.Log($"[LoadingScreen] Запуск загрузки сцены '{sceneToLoad}'...");
        StartCoroutine(LoadSceneRoutine());
    }

    private IEnumerator LoadSceneRoutine()
    {
        loadingPanel.SetActive(true);
        Debug.Log($"[LoadingScreen] Начинаем асинхронную загрузку сцены '{sceneToLoad}'...");

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);
        if (asyncLoad == null)
        {
            Debug.LogError($"[LoadingScreen] Ошибка! Сцена '{sceneToLoad}' не найдена. Проверьте Build Settings.");
            yield break;
        }

        asyncLoad.allowSceneActivation = false;

        while (asyncLoad.progress < 0.9f)
        {
            float progressValue = asyncLoad.progress / 0.9f;
            progressBar.fillAmount = progressValue;
            progressText.text = Mathf.RoundToInt(progressValue * 100f) + "%";
            yield return null;
        }

        progressBar.fillAmount = 1f;
        progressText.text = "100%";
        Debug.Log($"[LoadingScreen] Сцена '{sceneToLoad}' загружена на 90%, ждём 0.5 сек...");

        yield return new WaitForSecondsRealtime(0.5f);

        Debug.Log($"[LoadingScreen] Активируем сцену '{sceneToLoad}'...");
        asyncLoad.allowSceneActivation = true;
    }
}