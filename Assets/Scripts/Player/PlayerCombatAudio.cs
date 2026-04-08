using UnityEngine;

public class PlayerCombatAudio : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    [Header("Sword Sounds")]
    [SerializeField] private AudioClip swordSwing;
    [SerializeField] private AudioClip swordDamage;

    [Header("Movement")]
    [SerializeField] private AudioClip DashClip;
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip landClip;

    [Header("Player")]
    [SerializeField] private AudioClip HurtClip;

    public void Awake()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }
    public void PlaySwing()
    {
        if (swordSwing != null)
        {
            audioSource.PlayOneShot(swordSwing);
        }
    }
    public void PlayDamage()
    {
        if (swordDamage != null)
        {
            audioSource.PlayOneShot(swordDamage);
        }
    }

    public void PlayDash()
    {
        if (DashClip != null)
        {
            audioSource.PlayOneShot(DashClip);
        }
    }

    public void PlayHurt()
    {
        if (HurtClip != null)
        {
            audioSource.PlayOneShot(HurtClip);
        }
    }

    public void PlayJump()
    {
        if (HurtClip != null)
        {
            audioSource.PlayOneShot(jumpClip);
        }
    }

    public void PlayLand()
    {
        if (HurtClip != null)
        {
            audioSource.PlayOneShot(landClip);
        }
    }

}
