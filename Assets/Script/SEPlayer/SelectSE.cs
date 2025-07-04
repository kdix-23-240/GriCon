using UnityEngine;

public class SelectSE : MonoBehaviour
{
    [SerializeField] private AudioClip selectSE;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySelectSE()
    {
        if (audioSource != null && selectSE != null)
        {
            audioSource.PlayOneShot(selectSE);
        }
        else
        {
            Debug.LogWarning("AudioSource or selectSE is not set.");
        }
    }
}