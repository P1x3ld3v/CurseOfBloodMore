using UnityEngine;

public class NPCAnimationSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip anvilHit;
    [SerializeField] private AudioClip playTalk;

    private void Awake()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    public void PlayAnvilHit()
    {
        if (anvilHit == null)
        audioSource.pitch = Random.Range(0.95f, 1.05f);
        audioSource.PlayOneShot(anvilHit);
    }

    public void PlayTalk()
    {
        if (playTalk != null)
        {
            audioSource.PlayOneShot(anvilHit);
        }
    }
}