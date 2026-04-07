using UnityEngine;

public class Enemy_BossAttackState : EnemyState
{
    private Enemy_Boss enemyBoss;
    public Enemy_BossAttackState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        enemyBoss = enemy as Enemy_Boss;
    }

    public override void Enter()
    {
        base.Enter();
        SyncAttackSpeed();

    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
        {
            if (enemyBoss.ShouldTeleport())
                stateMachine.ChangeState(enemyBoss.bossTeleportState);
            else
                stateMachine.ChangeState(enemyBoss.bossBattleState);
        }
    }
}
