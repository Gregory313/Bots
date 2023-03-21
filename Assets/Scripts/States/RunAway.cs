using UnityEngine;
using UnityEngine.AI;

public class RunAway : IState
{
    private readonly Gatherer _gatherer;
    private NavMeshAgent _navMeshAgent;
    private readonly EnemyDetector _enemyDetector;
    //private Animator _animator;
    //private readonly ParticleSystem _particleSystem;
    private static readonly int FleeHash = Animator.StringToHash("Flee");
    public float timeToRun;

    private float _initialSpeed;
    private const float FLEE_SPEED = 6F;
    private const float FLEE_DISTANCE = 5F;

    public RunAway(Gatherer gatherer, NavMeshAgent navMeshAgent)//, EnemyDetector enemyDetector)//, Animator animator, ParticleSystem particleSystem)
    {
        _gatherer = gatherer;
        _navMeshAgent = navMeshAgent;
        //_enemyDetector = enemyDetector;
        //_animator = animator;
        //_particleSystem = particleSystem;
    }

    public void OnEnter()
    {
        timeToRun = UnityEngine.Random.Range(4,8);
        _gatherer.proabilityToAction = Random.value;
        _gatherer.GetComponent<Renderer>().material.color = Color.red;
        _navMeshAgent.enabled = true;
       // _animator.SetBool(FleeHash, true);
        _initialSpeed = _navMeshAgent.speed;
        _navMeshAgent.speed = FLEE_SPEED;
       // _particleSystem.Play();
    }
    public void Tick()
    {
        if(timeToRun > 0)
        {
            timeToRun -= Time.fixedDeltaTime;
        }
        else
        {
            _gatherer.stopRun = true;
        }
        if (_navMeshAgent.remainingDistance < 1f)
        {
            var away = GetRandomPoint();
            _navMeshAgent.SetDestination(away);
        }
    }
    private Vector3 GetRandomPoint()
    {
        float posX = UnityEngine.Random.Range(-2, 2) * 5;
        float posZ = UnityEngine.Random.Range(-1, 7) * 5;
        return new Vector3(posX,0,posZ);
    }
    public void OnExit()
    {
        _gatherer.GetComponent<Renderer>().material.color = Color.yellow;
        _navMeshAgent.speed = _initialSpeed;
        _navMeshAgent.enabled = true;
        //_animator.SetBool(FleeHash, false);
    }
}
