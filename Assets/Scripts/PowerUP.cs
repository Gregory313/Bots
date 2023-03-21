using UnityEngine;
public class PowerUP : MonoBehaviour
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
    //public int _value;

    //private int _available;
    //public bool IsDepleted => _available <= 0;

    //private void OnEnable()
    //{
    //    _value = UnityEngine.Random.Range(1, 5) * 20;
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("HIIIIII");
            Gatherer.SendPoints(_value);
            gameObject  .SetActive(false);
        }
    }
    //public void Destroy()
    //{
    //    transform.position += new Vector3(0, 5, 0);
    //    gameObject.SetActive(false);
    //}
}
