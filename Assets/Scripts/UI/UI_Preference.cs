using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Preference : MonoBehaviour
{
    public Ship ship;
    public GameObject ui_Preference;


    bool isOpen = false;

    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            toggle();
        }
        
    }

    public void toggle()
    {
        if (isOpen) 
        {
            Close();
            
        }
        else
        {
            Open();
            
        }
    }

    public void Open()
    {

        if(!ship.isGameOver && ship.isGameStart) 
        {
            isOpen = true;
            Time.timeScale = 0f;
            ui_Preference.SetActive(true);
        }

    }

    public void Close()
    {
        Time.timeScale = 1f;
        ui_Preference.SetActive(false);
        isOpen = false;
    }

    public void BTN_Title()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Environment_Free");
    }

    public void BTN_Restart()
    {
        ship.GameStart();
    }


}
