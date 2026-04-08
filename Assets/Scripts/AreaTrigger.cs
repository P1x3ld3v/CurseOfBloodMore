using UnityEngine;

public class AreaTrigger : MonoBehaviour
{
    [SerializeField] private string areaName = "Blacksmith";
    [SerializeField] private float cooldownSeconds = 1f;

    private float nextAllowedTime;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            Debug.Log("Player entered area: ");
            return;
        }
           
        if (Time.time < nextAllowedTime) return;

        nextAllowedTime = Time.time + cooldownSeconds;

        AreaNameUI.Instance.Show(areaName);
    }
}