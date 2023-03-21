using UnityEngine;
using UnityEngine.AI;

public class AttackEnemy : IState
{
    private readonly Gatherer _gatherer;
    private readonly NavMeshAgent _navMeshAgent;
    private float _timeToChangePos = 3;
    private int _time;
    private float posZ = 2;
    //private readonly Animator _animator;
    //private static readonly int Speed = Animator.StringToHash("Speed");

    private Vector3 _lastPosition = Vector3.zero;
    public float TimeStuck;
    private float time;

    public AttackEnemy(Gatherer gatherer, NavMeshAgent navMeshAgent)//, Animator animator)
    {
        _gatherer = gatherer;
        _navMeshAgent = navMeshAgent;
        //_animator = animator;
    }
    public void Tick()
    {

        if (_timeToChangePos > 0)
        {
            _timeToChangePos -= Time.fixedDeltaTime;
        }
        else
        {
            _timeToChangePos = UnityEngine.Random.Range(1, 3);
            posZ = _timeToChangePos * 5;
        }
        _gatherer.transform.LookAt(_gatherer.Enemy.transform.position);
        if (Vector3.Distance(_gatherer.Enemy.transform.position, _gatherer.transform.position) >= 3f)
            _navMeshAgent.SetDestination(_gatherer.Enemy.transform.position - new Vector3(0,0,posZ));
    }

    public void OnEnter()
    {
        _gatherer.GetComponent<Renderer>().material.color = Color.blue;
        TimeStuck = 0f;
        _gatherer.attackCollider.enabled = true;
        _navMeshAgent.enabled = true;
        //_gatherer.transform.SetParent(_gatherer.Target.transform);
        //_navMeshAgent.SetDestination(_gatherer.Enemy.transform.position);
        // _animator.SetFloat(Speed, 1f);
    }
    public void ChangePos()
    {

    }

    public void OnExit()
    {
        _gatherer.GetComponent<Renderer>().material.color = Color.yellow;
        _gatherer.attackCollider.enabled = false;
        //   _animator.SetFloat(Speed, 0f);
    }
}
