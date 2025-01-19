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
    public GameObject wakeUpCamera;
    public GameObject gameCamera;

    [Header("Player")]
    public GameObject firstPersonPlayer;

    [Header("Phone")]
    public GameObject phone;

    private PhoneController phoneController;
    private WakeUpAnimation wakeUpAnimation;

    // Start is called before the first frame update
    void Start()
    {
        ShowMenu();

        phoneController = phone.GetComponent<PhoneController>();

        wakeUpAnimation = wakeUpCamera.GetComponent<WakeUpAnimation>();
        wakeUpAnimation.onAnimationEnd = OnWakeUpAnimationEnd;
    }

    public void ShowMenu()
    {
        menuCanvas.SetActive(true);
        gameCanvas.SetActive(false);

        menuCamera.SetActive(true);
        wakeUpCamera.SetActive(false);
        gameCamera.SetActive(false);

        firstPersonPlayer.SetActive(false);
    }

    public void StartGame()
    {
        menuCanvas.SetActive(false);
        gameCanvas.SetActive(true);

        menuCamera.SetActive(false);
        wakeUpCamera.SetActive(true); // Activar la c�mara de despertar
        gameCamera.SetActive(false);

        firstPersonPlayer.SetActive(false);

        InitialAnimation();
    }

    private void InitialAnimation()
    {
        phoneController.StartRinging();
        wakeUpAnimation.PlayAnimation();
    }

    private void OnWakeUpAnimationEnd()
    {
        // Transici�n a la c�mara de juego y activaci�n del jugador
        wakeUpCamera.SetActive(false);
        gameCamera.SetActive(true);
        firstPersonPlayer.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit(); // Salir del juego
    }
}
