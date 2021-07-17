using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    [SerializeField] private float _startForce;
    [SerializeField] private int _damage;

    public int GetDamage => _damage;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _rigidbody.AddForce(new Vector2(Random.Range(-1f,1f),1) * _startForce,ForceMode2D.Impulse); 
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Vector2 _direction = transform.position;
        Vector2 normal = other.GetContact(0).normal;
        
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.AddForce(Vector2.Reflect(_direction, normal).normalized * _startForce,ForceMode2D.Impulse);
    }
}
