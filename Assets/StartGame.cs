using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

    public void ChangeScene (int change)
    {
        SceneManager.LoadScene(change);
    }
}
