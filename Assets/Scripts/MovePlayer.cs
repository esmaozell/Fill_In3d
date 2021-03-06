using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovePlayer : MonoBehaviour
{
    [SerializeField]
    Image[] joystickSprites;

    public Canvas parentCanvas;

    bool isPressed = false;

    void Update()
    {
        HandleJoystick();
    }

    void HandleJoystick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isPressed = true;
            SetJoystickImages(true);
        }

        if (!isPressed)
        {
            Vector2 movePos;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                parentCanvas.transform as RectTransform,
                Input.mousePosition, parentCanvas.worldCamera,
                out movePos);

            transform.position = parentCanvas.transform.TransformPoint(movePos);
        }


        if (Input.GetMouseButtonUp(0))
        {
            isPressed = false;
            SetJoystickImages(false);

        }
    }

    void SetJoystickImages(bool activeness)
    {
        int colorAlpha = activeness ? 255 : 0;

        for (int i = 0; i < joystickSprites.Length; i++)
        {
            joystickSprites[i].color = new Color(joystickSprites[i].color.r, joystickSprites[i].color.g, joystickSprites[i].color.b, colorAlpha);
        }
    }
}
