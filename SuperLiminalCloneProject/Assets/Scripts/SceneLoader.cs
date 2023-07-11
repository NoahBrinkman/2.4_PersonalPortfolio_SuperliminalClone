using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// A small class that allows ou to quit the game whenever and reload the scene in case of game breaking bugs.
/// </summary>
public class SceneLoader : MonoBehaviour
{
    /// <summary>
    /// If you can get the scene 
    /// </summary>
    /// <param name="index"> index of the scene you want to load</param>
    public void LoadScene(int index)
    {
       if(SceneManager.GetSceneByBuildIndex(index) != null) SceneManager.LoadScene(index);
    }
    /// <summary>
    /// Quit the game
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
    /// <summary>
    /// Manipulate the scene/game based on input.
    /// R reloads the scene and escape quits the game.
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
