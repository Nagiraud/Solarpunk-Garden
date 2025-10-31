using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("Reouverture");
        SceneManager.LoadScene("Garden");
    }

    public void QuitGame()
    {
        Debug.Log("fermeture");
        Application.Quit();
    }

    public void ContinueGame()
    {
        Debug.Log("Continue");
        GameManager.Instance.IsPaused = false;
        Time.timeScale = 1f;
        GameManager.Instance.pauseMenuUI.gameObject.SetActive(false);
        AudioListener.pause = false;
        
    }
}
