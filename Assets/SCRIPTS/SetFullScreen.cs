using UnityEngine;
using UnityEngine.UI;

public class FullscreenController : MonoBehaviour
{
    private Toggle Toggle;

    void Start()
    {
        Toggle = GetComponent<Toggle>();

        if (Toggle != null)
        {
            Toggle.isOn = Screen.fullScreen;
            Toggle.onValueChanged.AddListener(SetFullscreen);
        }
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}