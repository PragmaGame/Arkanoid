using UnityEngine;

public class Block : MonoBehaviour, IPooleable
{
    [SerializeField] private int _health;
    [SerializeField] private int _score;
    [SerializeField] private Types _type;

    public bool IsDead { get; set; }

    public Types Type => _type;
    public int Score => _score;

    private void OnEnable()
    {
        IsDead = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out Ball ball))
        {
            ChangeHealth(ball.GetDamage);
        }
    }

    private void ChangeHealth(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            IsDead = true;
        }
    }

}
