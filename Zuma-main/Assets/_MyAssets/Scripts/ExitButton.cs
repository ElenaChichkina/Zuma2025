using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public void OnExitButtonClicked()
    {

        Application.Quit();
 
        Debug.Log("Exit");
    }
}