using UnityEngine;

public class Skill_objectDomain : Skill_Base
{
    private Skill_Ultimate domainManager;

    private float maxSize = 10;
    private float expandSpeed = 2;
    private float duration = 5;
    private float slowDownPercent = .9f;

    private Vector3 targetScale;
    private bool isShrinking;

    public void SetupDomain(Skill_Ultimate domainManager)
    {
        this.domainManager = domainManager;

        targetScale = Vector3.one * maxSize;
        Invoke(nameof(ShrinkDomain), duration);

    }

    private void Update()
    {
        HandleScaling();
    }

    private void HandleScaling()
    {
        float sizeDifference = Mathf.Abs(transform.localScale.x - targetScale.x);
        bool shouldChangeScale = sizeDifference > .1f;

        if (shouldChangeScale)
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, expandSpeed * Time.deltaTime);

        if (isShrinking && sizeDifference < .1f)
        {
            Destroy(gameObject);
        }
    }

    private void ShrinkDomain()
    {
        targetScale = Vector3.zero;
        isShrinking = true; 
    }
}
