using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{

    [Header("Skybox Rotation Settings")]
    public string currentSkybox = "day";
    public float rotationSpeed = 1f; // Velocidad de rotación del skybox
    public float transitionDuration = 2f; // Duración de la transición en segundos

    [Header("Skybox Materials")]
    public Material daySkybox;       // Skybox para el día
    public Material eveningSkybox;   // Skybox para el atardecer
    public Material nightSkybox;     // Skybox para la noche
    public Material dawnSkybox;      // Skybox para el amanecer

    [Header("Fog Settings")]
    public GameObject House;

    private Color dayAmbientColor = Color.white; // Blanco para el día
    private Color otherAmbientColor = new Color(0.039f, 0.039f, 0.039f); // Gris oscuro (#0a0a0a)

    private Coroutine transitionCoroutine; // Para controlar la transición activa


    // Start is called before the first frame update
    void Start()
    {
        UpdateSkybox(currentSkybox, true);
        ApplyFog();
    }

    // Update is called once per frame
    void Update()
    {
        RotateSkybox();
    }
    // Método para rotar el Skybox
    private void RotateSkybox()
    {
        if (RenderSettings.skybox != null)
        {
            RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotationSpeed);
        }
    }

    public void UpdateSkybox(string skyboxName, bool instant = false)
    {
        Material targetSkybox = null;
        Color targetAmbientColor = Color.black;

        switch (skyboxName.ToLower())
        {
            case "day":
                targetSkybox = daySkybox;
                targetAmbientColor = dayAmbientColor;
                break;
            case "evening":
                targetSkybox = eveningSkybox;
                targetAmbientColor = otherAmbientColor;
                break;
            case "night":
                targetSkybox = nightSkybox;
                targetAmbientColor = otherAmbientColor;
                break;
            case "dawn":
                targetSkybox = dawnSkybox;
                targetAmbientColor = otherAmbientColor;
                break;
            default:
                Debug.LogWarning("Skybox no reconocido: " + skyboxName);
                return;
        }

        // Si hay una transición activa, detenerla
        if (transitionCoroutine != null)
        {
            StopCoroutine(transitionCoroutine);
        }

        // Iniciar la transición o aplicar cambios instantáneamente
        if (instant)
        {
            ApplySkyboxAndLighting(targetSkybox, targetAmbientColor);
        }
        else
        {
            transitionCoroutine = StartCoroutine(TransitionSkyboxAndLighting(targetSkybox, targetAmbientColor));
        }
    }

    // Método para aplicar cambios instantáneamente
    private void ApplySkyboxAndLighting(Material skybox, Color ambientColor)
    {
        RenderSettings.skybox = skybox;
        RenderSettings.ambientLight = ambientColor;
        DynamicGI.UpdateEnvironment();
    }

    private IEnumerator TransitionSkyboxAndLighting(Material targetSkybox, Color targetAmbientColor)
    {
        Material initialSkybox = RenderSettings.skybox;
        Color initialAmbientColor = RenderSettings.ambientLight;

        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / transitionDuration;

            // Interpolar el color ambiental
            RenderSettings.ambientLight = Color.Lerp(initialAmbientColor, targetAmbientColor, t);

            // Interpolar el material del Skybox si es posible
            if (initialSkybox != null && targetSkybox != null)
            {
                for (int i = 0; i < targetSkybox.shader.GetPropertyCount(); i++)
                {
                    string propertyName = targetSkybox.shader.GetPropertyName(i);
                    if (targetSkybox.HasProperty(propertyName) && initialSkybox.HasProperty(propertyName))
                    {
                        if (targetSkybox.GetColor(propertyName) != null)
                        {
                            initialSkybox.SetColor(propertyName,
                                Color.Lerp(initialSkybox.GetColor(propertyName),
                                targetSkybox.GetColor(propertyName), t));
                        }
                    }
                }
            }

            // Actualizar la iluminación global
            DynamicGI.UpdateEnvironment();

            yield return null;
        }

        // Asegurarse de que se aplican los valores finales
        RenderSettings.skybox = targetSkybox;
        RenderSettings.ambientLight = targetAmbientColor;
        DynamicGI.UpdateEnvironment();
    }

    private void ApplyFog()
    {

    }

}
