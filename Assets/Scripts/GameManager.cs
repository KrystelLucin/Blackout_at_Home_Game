using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Canvas")]
    public GameObject menuCanvas;
    public GameObject gameCanvas;

    [Header("Cameras")]
    public GameObject menuCamera;
    public GameObject gameCamera;

    [Header("Player")]
    public GameObject firstPersonPlayer;

    // Start is called before the first frame update
    void Start()
    {
        ShowMenu();
    }

    public void ShowMenu()
    {
        menuCanvas.SetActive(true);
        gameCanvas.SetActive(false);

        menuCamera.SetActive(true);
        gameCamera.SetActive(false);

        firstPersonPlayer.SetActive(false);
    }

    public void StartGame()
    {
        menuCanvas.SetActive(false);
        gameCanvas.SetActive(true);

        menuCamera.SetActive(false);
        gameCamera.SetActive(true);

        firstPersonPlayer.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit(); // Salir del juego
    }
}
