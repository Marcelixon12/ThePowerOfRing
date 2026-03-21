using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{

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
            
        }
    }
    public override void Move()
    {
        if (distance < detectionDistance && distance > attackDistance)
        {
            // Obrócenie wroga w stronê gracza
            transform.LookAt(player.transform);
            // W³¹czenie animacji biegu
            anim.SetBool("Walk", true);
            // Poruszanie chrz¹szczem do przodu
            rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
        }
        else
        {
            // Wy³¹czenie animacji biegu
            anim.SetBool("Walk", false);
        }
    }
}
