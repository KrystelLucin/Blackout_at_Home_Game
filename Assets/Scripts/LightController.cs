using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LightController : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject light_object;
    [SerializeField] private TMP_Text interactionText;

    private bool isOn = false;

    private Light lightComponent;  // Referencia al componente de luz.
    private Renderer lightRenderer; // Referencia al renderer para controlar la emisi�n del material.
    private Material lightMaterial; // Material del objeto para cambiar la emisi�n.

    public void Interact()
    {
        isOn = !isOn;  // Cambiar el estado de la luz.

        if (isOn)
        {
            // Encender la luz: Activar el componente Light y cambiar la emisi�n a blanco.
            lightComponent.enabled = true;
            lightMaterial.SetColor("_EmissionColor", Color.white * 1.5f);  // Intensidad aumentada.
        }
        else
        {
            // Apagar la luz: Desactivar el componente Light y cambiar la emisi�n a negro.
            lightComponent.enabled = false;
            lightMaterial.SetColor("_EmissionColor", Color.black);
        }
    }

    public void ShowText(bool flag)
    {
        interactionText.gameObject.SetActive(flag);
    }

    void Start()
    {
        interactionText.gameObject.SetActive(false);

        lightComponent = light_object.GetComponent<Light>();
        lightRenderer = light_object.GetComponent<Renderer>();

        // Verificar si el objeto tiene un material y obtener su propiedad de emisi�n.
        if (lightRenderer != null)
        {
            lightMaterial = lightRenderer.material;
        }

        // Inicializar el estado de la luz en apagado.
        lightComponent.enabled = false;
        lightMaterial.SetColor("_EmissionColor", Color.black);
    }

}
