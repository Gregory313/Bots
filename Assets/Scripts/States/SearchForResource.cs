using System.Linq;
using UnityEngine;

public class SearchForResource : IState
{
    private readonly Gatherer _gatherer;

    public SearchForResource(Gatherer gatherer)
    {
        _gatherer = gatherer;
    }
    public void Tick()
    {
        _gatherer.Enemy = ChooseOneOfTheNearestResources(5);
    }

    private Enemy ChooseOneOfTheNearestResources(int pickFromNearest)
    {
         return Object.FindObjectsOfType<Enemy>()
             .OrderBy(t=> Vector3.Distance(_gatherer.transform.position, t.transform.position))
             .Where(t=> t.IsDepleted == false)
             .Take(pickFromNearest)
             .OrderBy(t => Random.Range(0, int.MaxValue))
             .FirstOrDefault();
    }

    public void OnEnter() { _gatherer.GetComponent<Renderer>().material.color = Color.green; }
    public void OnExit() { _gatherer.GetComponent<Renderer>().material.color = Color.yellow; }
}