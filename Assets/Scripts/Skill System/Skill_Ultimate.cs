using UnityEngine;

public class Skill_Ultimate : Skill_Base
{
    [SerializeField] private GameObject domainPrefab;
    public void CreateDomain() 
    {
        GameObject domain = Instantiate(domainPrefab, transform.position, Quaternion.identity);
    }
}
