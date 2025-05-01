using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Prelouder : MonoBehaviour
{
    [Header("Scene Loading")]
    public string nextSceneName = "MainScene";
    
    [Header("Animation Settings")]
    public Animator animator; 
    public string animationTrigger = "SplashOut";

    private AsyncOperation operation;
    private bool isAnimationDone = false;

    private IEnumerator Start()
    {
        Debug.Log("Начало загрузки сцены: " + nextSceneName);
        operation = SceneManager.LoadSceneAsync(nextSceneName);
        operation.allowSceneActivation = false;

        while (operation.progress < 0.9f)
        {
            yield return null;
        }

        Debug.Log("Сцена загружена на 90%, запускаем анимацию...");

        animator.SetTrigger(animationTrigger);

        while (!isAnimationDone)
        {
            yield return null;
        }

        Debug.Log("Анимация завершена, переходим в сцену: " + nextSceneName);

        operation.allowSceneActivation = true;
    }

    public void OnSplashAnimationDone()
    {
        Debug.Log("Animation Event: OnSplashAnimationDone() вызван!");
        isAnimationDone = true;
    }
}