using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class playerManager : MonoBehaviour
{
    public static bool isGameOver;
    public static bool isGameWin;
    // Start is called before the first frame update
    private void Awake()
    {
        isGameOver = false;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {
            SceneManager.LoadSceneAsync(3);
        }

        if (isGameWin) 
        {
            SceneManager.LoadSceneAsync(2);    
        }
    }

    public void ReplayGame()
    {
        SceneManager.LoadScene("one");
    }
}

