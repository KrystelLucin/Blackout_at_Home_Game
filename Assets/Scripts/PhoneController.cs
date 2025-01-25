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
    public AudioSource ambienceSound;
    public AudioSource menuMusic;
    
    
    // Start is called before the first frame update
    void Start()
    {
        menuMusic.Play();
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
            Debug.Log("Tel�fono silenciado por el jugador.");
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
            menuMusic.Stop();
            ringingAudio.enabled = true;
            ringingAudio.Play();
            screenMaterial.EnableKeyword("_EMISSION");
            isRinging = true;
            ambienceSound.Play();
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
