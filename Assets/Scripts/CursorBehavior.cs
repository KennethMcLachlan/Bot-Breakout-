using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorBehavior : MonoBehaviour
{
    private bool _cursorIsActive;

    void Start()
    {
        Cursor.visible = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            _cursorIsActive = !_cursorIsActive;

            if (_cursorIsActive == true)
            {
                Cursor.visible = true;
            }

            if (_cursorIsActive == false)
            {
                Cursor.visible = false;
            }
        }
        
    }
}
