using UnityEngine;

public class Enemy_BossTeleportState : EnemyState
{
    private Enemy_Boss enemyBoss;
    public Enemy_BossTeleportState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        enemyBoss = enemy as Enemy_Boss;
    }

    public override void Enter()
    {
        base.Enter();
        //enemyBoss.MakeUntargetable(true);
    }

    public override void Update()
    {
        base.Update();

        if (enemyBoss.teleportTrigger)
        {
            enemyBoss.transform.position = enemyBoss.FindTeleportPoint();
            enemyBoss.SetTeleportTrigger(false);
        }

        if (triggerCalled)
        {
            if (enemyBoss.CanDoSpellCast())
                stateMachine.ChangeState(enemyBoss.bossSpellCastState);
            else
                stateMachine.ChangeState(enemyBoss.bossBattleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        //enemyBoss.MakeUntargetable(false);
    }
}
