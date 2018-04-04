using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Class{

	public enum Type{FatKid, FastKid, NormalKid};
	public Type TypeKid;
	public bool isChaser;
	public float stamina;
	public float speed;
	public float reactionSpeed;
	public float fov;


	public AI_Class(int i)
	{
		int t = Random.Range(0,3);
		switch (t)
		{
			case 0:
				TypeKid = Type.FatKid;
				stamina = 30f;
				speed = 2.8f;
				reactionSpeed = 4f;
				fov = 170f;
			break;

			case 1:
				TypeKid = Type.FastKid;
				stamina = 80f;
				speed = 4.5f;
				reactionSpeed = 1f;
				fov = 130f;
			break;

			case 2:
				TypeKid = Type.NormalKid;
				stamina = 50f;
				speed = 3.5f;
				reactionSpeed = 2f;
				fov = 150f;
			break;

			default:
				Debug.Log("POP GOES THE WEAZEL");
			break;
		}
	}
}