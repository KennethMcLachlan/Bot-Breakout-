using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoothMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            MoveUp();
            //Add SFX
        }

        if (Input.GetKey(KeyCode.E))
        {
            MoveDown();
            //Add SFX
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
