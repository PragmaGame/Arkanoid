using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Platform : MonoBehaviour
{
    [SerializeField] private GameLogic _gameLogic;
    [SerializeField] private float _speed;
    private Rigidbody2D _rigidbody;

    private Vector2 _tapPosition;
    private Vector2 _direction;
    
    private bool _isMobile;
    private bool _isGameActive = true;

    private void Awake()
    {
        _isMobile = Application.isMobilePlatform;
        
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _gameLogic.GameOveredEvent += OnGameOver;
    }

    private void OnDisable()
    {
        _gameLogic.GameOveredEvent -= OnGameOver;
    }

    private void Update()
    {
        if (_isGameActive)
        {
            Movement();
        }
    }

    private void OnGameOver()
    {
        _direction = Vector2.zero;
        _isGameActive = false;
    }
    
    private void FixedUpdate()
    {
        _rigidbody.velocity = _direction * _speed;
    }

    private void Movement()
    {
        if (!_isMobile)
        {
            if (Input.GetMouseButton(0))
            {
                Vector2 currentPosition = Input.mousePosition;
                _direction = Vector2.zero;

                if (currentPosition.x > _tapPosition.x)
                    _direction = Vector2.right;
                else if (currentPosition.x < _tapPosition.x)
                    _direction = Vector2.left;
                else
                    _direction = Vector2.zero;

                _tapPosition = currentPosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                _direction = Vector2.zero;
            }
        }
        else
        {
            
        }
        
    }
}

/*private void SwipeControl()
{
    if (!_isMobile)
    {
        Debug.Log("Swipe comp");
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Swip true");
            _isSwiping = true;
            _tapPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            ResetSwipe();
        }
    }
    else
    {
        Debug.Log("Swipe mobile");
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                _isSwiping = true;
                _tapPosition = Input.GetTouch(0).position;
            }
            else if(Input.GetTouch(0).phase == TouchPhase.Canceled || Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                ResetSwipe();
            }
        }
    }

    CheckSwipe();
}

private void CheckSwipe()
{
    _swipeDelta = Vector2.zero;

    if (_isSwiping)
    {
        Debug.Log("check swipe true");
        if (!_isMobile && Input.GetMouseButton(0))
            _swipeDelta = (Vector2) Input.mousePosition - _tapPosition;
        else if (Input.touchCount > 0)
            _swipeDelta = Input.GetTouch(0).position - _tapPosition;
    }

    Debug.Log("Delta : " + _swipeDelta.magnitude);
    
    if (_swipeDelta.magnitude > _deadZone)
    {
        Debug.Log("magnitude > ");
        if (Mathf.Abs(_swipeDelta.x) > Mathf.Abs(_swipeDelta.y))
            Control(_swipeDelta.x > 0 ? Vector2.right : Vector2.left);
        else 
            Control(_swipeDelta.y > 0 ? Vector2.up : Vector2.down);
    }
    
    ResetSwipe();
}

private void ResetSwipe()
{
    _isSwiping = false;
    _tapPosition = Vector2.zero;
    _swipeDelta = Vector2.zero;
}

}*/
