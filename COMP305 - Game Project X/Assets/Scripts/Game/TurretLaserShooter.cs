using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLaserShooter : MonoBehaviour
{
    [SerializeField] private GameObject laser, laserSpawn;
    [SerializeField] [Range(0.01f, 1.99f)] private float fireRate;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LaserShooter(fireRate));
    }
    
    private IEnumerator LaserShooter(float fireRate)
    {
        while (true)
        {
            GameObject gObj;
            gObj = GameObject.Instantiate(laser, laserSpawn.transform.position, laserSpawn.transform.rotation);
            gObj.transform.Rotate(new Vector3(0, 0, 90));

            yield return new WaitForSeconds(2 - fireRate);
        }
    }
}
