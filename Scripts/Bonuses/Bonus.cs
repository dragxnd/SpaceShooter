using UnityEngine;

[System.Serializable]
public class Bonus : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    private Vector2 dropForce = new Vector2(0, -250);
    private TakeBonus takeBonusSound;

    private void OnEnable()
    {
        takeBonusSound = new TakeBonus();
        rigidbody.AddForce(dropForce);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Ship.Type.Player.ToString() == collision.tag)
        {
            AudioManager.Instance.PlaySound(takeBonusSound);
            Execute(collision.GetComponent<PlayerShip>());
            Remove();
        }
    }

    public virtual void Execute(PlayerShip playerShip) {}

    public void Remove()
    {
        PoolManager.ReleaseObject(gameObject);
    }
}

