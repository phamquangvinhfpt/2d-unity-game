using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    public void Quit()
    {
        //when click button quit then return to the start menu
        SceneManager.LoadScene("_Menu");
    }
}
