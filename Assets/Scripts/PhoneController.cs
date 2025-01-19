using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PhoneController : MonoBehaviour, IInteractable
{
    public TMP_Text interactionText;
    public AudioSource audioSource;
    public Material screenMaterial;
    private bool isRinging = false;  // Indica si el teléfono está sonando

    // Start is called before the first frame update
    void Start()
    {
        interactionText.gameObject.SetActive(false);

        audioSource.enabled = false;
        audioSource.loop = true;

        screenMaterial.DisableKeyword("_EMISSION");
    }

    public void Interact()
    {
        if (isRinging)
        {
            StopRinging();
            Debug.Log("Teléfono silenciado por el jugador.");
        }
    }

    public void ShowText(bool flag)
    {
        interactionText.gameObject.SetActive(flag);
    }

    public void StartRinging()
    {
        if (!isRinging && audioSource != null)
        {
            audioSource.enabled = true;
            audioSource.Play();
            screenMaterial.EnableKeyword("_EMISSION");
            isRinging = true;
        }
    }
    public void StopRinging()
    {
        if (isRinging && audioSource != null)
        {
            audioSource.Stop();
            screenMaterial.DisableKeyword("_EMISSION");
            isRinging = false;
        }
    }
}
