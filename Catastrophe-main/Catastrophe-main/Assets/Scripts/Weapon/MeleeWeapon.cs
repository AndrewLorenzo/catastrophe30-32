using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : WeaponBase
{
    [SerializeField] GameObject leftMeleeObject;
    [SerializeField] GameObject rightMeleeObject;

    Movement playerMove;
    [SerializeField] Vector2 attackSize = new Vector2(4f, 2f);

    private void Awake()
    {
        playerMove = GetComponentInParent<Movement>();
    }

    private void ApplyDamage(Collider2D[] colliders)
    {
        int damage = GetDamage();
        for (int i = 0; i < colliders.Length; i++)
        {
            IDamageable temp = colliders[i].GetComponent<IDamageable>();
            if (temp != null)
            {

                PostDamage(damage, colliders[i].transform.position);
                temp.TakeDamage(damage);
            }
        }
    }

    public override void Attack()
    {
        StartCoroutine(AttackProcess());
    }

    IEnumerator AttackProcess()
    {
        for (int i = 0; i < weaponStats.numberOfAttacks; i++)
        {
            // Debug.Log("Attack");
            if (playerMove.lastHorizontalVector > 0)
            {
                rightMeleeObject.SetActive(true);
                Collider2D[] colliders = Physics2D.OverlapBoxAll(rightMeleeObject.transform.position, attackSize, 0f);
                ApplyDamage(colliders);
            }
            else
            {
                leftMeleeObject.SetActive(true);
                Collider2D[] colliders = Physics2D.OverlapBoxAll(leftMeleeObject.transform.position, attackSize, 0f);
                ApplyDamage(colliders);
            }
            yield return new WaitForSeconds(0.3f);
        }

    }

}

