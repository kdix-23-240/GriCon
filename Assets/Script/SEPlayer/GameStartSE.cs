using UnityEngine;

public class GameStartSE : MonoBehaviour
{
    [SerializeField] private AudioClip gameStart;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(gameStart);
    }
}