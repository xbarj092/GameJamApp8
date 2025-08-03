using UnityEngine;

public class TutorialScreen : BaseScreen
{
    public void CloseTutorialScreen()
    {
        LocalDataStorage.Instance.PlayerPrefs.SaveTutorial(true);
        Destroy(gameObject);
    }
}
