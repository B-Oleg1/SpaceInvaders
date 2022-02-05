using UnityEngine;

public class MovePlayerScript : MonoBehaviour
{
    [SerializeField]
    private RectTransform _player;

    private readonly int _speed = 5;

    private bool _playerMoving = false;
    private string _direction = string.Empty;

    private void Update()
    {
        if (_playerMoving)
        {
            MovePlayer();
        }
    }

    public void MouseDown(string direction)
    {
        _playerMoving = true;
        _direction = direction;
    }

    public void MouseUp()
    {
        _playerMoving = false;
        _direction = string.Empty;
    }

    // If the cursor/finger moved to the other side while pressing the button
    public void PointerEnter(string direction)
    {
        _direction = direction;
    }

    public void PointerExit()
    {
        _direction = string.Empty;
    }

    // Player movement
    private void MovePlayer()
    {
        if (_direction == "Right")
        {
            // We check that the starboard side does not fly out of space
            if (_player.transform.localPosition.x + _player.rect.width / 2 < Screen.width / 2)
            {
                _player.transform.localPosition = new Vector2(_player.transform.localPosition.x + _speed, _player.transform.localPosition.y);
            }
        }
        else if (_direction == "Left")
        {
            if (_player.transform.localPosition.x + Screen.width / 2 - _player.rect.width / 2 > 0)
            {
                _player.transform.localPosition = new Vector2(_player.transform.localPosition.x - _speed, _player.transform.localPosition.y);
            }
        }
    }
}