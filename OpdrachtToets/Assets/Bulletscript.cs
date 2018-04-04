using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulletscript : MonoBehaviour {

    Vector3 dir = new Vector3(1, 0 ,0);
    float speed = 0.1f;

    public int damage = 1;

    private void Awake()
    {
        StartCoroutine(DestroyItem());
    }

    public void Update()
    {
        transform.position += dir * speed / 2;
    }

    IEnumerator DestroyItem()
    {
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }
}
