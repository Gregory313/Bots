using System;
using UnityEngine;
using UnityEngine.AI;

public class Gatherer : MonoBehaviour
{
    public event Action<int> OnGatheredChanged;

    [SerializeField] private int _maxCarried = 20;

    private StateMachine _stateMachine;
    private int _gathered;
    public float distanceToAttack;
    public float hP;
    public float points;
    public int criticalHPDelighter;
    public bool isRunForHealth;
    public bool stopRun;
    public float proabilityToAction;
    public ParticleSystem shot;
    public bool stuck = false;
    public BoxCollider attackCollider;

    public Enemy Enemy { get; set; }
    public PowerUP PowerUP { get; set; }
    public Medicine Medicine { get; set; }

    public static event Action<int> onHpUpdate;
    public static event Action<float> onPointsUpdate;

    public static void SendHpUpdate(int value)
    {
        onHpUpdate?.Invoke(value);
    }
    public static void SendPoints(float value)
    {
        onPointsUpdate?.Invoke(value);
    }
    private void Awake()
    {
        attackCollider = GetComponentInChildren<BoxCollider>();
        proabilityToAction = UnityEngine.Random.value;
        Debug.Log(proabilityToAction + " proabilityToAction");
        var navMeshAgent = GetComponent<NavMeshAgent>();
        var animator = GetComponent<Animator>();
        var enemyDetector = gameObject.AddComponent<EnemyDetector>();
        var fleeParticleSystem = gameObject.GetComponentInChildren<ParticleSystem>();

        _stateMachine = new StateMachine();

        var searchEnemy = new SearchForResource(this);
        var moveToEnemy = new MoveToSelectedResource(this, navMeshAgent);
        var ranFromEnemy = new RunAway(this, navMeshAgent);
        var attackEnemy = new AttackEnemy(this, navMeshAgent);
        var findMedicine = new SearchForUps(this, false);
        var findPi = new SearchForUps(this, true);
        var moveToMedicine = new MoveToObj(this, navMeshAgent, false);
        var moveToPI = new MoveToObj(this, navMeshAgent, true);
        var harvestMedicine = new Harvest(this, false);
        var harvestPI = new Harvest(this, true);



        At(searchEnemy, moveToEnemy, HasEnemy());
        At(moveToEnemy, searchEnemy, StuckForOverASecond());
        At(moveToEnemy, attackEnemy, ReachedEnemy());
        At(attackEnemy, searchEnemy, TargetDead());

        _stateMachine.AddAnyTransition(ranFromEnemy, () => hP <= hP / criticalHPDelighter && stopRun == false);

        if (proabilityToAction > 0.5f)
        {
            At(ranFromEnemy, findMedicine, () => stopRun == true);
            At(findMedicine, moveToMedicine, () => true);
            At(moveToMedicine, harvestMedicine, ReachedMedicine());
            At(harvestMedicine, searchEnemy, HasEnemy());
        }
        else
        {
            At(ranFromEnemy, findPi, () => stopRun == true);
            At(findPi, moveToPI, () => true);
            At(moveToPI, harvestPI, ReachedPI());
            At(harvestPI, searchEnemy, HasEnemy()); ;
        }

        _stateMachine.SetState(searchEnemy);

        void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);
        Func<bool> HasEnemy() => () => Enemy != null;
        Func<bool> ReachedEnemy() => () => Enemy != null && Vector3.Distance(transform.position, Enemy.transform.position) < 3f;
        Func<bool> ReachedMedicine() => () => Medicine != null && Vector3.Distance(transform.position, Medicine.transform.position) < 1f;
        Func<bool> ReachedPI() => () => PowerUP != null && Vector3.Distance(transform.position, PowerUP.transform.position) < 1f;
        Func<bool> StuckForOverASecond() => () => moveToEnemy.TimeStuck > 0.1f;
        Func<bool> TargetDead() => () => (Enemy == null || Enemy.IsDepleted);
    }

    private void Update()
    {
        _stateMachine.Tick();
    }
    public void TakeFromTarget()
    {
        if (Enemy.Take())
        {
            _gathered++;
            OnGatheredChanged?.Invoke(_gathered);
        }
    }
    public bool TakePowerUP()
    {
        if (PowerUP.Take())
        {
            points += 20;
        }
        return true;
    }
    public bool TakeMedicine()
    {
        if (Medicine.Take())
        {
            hP += 20;
        }
        return true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            hP -= 2;
            if (hP < hP / criticalHPDelighter)
            {
                stopRun = false;
            }

        }
    }
}