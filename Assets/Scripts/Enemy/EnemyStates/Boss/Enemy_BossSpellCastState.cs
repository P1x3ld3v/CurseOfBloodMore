using UnityEngine;

public class Enemy_BossSpellCastState : EnemyState
{
    private Enemy_Boss enemyBoss;
    public Enemy_BossSpellCastState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        enemyBoss = enemy as Enemy_Boss;

    }

    public override void Enter()
    {
        base.Enter();

        enemyBoss.SetVelocity(0, 0);
        enemyBoss.SetSpellCastPreformed(false);
        enemyBoss.SetSpellCastOnCooldown();
    }

    public override void Update()
    {
        base.Update();
        if (enemyBoss.spellCastPreformed)
        {
            anim.SetBool("spellCast_Performed", true);
        }

        if (triggerCalled)
        {
            if (enemyBoss.ShouldTeleport())
                stateMachine.ChangeState(enemyBoss.bossTeleportState);

            else
                stateMachine.ChangeState(enemyBoss.bossBattleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        anim.SetBool("spellCast_Performed", false);
    }
}
