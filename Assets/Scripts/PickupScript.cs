using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupScript : MonoBehaviour
{
    public Text scoretxt;
    private int score = 0;
    public GameObject Cop;

    private void Update()
    {
        //#display score in UI#
        scoretxt.text = "Score: " + score;
    }
    private void OnTriggerEnter(Collider coll)
    {
        //# IF YOU COLLECT A CHEESE THAN +1 SCORE;
        if (coll.gameObject.CompareTag("Cheese"))
        {
            score++;
            Destroy(coll.gameObject);
            Vector3 Pos = coll.transform.position;
            StartCoroutine(spawnCop(Pos));
        }
    }

    // ##Coroutine to wait till cop spawns on cheese possition##
    IEnumerator spawnCop(Vector3 Pos)
    {
            yield return new WaitForSeconds(2f);
            GameObject Newcop = Instantiate(Cop) as GameObject;
            Newcop.transform.position = Pos;
            yield return null;
    }
}
