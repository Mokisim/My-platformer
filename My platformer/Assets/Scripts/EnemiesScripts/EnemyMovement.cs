using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform _path;
    [SerializeField] private bool _canFollow;
    
    private Transform[] _points;
    private float _speed = 4f;
    private int _currentPoint;
    private bool _facingRight = true;
    
    private bool _isPatrol = true;
    private bool _isFollow;
    private bool _isReturn;
    
    private void Awake()
    {
        _points = new Transform[_path.childCount];

        for (int i = 0; i < _path.childCount; i++)
        {
            _points[i] = _path.GetChild(i);
        }
    }

    private void Update()
    {
        Patrol();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<Player>(out Player player))
        {
            player.TakeDamage();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _isFollow = true;
            _isPatrol = false;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, _speed * Time.deltaTime);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _isFollow = false;
            _isPatrol = true;
        }
    }

    private void Patrol()
    {
        if (_isPatrol == true)
        {
            Transform target = _points[_currentPoint];

            transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);

            if (transform.position == target.position)
            {
                _currentPoint++;

                if (_currentPoint >= _points.Length)
                {
                    _currentPoint = 0;
                }
            }

            if (transform.position.x > target.position.x && _facingRight == false)
            {
                Flip();
            }
            else if (transform.position.x < target.position.x && _facingRight)
            {
                Flip();
            }
        }
        else
        {
            return;
        }
    }

    private void Flip()
    {
        _facingRight = !_facingRight;

        transform.Rotate(0f, 180f, 0f);
    }
}
