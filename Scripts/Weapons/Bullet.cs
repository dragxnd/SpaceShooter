using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    public Ship.Type OwnerType { get; set; }
    public float BulletSpeed { get; set; }
    public int Damage { get; set; }

    private Coroutine disableCoroutine;
    private readonly float disableTime = 2;
    private readonly string bulletTag = "Bullet";

    private readonly string hitExplosionPath = "Effects/Prefabs/DamageExplosion";
    private GameObject hitExplosionGameObject;
    private HitShipSound hitShipSound;
    private readonly int lifeTime = 2;

    private Dictionary<Ship.Type, Vector2> forceDirection = new Dictionary<Ship.Type, Vector2>()
    {
        {Ship.Type.Player, Vector2.up },
        {Ship.Type.Enemy, Vector2.down }
    };

    public void Init()
    {
        rigidbody.velocity = Vector2.zero;
        rigidbody.angularVelocity = 0;

        hitExplosionGameObject = Resources.Load(hitExplosionPath) as GameObject;
        hitShipSound = new HitShipSound();

        rigidbody.AddForce(forceDirection[OwnerType] * BulletSpeed);

        if (disableCoroutine != null) StopCoroutine(disableCoroutine);
        disableCoroutine = StartCoroutine(Disable());
    }


    public IEnumerator Disable()
    {
        yield return new WaitForSeconds(disableTime);
         PoolManager.ReleaseObject(gameObject);
    }

    public void DestroyActions()
    {
        PoolManager.ReleaseObject(gameObject);
        PoolManager.SpawnObject(hitExplosionGameObject, transform.position, transform.rotation, lifeTime);
        AudioManager.Instance.PlaySound(hitShipSound);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((OwnerType.ToString() != collision.tag))
        {
            if (disableCoroutine != null) StopCoroutine(disableCoroutine);
            var compShip = collision.gameObject.GetComponent<Ship>();
            compShip?.ApplyDamage(Damage,transform.position);

            DestroyActions();
        }
        else if (collision.tag == bulletTag)
        {
            if (disableCoroutine != null) StopCoroutine(disableCoroutine);
            DestroyActions();
        }
    }
}
