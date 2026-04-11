using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Goblin : Enemy
{
    
    float patrolTimer;
    public override void Attack()
    {
        timer += Time.deltaTime;
        if (distance < attackDistance && timer > cooldown)
        {
            // Resetowanie timera
            timer = 0;
            // Pobranie skryptu gracza i wywo³anie funkcji odejmowania zdrowia
            player.GetComponent<CharacterMovement>().ChangeHealth(damage);
            // W³¹czenie animacji ataku
            anim.SetBool("Attack", true);
            anim.SetBool("Move", false);
            anim.SetBool("Idle", false);
            anim.SetBool("Die", false);
        }
    }
    public override void Move()
    {
        // Je¿eli odleg³oœæ miêdzy wrogiem a graczem jest mniejsza ni¿ promieñ wykrywania chrz¹szcza
        // ORAZ odleg³oœæ miêdzy wrogiem a graczem jest wiêksza ni¿ promieñ ataku, to:
        if (distance < detectionDistance && distance > attackDistance)
        {
            // Obrócenie wroga w stronê gracza
            transform.LookAt(player.transform);
            // W³¹czenie animacji biegu
            anim.SetBool("Attack", false);
            anim.SetBool("Move", true);
            anim.SetBool("Idle", false);
            anim.SetBool("Die", false);
            // Poruszanie chrz¹szczem do przodu
            rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
        }
        else if (distance > detectionDistance)
        {
            rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
            patrolTimer += Time.deltaTime;
            anim.SetBool("Attack", false);
            anim.SetBool("Move", true);
            anim.SetBool("Idle", false);
            anim.SetBool("Die", false);
            if (patrolTimer > 2)
            {
                transform.Rotate(new Vector3(0, 90, 0));
                patrolTimer = 0;

            }
        }
        // W przeciwnym razie:
        else
        {
            // Wy³¹czenie animacji biegu
            anim.SetBool("Attack", false);
            anim.SetBool("Move", false);
            anim.SetBool("Idle", true);
            anim.SetBool("Die", false);
        }
    }
}
