using UnityEngine;
using UnityEngine.AI;

public class MoveToObj : IState
{
    private readonly Gatherer _gatherer;
    private readonly NavMeshAgent _navMeshAgent;
    private readonly Animator _animator;
    private readonly bool _isUpper;
    private static readonly int Speed = Animator.StringToHash("Speed");

    private Vector3 _lastPosition = Vector3.zero;

    public float TimeStuck;

    public MoveToObj(Gatherer gatherer, NavMeshAgent navMeshAgent, bool isUpper)//, Animator animator)
    {
        _gatherer = gatherer;
        _navMeshAgent = navMeshAgent;
        _isUpper = isUpper;
        //_animator = animator;
    }
    public void Tick()
    {
        if (Vector3.Distance(_gatherer.transform.position, _lastPosition) <= 0f)
            TimeStuck += Time.deltaTime;

        _lastPosition = _gatherer.transform.position;
    }

    public void OnEnter()
    {
        _gatherer.GetComponent<Renderer>().material.color = Color.grey;
        TimeStuck = 0f;
        _navMeshAgent.enabled = true;
        if (_isUpper)
        {
            Debug.Log("POWERUP");
            _navMeshAgent.SetDestination(_gatherer.PowerUP.transform.position);
        }
        else
        {
            Debug.Log("MEDICINE");
            _navMeshAgent.SetDestination(_gatherer.Medicine.transform.position);
        }
        //_animator.SetFloat(Speed, 1f);
    }

    public void OnExit()
    {
        _gatherer.GetComponent<Renderer>().material.color = Color.yellow;
        _navMeshAgent.enabled = false;
        //_animator.SetFloat(Speed, 0f);
    }
}
