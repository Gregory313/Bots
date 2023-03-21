using UnityEngine;
using System.Linq;

public class SearchForUps : IState
{
    private readonly Gatherer _gatherer;
    private readonly bool _isUps;

    public SearchForUps(Gatherer gatherer, bool isUps )
    {
        _gatherer = gatherer;
        _isUps = isUps;
    }
    public void Tick()
    {
        if (_isUps)
        {
            _gatherer.PowerUP = ChooseOneOfTheNearestPowerUP(5);
        }
        else
        {
            _gatherer.Medicine = ChooseOneOfTheNearestMedicine(5);
        }
    }

    private PowerUP ChooseOneOfTheNearestPowerUP(int pickFromNearest)
    {
        return Object.FindObjectsOfType<PowerUP>()
            .OrderBy(t => Vector3.Distance(_gatherer.transform.position, t.transform.position))
            //.Where(t => t.IsDepleted == false)
            .Take(pickFromNearest)
            .OrderBy(t => Random.Range(0, int.MaxValue))
            .FirstOrDefault();
    }
    private Medicine ChooseOneOfTheNearestMedicine(int pickFromNearest)
    {
        return Object.FindObjectsOfType<Medicine>()
            .OrderBy(t => Vector3.Distance(_gatherer.transform.position, t.transform.position))
            .Take(pickFromNearest)
            .OrderBy(t => Random.Range(0, int.MaxValue))
            .FirstOrDefault();
    }

    public void OnEnter() { _gatherer.GetComponent<Renderer>().material.color = Color.black;
        Debug.Log("enter search");
    }
    public void OnExit() { _gatherer.GetComponent<Renderer>().material.color = Color.yellow; }
}
