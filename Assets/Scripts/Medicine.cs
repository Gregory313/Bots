using UnityEngine;

public class Medicine : MonoBehaviour
{
    [SerializeField] private int _totalAvailable = 20;

    private int _available;
    public int _value;
    public bool IsDepleted => _available <= 0;

    private void OnEnable()
    {
        _value = UnityEngine.Random.Range(1, 5) * 20;
        _available = _totalAvailable;
    }

    public bool Take()
    {
        gameObject.SetActive(false);
        return true;
    }

    public void SetAvailable(int amount) => _available = amount;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("HIIIIII");
        Gatherer.SendPoints(_value);
    }
}
