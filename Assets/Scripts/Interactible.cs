using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// The Interactible class flags a Game Object as being "Interactible".
/// Determines what happens when an Interactible is being gazed at.
/// </summary>
public class Interactible : MonoBehaviour
{
    [Tooltip("Audio clip to play when interacting with this hologram.")]
    public AudioClip TargetFeedbackSound;
    private AudioSource audioSource;

    private Material[] defaultMaterials;
    

    private Text message;

    private Color highlightColor;

    private GUIStyle currentStyle = null;

    void Start()
    {
        defaultMaterials = GetComponent<Renderer>().materials;

        // Add a BoxCollider if the interactible does not contain one.
        Collider collider = GetComponentInChildren<Collider>();
        if (collider == null)
        {
            gameObject.AddComponent<BoxCollider>();
        }

        EnableAudioHapticFeedback();

        message = GameObject.Find("Text").GetComponent<Text>();
        //objectName = this.gameObject.name;

       // Debug.log("Starting Interactable");
    }

    private void EnableAudioHapticFeedback()
    {
        // If this hologram has an audio clip, add an AudioSource with this clip.
        if (TargetFeedbackSound != null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }

            audioSource.clip = TargetFeedbackSound;
            audioSource.playOnAwake = false;
            audioSource.spatialBlend = 1;
            audioSource.dopplerLevel = 0;
        }
    }

    private void choosePart(Color color, string messageString) {

        for (int i = 0; i < defaultMaterials.Length; i++)
        {
            defaultMaterials[i].SetColor("_Color", color);
        }

        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }

        message.text = messageString;
    }

    void GazeEntered()
    {
        switch (gameObject.name)
        {
            case "MainHead":
                highlightColor = Color.white;
                message.text = "This is an insect head";
                choosePart(highlightColor, message.text);
                break;

            case "Labrum":
                highlightColor = Color.green;
                message.text = "Lorem ipsum sit dolor amet lorem ipsum sit dolor amet";
                choosePart(highlightColor, message.text);
                break;

            case "LMandible":
                highlightColor = Color.cyan;
                message.text = "Lorem ipsum sit dolor amet lorem ipsum sit dolor amet";
                choosePart(highlightColor, message.text);
                break;

            case "RMandible":
                highlightColor = Color.cyan;
                message.text = "Lorem ipsum sit dolor amet lorem ipsum sit dolor amet";
                choosePart(highlightColor, message.text);
                break;

            case "LMaxilla":
                highlightColor = Color.magenta;
                message.text = "Lorem ipsum sit dolor amet lorem ipsum sit dolor amet";
                choosePart(highlightColor, message.text);
                break;

            case "RMaxilla":
                highlightColor = Color.magenta;
                message.text = "Lorem ipsum sit dolor amet lorem ipsum sit dolor amet";
                choosePart(highlightColor, message.text);
                break;

            case "Labium":
                highlightColor = Color.yellow;
                message.text = "Lorem ipsum sit dolor amet lorem ipsum sit dolor amet";
                choosePart(highlightColor, message.text);
                break;
        }
    }

    void GazeExited()
    {
        for (int i = 0; i < defaultMaterials.Length; i++)
        {
            // 2.d: Uncomment the below line to remove highlight on material when gaze exits.
            defaultMaterials[i].SetColor("_Color", Color.white);
        }

        message.text = "Nothing Selected";
    }

    void OnSelect()
    {
        for (int i = 0; i < defaultMaterials.Length; i++)
        {
            defaultMaterials[i].SetFloat("_Highlight", .5f);
        }

        // Play the audioSource feedback when we gaze and select a hologram.
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }

        Material m = transform.parent.GetComponentInChildren<Renderer>().material;
        m = this.GetComponent<Renderer>().material;
        m.color = Color.green;



    }
}