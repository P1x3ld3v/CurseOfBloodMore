using UnityEngine;

public class Enemy_BossAnimationTrigger : Enemy_AnimationTriggers
{
    private Enemy_Boss enemyBoss;

    protected override void Awake()
    {
        base.Awake();
        enemyBoss = GetComponentInParent<Enemy_Boss>();
    }

    private void TeleportTrigger()
    {
        enemyBoss.SetTeleportTrigger(true);
    }
}
