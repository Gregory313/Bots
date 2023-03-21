using UnityEngine;

public class RotateAround : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.RotateAround(transform.parent.position, new Vector3(0, 1, 0), 100 * Time.deltaTime);
    }
}
