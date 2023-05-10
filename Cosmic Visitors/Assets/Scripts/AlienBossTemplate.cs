using UnityEngine;

[CreateAssetMenu(fileName ="New Alien Boss", menuName ="Scriptable Objects/Aliens/Alien Boss")]
public class AlienBossTemplate : ScriptableObject
{

    public string BossName;
    public GameObject AlienBossPrefab;
    public int Health;
    public int MoveSpeed;

    [HideInInspector] public float[] MoveTimer = new float[(int)BossPhases.ALL];
    [HideInInspector] public float[] StayTimer = new float[(int)BossPhases.ALL];
    [HideInInspector] public float[] ShotsTimer = new float[(int)BossPhases.ALL];
    [HideInInspector] public BulletTemplate[] BossBT = new BulletTemplate[(int)BossPhases.ALL];
    [Space(5)]

    [Header("Phase 1")]
    public BulletTemplate BulletTemplateP1;
    [Tooltip("Choose the fight behaviour of the boss")]
    public BossAttackBehaviour behaviourP1;
    [Tooltip("Boss will move for x seconds then it will stop")]
    public float MoveTimerP1;
    [Tooltip("Boss will stay in position for x seconds then it will move")]
    public float StayTimerP1;
    [Tooltip("Every x second the boss will shoot")]
    public float ShotsTimerP1;
    [Space(5)]

    [Header("Phase 2")]
    public BulletTemplate BulletTemplateP2;
    [Tooltip("choose the fight behaviour of the boss")] 
    public BossAttackBehaviour behaviourP2;
    [Tooltip("Boss will move for x seconds then it will stop")] 
    public float MoveTimerP2;
    [Tooltip("Boss will stay in position for x seconds then it will move")] 
    public float StayTimerP2;
    [Tooltip("Every x second the boss will shoot")] 
    public float ShotsTimerP2;
    [Space(5)]

    [Header("Phase 3")]
    public BulletTemplate BulletTemplateP3;
    [Tooltip("choose the fight behaviour of the boss")]
    public BossAttackBehaviour behaviourP3;
    [Tooltip("Boss will move for x seconds then it will stop")]
    public float MoveTimerP3;
    [Tooltip("Boss will stay in position for x seconds then it will move")]
    public float StayTimerP3;
    [Tooltip("Every x second the boss will shoot")]
    public float ShotsTimerP3;

    public float BulletHellTimeChange = 2.0f;

    public void InitBoss()
    {
        MoveTimer[(int)BossPhases.Phase1] = MoveTimerP1;
        StayTimer[(int)BossPhases.Phase1] = StayTimerP1;
        ShotsTimer[(int)BossPhases.Phase1] = ShotsTimerP1;
        BossBT[(int)BossPhases.Phase1] = BulletTemplateP1;

        MoveTimer[(int)BossPhases.Phase2] = MoveTimerP2;
        StayTimer[(int)BossPhases.Phase2] = StayTimerP2;
        ShotsTimer[(int)BossPhases.Phase2] = ShotsTimerP2;
        BossBT[(int)BossPhases.Phase2] = BulletTemplateP2;

        MoveTimer[(int)BossPhases.Phase3] = MoveTimerP3;
        StayTimer[(int)BossPhases.Phase3] = StayTimerP3;
        ShotsTimer[(int)BossPhases.Phase3] = ShotsTimerP3;
        BossBT[(int)BossPhases.Phase3] = BulletTemplateP3;
    }
}
