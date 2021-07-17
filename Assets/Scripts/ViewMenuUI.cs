using UnityEngine;

public class ViewMenuUI : MonoBehaviour
{
    public void OnButtonPlay()
    {
        SceneManager.Instance.Play();
    }

    public void OnAudioOnOff()
    {
        SceneManager.Instance.Audio();
    }
}
