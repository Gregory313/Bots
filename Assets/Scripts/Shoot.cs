using System.Collections;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float fireRate;
    public float cooldown;
    public int bulletInRow;
    private int power;
    private bool mayFire;
    public ParticleSystem part;

    //public ParticleSystem shoot;


    public bool _mayFire { get { return mayFire; } set { mayFire = value; if (value == true) StartCoroutine(FireA()); } }

    private void Start()
    {
    }
    private void OnEnable()
    {
        Shooting();
    }
    private void OnDisable()
    {
        mayFire = false;
        StopAllCoroutines();
    }
    public void Shooting()
    {
        mayFire = true;
        StartCoroutine(FireA());
    }
    IEnumerator FireA()
    {
        //for (int i = 0; i < bulletInRow; i++)
        //{
        //    part.Play();
        //    yield return new WaitForSeconds(fireRate);
        //}
        part.Play();

        yield return new WaitForSeconds(cooldown);
        if (mayFire)
            StartCoroutine(FireA());
        yield break;

    }
    public Vector3 SpawnPos()
    {
        float posX = (Random.value / 2);
        return new Vector3(posX, posX, -1);
    }
}
