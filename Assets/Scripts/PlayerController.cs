using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed;
    public float shootThreshold;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float timeBetweenShot;
    public bl_Joystick JoystickLeft;
    public bl_Joystick JoystickRight;
    [SerializeField] private Vector2 leftStickInput;
    [SerializeField] private Vector2 rightStickInput;
    private Rigidbody2D playerBody;
    private bool canShoot;


    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        canShoot = true;
    }

    void FixedUpdate()
    {
        if (GameController.instance.IsGamePlaying)
        {
            GetPlayerInput();
        }
        else
        {
            leftStickInput = Vector2.zero;
            rightStickInput = Vector2.zero;
        }
        

        Vector2 currentMovement = leftStickInput * playerSpeed * Time.deltaTime;
        playerBody.MovePosition(playerBody.position + currentMovement);
       
        if(rightStickInput.magnitude > 0f)
        {
            Vector3 currentRotation = Vector3.left * rightStickInput.x + Vector3.down * rightStickInput.y;
            Quaternion playerRotation = Quaternion.LookRotation(currentRotation, Vector3.forward);
            playerBody.SetRotation(playerRotation);
            if(rightStickInput.magnitude > shootThreshold && canShoot)
            {
                Shoot();
            }
        }
        
    }

    private void GetPlayerInput()
    {
        leftStickInput = new Vector2(JoystickLeft.Horizontal, JoystickLeft.Vertical);
        rightStickInput = new Vector2(JoystickRight.Horizontal, JoystickRight.Vertical);
    }

    private void Shoot()
    {
        canShoot = false;
        Instantiate(bulletPrefab, firePoint.position, transform.rotation);
        StartCoroutine(ShotCooldown());
    }

    IEnumerator ShotCooldown()
    {
        yield return new WaitForSeconds(timeBetweenShot);
        canShoot = true;
    }

}
