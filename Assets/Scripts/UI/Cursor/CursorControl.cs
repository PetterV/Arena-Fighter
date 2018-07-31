using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CursorControl : MonoBehaviour
{
    public static CursorControl instance;

    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    void Awake()
    {
        if (instance == null) {
	        instance = this;
            SetDefaultCursor();
        } else if (instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad (gameObject);
    }

    public void SetDefaultCursor() {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }
}
