using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bullet;
    public GameObject bulletSpawnPoint;
    public AudioSource gunShot;
    public GameObject parentObject;
    private float timeBetweenShots;
    private float timeBetweenShotsDefault;

    // Use this for initialization
    private void Start()
    {
        timeBetweenShotsDefault = 0.3f;
        timeBetweenShots = 0f;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0)) ShootBullet();

        ReduceTimer();
    }

    private void ShootBullet()
    {
        if (timeBetweenShots <= 0f)
        {
            var gunSoundCheck = GameObject.Find("Player").GetComponent<GunSound>();
            gunShot.Play();
            gunSoundCheck.GunSoundCheck();

            Instantiate(bullet, bulletSpawnPoint.transform.position, parentObject.transform.rotation);
            timeBetweenShots = timeBetweenShotsDefault;
        }
    }

    private void ReduceTimer()
    {
        timeBetweenShots = timeBetweenShots - Time.deltaTime;
    }
}