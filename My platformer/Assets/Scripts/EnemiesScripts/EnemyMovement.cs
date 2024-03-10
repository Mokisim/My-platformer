using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform _path;
    [SerializeField] private bool _canFollow;
    [SerializeField] private EnemyVisibilityArea _visibilityArea;
    
    private Transform[] _points;
    private float _speed = 4f;
    private int _currentPoint;
    private bool _facingRight = true;
    
    private bool _isFollow;

    private Player _player;
    
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
        _isFollow = _visibilityArea.IsPlayer;

        if (_isFollow == false)
        {
            Patrol();
        }
        else
        {
            Follow(_player);
        }
    }

    private void OnEnable()
    {
        _visibilityArea.PlayerNoticed += SetPlayer;
    }

    private void OnDisable()
    {
        _visibilityArea.PlayerNoticed -= SetPlayer;
    }

    private void Patrol()
    {
        Transform target = _points[_currentPoint];

            transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);

            if (transform.position == target.position)
            {
                _currentPoint = ++_currentPoint % _points.Length;
            }

            TrackTarget(target);
    }

    private void Follow(Player player)
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, _speed * Time.deltaTime);
        TrackTarget(player.transform);
    }

    private void SetPlayer(Player player)
    {
        _player = player;
    }

    private void TrackTarget(Transform target)
    {
        if (transform.position.x > target.position.x && _facingRight == false || transform.position.x < target.position.x && _facingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        _facingRight = !_facingRight;

        transform.Rotate(0f, 180f, 0f);
    }
}
