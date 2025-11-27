using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    public int test; 

    void Awake()
    {
        gm = this;

        GameObject [] objs = GameObject.FindGameObjectsWithTag("Game Manager");

        if(objs.Length > 1) Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) LoadNextScene();
    }

    public void LoadNextScene()
    {
        if(Input.GetKey(KeyCode.R)) test = 6;
        SceneManager.LoadScene("Scene2");
    }
}
