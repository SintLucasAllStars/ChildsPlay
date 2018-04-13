using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentralIntelligence : MonoBehaviour {
	public Vector3[] sector;
	public Vector3[,] sectorVectors;

	void Awake () {
		sector = new Vector3[4];
		sectorVectors = new Vector3[sector.Length, 2];
	}

	void Start () {
		for (int i = 0; i < sectorVectors.GetLength(0); i++) {
			for (int j = 0; j < sectorVectors.GetLength(1); j++) {
				sectorVectors [i, 0] = GameObject.Find ("Sector Location SE" + i).GetComponent<Transform>().position;
				sectorVectors [i, 1] = GameObject.Find ("Sector Location NW" + i).GetComponent<Transform>().position;
			}
		}
		MoveToSector (1);
	}

	public Vector3 MoveToSector (int s) {
		Vector3 destination = new Vector3 (Random.Range (sectorVectors [s, 0].x, sectorVectors [s, 1].x), Random.Range (sectorVectors [s, 0].y, sectorVectors [s, 1].y), Random.Range (sectorVectors [s, 0].z, sectorVectors [s, 1].z));
		return destination;
	}

	public Vector3 MoveInSector (int s) {
		Vector3 destination = new Vector3 (Random.Range (sectorVectors [s, 0].x, sectorVectors [s, 1].x), Random.Range (sectorVectors [s, 0].y, sectorVectors [s, 1].y), Random.Range (sectorVectors [s, 0].z, sectorVectors [s, 1].z));
		return destination;
	}
}