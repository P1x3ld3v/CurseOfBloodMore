using UnityEngine;

public class Enemy_BossAnimationTrigger : Enemy_AnimationTriggers
{
    private Enemy_Boss enemyBoss;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip attackSFX;
    [SerializeField] private AudioClip spellCastSFX;
    [SerializeField] private AudioClip teleportfx;
    [SerializeField] private float sfxVolume = 1f;

    protected override void Awake()
    {
        base.Awake();
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
        enemyBoss = GetComponentInParent<Enemy_Boss>();
    }

    private void TeleportTrigger()
    {
        enemyBoss.SetTeleportTrigger(true);
    }

    public void PlayAttackSFX()
    {
        if (audioSource != null && attackSFX != null)
            audioSource.PlayOneShot(attackSFX, sfxVolume);
    }

    public void PlaySpellCastSFX()
    {
        if (audioSource != null && spellCastSFX != null)
            audioSource.PlayOneShot(spellCastSFX, sfxVolume);
    }

    public void PlayTeleportSFX()
    {
        if (audioSource != null && spellCastSFX != null)
            audioSource.PlayOneShot(spellCastSFX, sfxVolume);
    }
}
