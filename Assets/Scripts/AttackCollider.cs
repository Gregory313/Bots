using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    private Shoot _shoot;
    private void Start()
    {
        _shoot = GetComponent<Shoot>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            _shoot.enabled = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        _shoot.enabled = false;
    }
}
