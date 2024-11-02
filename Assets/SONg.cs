using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SONg : MonoBehaviour
{
    public AudioClip SceneAudio;
    public AudioSource AudioSource;

    void Start()
    {
        AudioSource audio = GetComponent<AudioSource>();

        // Asigna el clip de audio
        audio.clip = SceneAudio;

        // Activa la repetición en bucle
        audio.loop = true;

        // Reproduce el audio
        audio.Play();
    }
}
