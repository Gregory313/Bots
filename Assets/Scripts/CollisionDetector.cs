using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CollisionDetector : MonoBehaviour
{
    public bool ObjInRange => _detectedBeast != null;

    private Beast _detectedBeast;
    private Medicine _medicine;
    private PowerUP _powerUP;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Beast>())
        {
            _detectedBeast = other.GetComponent<Beast>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Beast>())
        {
            StartCoroutine(ClearDetectedBeastAfterDelay());
        }
    }

    private IEnumerator ClearDetectedBeastAfterDelay()
    {
        yield return new WaitForSeconds(3f);
        _detectedBeast = null;
    }

    public Vector3 GetNearestBeastPosition()
    {
        return _detectedBeast?.transform.position ?? Vector3.zero;
    }
}
