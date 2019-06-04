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

    // Update is called once per frame
    void Update()
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
