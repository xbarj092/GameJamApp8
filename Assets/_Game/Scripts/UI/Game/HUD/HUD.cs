using UnityEngine;
using UnityEngine.UIElements;

public class HUD : BaseScreen
{
    [SerializeField] private OptionsPopup _optionsPopup;

    [SerializeField] private RectTransform _shopScrollview;


    public void OpenOptions()
    {
        Time.timeScale = 0f;
        _optionsPopup.gameObject.SetActive(true);
    }

    public void CloseOptions()
    {
        _optionsPopup.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
