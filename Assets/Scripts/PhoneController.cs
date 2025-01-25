using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PhoneController : MonoBehaviour, IInteractable
{
    public TMP_Text interactionText;
    public AudioSource ringingAudio;
    public AudioSource momCallAudio;
    public Material screenMaterial;
    private bool isRinging = false;
    
    // Start is called before the first frame update
    void Start()
    {
        interactionText.gameObject.SetActive(false);

        ringingAudio.enabled = false;
        ringingAudio.loop = true;


        momCallAudio.enabled = false;

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
        if (!isRinging && ringingAudio != null)
        {
            ringingAudio.enabled = true;
            ringingAudio.Play();
            screenMaterial.EnableKeyword("_EMISSION");
            isRinging = true;
        }
    }
    public void StopRinging()
    {
        if (isRinging && ringingAudio != null)
        {
            ringingAudio.Stop();
            screenMaterial.DisableKeyword("_EMISSION");
            isRinging = false;

            momCallAudio.enabled = true;
            momCallAudio.Play();
        }
    }
}
