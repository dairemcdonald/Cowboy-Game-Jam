using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public KeyCode key = KeyCode.None;
    public int bulletDirection;
    const float firerate = 1f;
    private float timestamp;
   

    void Start()
    {
        //Get the Animator attached to the GameObject you are intending to animate.
     
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(key) && Time.time >= timestamp)
        {
            CreateBullet();
            timestamp = Time.time + firerate;
           
        }
       
    }

    void CreateBullet()
    {
        var genBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Bullet bullet = genBullet.GetComponent<Bullet>();
        bullet.Direction(bulletDirection);
    }


}
