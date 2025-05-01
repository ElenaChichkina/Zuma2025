using UnityEngine;

public class Link : MonoBehaviour
{
    public void linkIn(string LinkId)
    {
        Application.OpenURL(LinkId);
    }
}
