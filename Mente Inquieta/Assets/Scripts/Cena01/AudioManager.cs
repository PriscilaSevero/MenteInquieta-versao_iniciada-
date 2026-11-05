using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    private AudioSource audioSource;

    void Awake()
    {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else { Destroy(gameObject); }
        
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
    }

    public void TrocarMusica(AudioClip novaMusica)
    {
        if (novaMusica == null) return;
        if (audioSource.clip == novaMusica) return; // Já está tocando

        audioSource.Stop();
        audioSource.clip = novaMusica;
        audioSource.Play();
    }
}