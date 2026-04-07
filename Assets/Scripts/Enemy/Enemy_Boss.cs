using System.Xml;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using System.Collections;

public class Enemy_Boss : Enemy, ICounterable
{
    public Collider2D col;

    public bool CanBeCountered { get => canBeStunned; }
    public Enemy_BossAttackState bossAttackState { get; private set; }
    public Enemy_BossBattleState bossBattleState { get; private set; }
    public Enemy_BossTeleportState bossTeleportState { get; private set; }
    public Enemy_BossSpellCastState bossSpellCastState { get;  private set; }

    [Header("Boss Details")]
    public float maxBattleIdleTime = 5;

    [Header("Boss SpellCast")]
    [SerializeField] private GameObject spellCastPrefab;
    [SerializeField] private int amountToCast = 6;
    [SerializeField] private float spellCastRate = 1.2f;
    [SerializeField] private float spellCastStateCooldown = 10;
    [SerializeField] private Vector2 playerOffsetPrediction;
    private float lastTimeCastedSpells = float.NegativeInfinity;

    public bool spellCastPreformed { get; private set; }
    private Player playerScript;


    [Header("Boss Teleport")]
    [SerializeField] private BoxCollider2D arenaBounds;
    [SerializeField] private float offsetCenterY = 1.00f;
    [SerializeField] private float chanceTeleport = .25f;
    private float defaultTeleportChance;
    public bool teleportTrigger { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        idleState = new Enemy_IdleState(this, stateMachine, "idle");
        moveState = new Enemy_MoveState(this, stateMachine, "move");
        deadState = new Enemy_DeadState(this, stateMachine, "idle");
        stunnedState = new Enemy_StunnedState(this, stateMachine, "stunned");
        bossBattleState = new Enemy_BossBattleState(this, stateMachine, "battle");
        bossAttackState = new Enemy_BossAttackState(this, stateMachine, "attack");
        bossTeleportState = new Enemy_BossTeleportState(this, stateMachine, "teleport");
        bossSpellCastState = new Enemy_BossSpellCastState(this, stateMachine, "spellCast");
        battleState = bossBattleState;

    }
    protected override void Start()
    {
        base.Start();
        arenaBounds.transform.parent = null;
        defaultTeleportChance = chanceTeleport;
        stateMachine.Initialize(idleState);
    }

    public void HandleCounter()
    {
        if (CanBeCountered == false)
            return;

        stateMachine.ChangeState(stunnedState);
    }
    public override void SpecialAttack()
    {
        StartCoroutine(CastSpellCo());
    }

    private IEnumerator CastSpellCo()
    {
        if (playerScript == null)
            playerScript = player.GetComponent<Player>();

        for (int i = 0; i < amountToCast; i++)
        {
            bool playerMoving = playerScript.rb.linearVelocity.magnitude > 0;

            float xOffset = playerMoving ? playerOffsetPrediction.x * playerScript.facingDir : 0;
            Vector3 spellPosition = player.transform.position + new Vector3(xOffset, playerOffsetPrediction.y);

            Enemy_BossSpell spell
                = Instantiate(spellCastPrefab, spellPosition, Quaternion.identity).GetComponent<Enemy_BossSpell>();

            spell.SetupSpell(combat);

            yield return new WaitForSeconds(spellCastRate);
        }

        SetSpellCastPreformed(true);
    }
    public void SetSpellCastPreformed(bool spellCastStatus) => spellCastPreformed = spellCastStatus;
    public bool CanDoSpellCast() => Time.time > lastTimeCastedSpells + spellCastStateCooldown;
    public void SetSpellCastOnCooldown() => lastTimeCastedSpells = Time.time;
    public bool ShouldTeleport()
    {
        if (Random.value < chanceTeleport)
        {
            chanceTeleport = defaultTeleportChance;
            return true;
            
        }
            chanceTeleport = chanceTeleport + 0.5f;
        return false;
    }

    public void SetTeleportTrigger(bool triggerStatus) => teleportTrigger = triggerStatus;

    public Vector3 FindTeleportPoint() 
    {
        int maxAttempts = 10;
        float bossWithColliderHalf = col.bounds.size.x/2;

        for (int i = 0; i < maxAttempts; i++)
        {
            float randomX = Random.Range(arenaBounds.bounds.min.x, 
                                         arenaBounds.bounds.max.x - bossWithColliderHalf);
            Vector2 raycastPoint = new Vector2(randomX, arenaBounds.bounds.max.y);

            RaycastHit2D hit = Physics2D.Raycast(raycastPoint, Vector2.down, Mathf.Infinity, whatIsGround);

            if (hit.collider != null)
            {
                return hit.point + new Vector2(0, offsetCenterY);
            }

        }
            return transform.position;

    }
}
