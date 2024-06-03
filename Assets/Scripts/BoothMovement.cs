using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoothMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private AudioSource _sfx;

    private bool _isMovingUp;
    private bool _isMovingDown;

    private void Start()
    {
        _sfx = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _isMovingUp = true;
            _sfx.Play();
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            _isMovingUp = false;
            _sfx.Stop();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            _isMovingDown = true;
            _sfx.Play();
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            _isMovingDown = false;
            _sfx.Stop();
        }

        if (_isMovingUp == true)
        {
            MoveUp();
        }

        if (_isMovingDown == true)
        {
            MoveDown();
        }
    }

    private void MoveUp()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y >= -230f)
        {
            transform.position = new Vector3(transform.position.x, -230f, 106.0932f);
        }
    }

    private void MoveDown()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -237f)
        {
            transform.position = new Vector3(transform.position.x, -237, 106.0932f);
        }
    }
}
