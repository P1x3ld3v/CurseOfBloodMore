using UnityEngine;

public class BossMusicTrigger : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip bossMusic;

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggered) return;

        if (collision.CompareTag("Player"))
        {
            triggered = true;

            if (musicSource != null && bossMusic != null)
            {
                musicSource.clip = bossMusic;
                musicSource.Play();
            }
        }
    }
}