using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string myScene = "";

    public void LoadLevel()
    {
        SceneManager.LoadScene(myScene);
    }
}
