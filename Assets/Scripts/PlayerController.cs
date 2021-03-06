using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public FixedJoystick veriableJoystick;

    public Rigidbody rb;


    Quaternion targetRotation;

    void FixedUpdate()
    {
        if (LevelManager.Instance.levelFinish)
        {
            return;
        }
        Run();
        Rotate();
    }

    void Run()
    {
        if (veriableJoystick.Vertical != 0 || veriableJoystick.Horizontal != 0)
        {
            Vector3 direction = Vector3.forward * veriableJoystick.Vertical + Vector3.right * veriableJoystick.Horizontal;
            rb.velocity = direction * speed * Time.fixedDeltaTime;
            
        }
    }

    void Rotate()
    {
        if (Input.GetMouseButton(0))
        {
            var input = new Vector3(veriableJoystick.Horizontal, 0, veriableJoystick.Vertical);

            if (input != Vector3.zero)
            {
                targetRotation = Quaternion.LookRotation(input);
            }
            else
            {
               // transform.rotation = Quaternion.identity;
            }

            var rot = Quaternion.RotateTowards(transform.rotation, targetRotation, (speed/ 10  ));
            
            transform.rotation= Quaternion.Lerp(transform.rotation,rot,Time.fixedDeltaTime*10);
        }
        
    }

}

