using UnityEngine;

public class TutorialScreen : BaseScreen
{
    private void Start()
    {
        Time.timeScale = 0f;
    }

    public void CloseTutorialScreen()
    {
        LocalDataStorage.Instance.PlayerPrefs.SaveTutorial(true);
        Destroy(gameObject);
        Time.timeScale = 1;

        GameManager.Instance.CanPlay = true;
        FindFirstObjectByType<HUD>().SetButtonInteractable(true);
    }
}
