using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Button newGameButton;
    public Button continueButton;
    public string gameSceneName = "SampleScene";

    void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        if (continueButton)
            continueButton.interactable = PersistenceManager.SaveExists();
    }

    public void OnNewGame()
    {
        PersistenceManager.Delete();  
        SceneManager.LoadScene(gameSceneName);
    }

    public void OnContinue()
    {
        SceneManager.LoadScene(gameSceneName);
    }
}
