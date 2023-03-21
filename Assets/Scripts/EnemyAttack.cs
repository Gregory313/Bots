using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int attackTime;
    public int continuesAttackTime;
    private BoxCollider boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        boxCollider.enabled = false;
        yield return new WaitForSeconds(attackTime);
        boxCollider.enabled = true;
        yield return new WaitForSeconds(continuesAttackTime);
        StartCoroutine(Attack());
    }
}
