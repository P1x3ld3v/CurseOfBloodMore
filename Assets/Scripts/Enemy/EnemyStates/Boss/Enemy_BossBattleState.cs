using UnityEngine;

public class Enemy_BossBattleState : Enemy_BattleState
{
    private Enemy_Boss enemyBoss;
    public Enemy_BossBattleState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        enemyBoss = enemy as Enemy_Boss;
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = enemyBoss.maxBattleIdleTime;
    }

    public override void Update()
    {
        stateTimer -= Time.deltaTime;
        UpdateAnimationParameters();

        if (stateTimer < 0)
        {
            stateMachine.ChangeState(enemyBoss.bossTeleportState);
        }

        if (enemy.PlayerDetected())
            UpdateTargetIfNeeded();

        if (WithinAttackRange() && enemy.PlayerDetected() && CanAttack())
        {
            lastTimeAttacked = Time.time;
            stateMachine.ChangeState(enemyBoss.bossAttackState);
        }
        else 
        {
            float xVelocity = enemy.canChasePlayer ? enemy.GetBattleMoveSpeed() : 0.001f;

            if (enemy.groundDetected == false)
            {
                xVelocity = 0.00001f;
            }
            enemy.SetVelocity(enemy.battleMoveSpeed * DirectionToPlayer(), rb.linearVelocity.y);
        }
    }
}
