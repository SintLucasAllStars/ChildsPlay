using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeRotator : MonoBehaviour {

	void Start () {
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, Random.Range(-180, 181), transform.eulerAngles.z);
	}
}
