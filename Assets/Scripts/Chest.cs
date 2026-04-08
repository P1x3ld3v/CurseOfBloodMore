using UnityEngine;

public class Object_Chest : MonoBehaviour, IDamgable
{
    private Rigidbody2D rb => GetComponentInChildren<Rigidbody2D>();
    private Animator anim => GetComponentInChildren<Animator>();
    private Entity_VFX fx => GetComponent<Entity_VFX>();

    [Header("Open Details")]
    [SerializeField] private Vector2 knockback;

    [Header("Drop")]
    [SerializeField] private GameObject keyDropPrefab;
    [SerializeField] private Transform dropPoint;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip openSFX;

    private bool isOpened = false;

    private void Awake()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    public bool TakeDamage(float damage, float elementalDamage, ElementType element, Transform damageDealer)
    {
        if (isOpened) return false;
        isOpened = true;

        Debug.Log("Chest got hit");

        fx.PlayOnDamageVfx();
        anim.SetBool("chestOpen", true);

        if (audioSource != null && openSFX != null)
        {
            audioSource.PlayOneShot(openSFX);
            Debug.Log("Chest sound played");
        }
        else
        {
            Debug.LogWarning("Missing AudioSource or openSFX on " + gameObject.name);
        }

        rb.linearVelocity = knockback;
        rb.angularVelocity = Random.Range(-200f, 200f);

        DropKey();
        return true;
    }

    private void DropKey()
    {
        if (keyDropPrefab == null)
        {
            Debug.LogWarning("Key drop prefab is missing on " + gameObject.name);
            return;
        }

        Vector3 spawnPos = dropPoint != null ? dropPoint.position : transform.position;
        Instantiate(keyDropPrefab, spawnPos, Quaternion.identity);
    }
}