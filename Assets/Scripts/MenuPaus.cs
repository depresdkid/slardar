using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPaus : MonoBehaviour
{
    private static bool gameIsPaus = false;

    public GameObject pausMenuUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            print("Пауза");
            if (gameIsPaus)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        pausMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaus = true;
    }

    public void Resume()
    {
        pausMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaus = false;
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        gameIsPaus = false;
        Time.timeScale = 1f;
    }
    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
