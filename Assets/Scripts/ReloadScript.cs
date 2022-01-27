using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScript : MonoBehaviour
{
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
