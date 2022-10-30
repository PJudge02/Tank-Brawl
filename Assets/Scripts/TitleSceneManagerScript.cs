using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    public void onSmallMapClick()
    {
        SceneManager.LoadScene("SmallMapGameScene");
    }
    public void onMediunMapClick()
    {
        SceneManager.LoadScene("MediumMapGameScene");
    }
    public void onLargeMapClick()
    {
        SceneManager.LoadScene("LargeMapGameScene");
    }
    public void backToStartClick()
    {
        SceneManager.LoadScene("StartScreen");
    }
    public void howToPlayClick()
    {
        SceneManager.LoadScene("HowToPlayScene");
    }
}
