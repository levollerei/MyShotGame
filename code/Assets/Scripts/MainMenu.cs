using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start the game
    public void StartGame()
    {
        SceneManager.LoadScene("LoadSceneToChoice");
    }

    // Quit the game
    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        // Stop the play mode in the editor
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
