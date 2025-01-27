using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairController : MonoBehaviour
{
    public Transform sittingPosition; // Posición donde el jugador se sentará

    public void SitDown(GameObject player)
    {
        player.transform.position = sittingPosition.position;
        Debug.Log("El jugador se ha sentado en la silla.");
    }
}

