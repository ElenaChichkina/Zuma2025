using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    [Header("Ссылки на кнопки уровней и иконки замков")]
    public Button[] levelButtons;
    public GameObject[] lockIcons;

    private void Start()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 5);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            int sceneBuildIndex = 5 + i;

            if (unlockedLevel >= sceneBuildIndex)
            {
                levelButtons[i].interactable = true;
                lockIcons[i].SetActive(false);
            }
            else
            {
                levelButtons[i].interactable = false;
                lockIcons[i].SetActive(true);
            }
        }
    }

    public void LoadLevel(int buildIndex)
    {
        Debug.Log($"[LevelMenu] Загружаем уровень: buildIndex={buildIndex}");
        SceneManager.LoadSceneAsync(buildIndex);
    }

    public void ResetProgress()
    {
        PlayerPrefs.SetInt("UnlockedLevel", 5); 
        PlayerPrefs.Save();
        Debug.Log("[LevelMenu] Прогресс сброшен!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}