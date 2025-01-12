using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Door : MonoBehaviour, IInteractable
{

    bool open;
    public Animator anim;
    public TMP_Text interactionText;
    public AudioSource openingDoor;
    public AudioSource closingDoor;


    void Start()
    {
        interactionText.gameObject.SetActive(false);
    }

    public void Interact()
    {
        openClose();
    }

    public void ShowText(bool flag)
    {
        interactionText.gameObject.SetActive(flag);
    }

    public void openClose()
    {
        open = !open;
        if(open)
        {
            openingDoor.Play();
            anim.ResetTrigger("close");
            anim.SetTrigger("open");
        }
        else
        {
            closingDoor.Play();
            anim.ResetTrigger("open");
            anim.SetTrigger("close");
        }
    }

}
