using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoter : MonoBehaviour
{
    public GunData Data;
    public int Damage = 4;
    public float MaxDistance = 5;
    public float minDistance = 3;
    public int spread = 57;
    public int Knockback = 0;

    public int InitIemainAmmo = 5;
    public int InitAmmoInMagazine = 2;

    public int IemainAmmo;

    public int AmmoInMagazine;

    private int _rechargeAmmo = 1;

    public GameObject bulletPrefab;

    public Queue<GameObject> Bullets = new Queue<GameObject>();


    private void Awake()
    {
        for (int i = 0; i < 100; ++i)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            Bullets.Enqueue(bullet);
            bullet.SetActive(false);
        }
    }

    private void OnEnable()
    {
        IemainAmmo = InitIemainAmmo;
        AmmoInMagazine = InitAmmoInMagazine;
    }

    public bool TryShot()
    {
        if (AmmoInMagazine > 0)
        {
            return true;
        }
        return false;
    }

    public void shot(Vector3 dir)
    {
        for (int i = 0; i < Damage; ++i)
        {
            GameObject bullet = Bullets.Dequeue();
            bullet.transform.position = transform.position;

            bullet.transform.rotation = Quaternion.FromToRotation(bullet.transform.forward, Quaternion.AngleAxis(Random.Range(-spread / 2, spread / 2), Vector3.up) * dir);
            bullet.GetComponent<Bullet>().distance = Random.Range(minDistance, MaxDistance) * 0.06f/100;

            bullet.SetActive(true);
            Bullets.Enqueue(bullet);
        }
        --AmmoInMagazine;

    }

    public void tryReload()
    {
        if (InitAmmoInMagazine > AmmoInMagazine && IemainAmmo > 0)
        {
            Reload();
        }
        else if (InitIemainAmmo > IemainAmmo)
        {
            Recharge();
        }
    }

    public void Reload()
    {
        int ammoToFill = Mathf.Min(InitAmmoInMagazine - AmmoInMagazine, IemainAmmo);
        AmmoInMagazine += ammoToFill;
        IemainAmmo -= ammoToFill;
    }

    public void Recharge()
    {
        IemainAmmo += _rechargeAmmo;
    }
}
