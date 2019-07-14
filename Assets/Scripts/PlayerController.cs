using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public KeyCode key = KeyCode.None;
    public int bulletDirection;
    public string keyString;

    const float firerate = 1f;
    private float timestamp;

    public LevelManager levelManager;
    Animator m_Animator;

    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        levelManager = FindObjectOfType<LevelManager>();
        keyString = key.ToString();
        levelManager.AddUnit(keyString, this);

    }

    // Update is called once per frame
    public void ShootInput()
    {
        if (Time.time >= timestamp)
        {
            CreateBullet();
            timestamp = Time.time + firerate;
            m_Animator.SetTrigger("Shoot");

        }
       
    }

    void CreateBullet()
    {
        var genBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Bullet bullet = genBullet.GetComponent<Bullet>();
        bullet.Direction(bulletDirection);
    }

    void OnDestroy()
    {
        levelManager.RemoveUnit(key.ToString());
    }

}
