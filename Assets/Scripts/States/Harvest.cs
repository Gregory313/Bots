using UnityEngine;

public class Harvest : IState
{
    private readonly Gatherer _gatherer;
    //private readonly Animator _animator;
    private float _resourcesPerSecond = 3;
    private readonly bool _isUpper;

    private float _nextTakeResourceTime;
    //private static readonly int Harvest = Animator.StringToHash("Harvest");

    public Harvest(Gatherer gatherer, bool isUpper)//, Animator animator)
    {
        _gatherer = gatherer;
        //_animator = animator;
    }

    public void Tick()
    {
        if (_isUpper)
        {
            if (_gatherer.PowerUP != null)
            {
                if (_nextTakeResourceTime <= Time.time)
                {
                    _nextTakeResourceTime = Time.time + (1f / _resourcesPerSecond);
                    _gatherer.TakePowerUP();
                    //_animator.SetTrigger(Harvest);
                }
            }
        }
        else
        {
            if (_gatherer.Medicine != null)
            {
                if (_nextTakeResourceTime <= Time.time)
                {
                    _nextTakeResourceTime = Time.time + (1f / _resourcesPerSecond);
                    _gatherer.TakeMedicine();
                    //_animator.SetTrigger(Harvest);
                }
            }
        }
    }

    public void OnEnter()
    {
        Debug.Log("enter harvest");
    }

    public void OnExit()
    {
    }
}
