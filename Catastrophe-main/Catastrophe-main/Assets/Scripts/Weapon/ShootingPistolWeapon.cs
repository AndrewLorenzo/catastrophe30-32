using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingPistolWeapon : WeaponBase
{

    Movement playerMove;

    [SerializeField] GameObject PistolBulletPrefab;
    [SerializeField] float spread = 0.5f;

    private void Awake()
    {
        playerMove = GetComponentInParent<Movement>();
    }

    public override void Attack()
    {

        for (int i = 0; i < weaponStats.numberOfAttacks; i++)
        {
            GameObject bullet = Instantiate(PistolBulletPrefab);

            Vector3 newPosition = transform.position;
            if (weaponStats.numberOfAttacks > 1)
            {
                newPosition.y -= (spread * weaponStats.numberOfAttacks - 1) / 2;
                newPosition.y += spread * i;
            }

            bullet.transform.position = newPosition;

            ShootingBulletProjectile shootingBulletProjectile = bullet.GetComponent<ShootingBulletProjectile>();
            shootingBulletProjectile.SetDirection(playerMove.lastHorizontalVector, 0f);
            shootingBulletProjectile.damage = GetDamage();

        }


    }

}
