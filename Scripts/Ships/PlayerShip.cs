using System;
using UnityEngine;

public class PlayerShip : Ship
{
  
    private readonly float offset = 1f;

    // Управление кораблем игрока
    public void Update()
    {
        if (!Enable) return;

        if (Input.GetKey(InputKeyCodes.Left))
        {
            if (rigidbody.transform.position.x > Extensions.OrthographicBounds().min.x + offset)
            {
                rigidbody.AddForce(Vector3.left * Time.deltaTime * CurrentMoveingSpeed);
            }
            else
            {
                rigidbody.velocity = Vector2.zero;
            }
        }

        if (Input.GetKey(InputKeyCodes.Right))
        {
            if (rigidbody.transform.position.x < Extensions.OrthographicBounds().max.x - offset)
            {
                rigidbody.AddForce(Vector3.right * Time.deltaTime * CurrentMoveingSpeed);
            }
            else
            {
                rigidbody.velocity = Vector2.zero;
            }

        }

        if (Input.GetKey(InputKeyCodes.StartGame) && Time.time > nextFireTime)
        {
            nextFireTime = Time.time + CurrentShootingSpeed;
            CurrentWeapon.Shoot(statMultiplier.WeaponDamageMultiplier);
        }
    }



}
