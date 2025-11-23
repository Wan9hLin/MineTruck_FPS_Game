using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayeController : MonoBehaviour
{
    public static PlayeController instance;

    public float Movespeed, gravityModifier, jumpingPower, runSpeed = 12f;
    public CharacterController charcon;
    private bool canDoubleJump;
    private Vector3 moveInput;

    public Transform camTrans;

    public float MouseSdensivity;

    public Animator anim;

    public GameObject bullet;
    public Transform firePoint;

    public Gun activeGun;

    public List<Gun> allGuns = new List<Gun>();
    public int currentGun;

    public GameObject muzzleFlash;

    public AudioSource footStepFast, footStepSlow;
    private float bounceAmount;
    private bool bounce;

    public float maxViewAngle = 60f;
    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        activeGun = allGuns[currentGun];
        activeGun.gameObject.SetActive(true);

        UIController.instance.ammoText.text = "AMMO: " + activeGun.currentAmmo;
    }

    // Update is called once per frame
    void Update()
    {

        if (!UIController.instance.pauseScreen.activeInHierarchy && !GameManager.instance.ending)
        {




            float yStore = moveInput.y;//saving last y position of the player

            Vector3 vertMove = transform.forward * Input.GetAxis("Vertical");
            Vector3 horiMove = transform.right * Input.GetAxis("Horizontal");

            moveInput = horiMove + vertMove;
            moveInput.Normalize();

            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveInput = moveInput * runSpeed;
            }
            else
            {
                moveInput = moveInput * Movespeed;
            }

            //moveInput = moveInput * Movespeed;


            moveInput.y = yStore;//continue the from latest position of the y player
            moveInput.y += Physics.gravity.y * gravityModifier * Time.deltaTime;

            if (charcon.isGrounded)//detect ground
            {
                moveInput.y = 0f;
                moveInput.y += Physics.gravity.y * gravityModifier * Time.deltaTime;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    moveInput.y = jumpingPower;
                    canDoubleJump = true;
                }
            }
            if (!charcon.isGrounded && canDoubleJump && Input.GetKeyDown(KeyCode.Space))
            {
                moveInput.y = jumpingPower;
                canDoubleJump = false;

            }

            if (bounce)
            {
                bounce = false;
                moveInput.y = bounceAmount;
                canDoubleJump = true;
            }

            charcon.Move(moveInput * Time.deltaTime);

            //control camera rotation
            Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * MouseSdensivity;

            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);
            camTrans.rotation = Quaternion.Euler(camTrans.rotation.eulerAngles.x - mouseInput.y, camTrans.rotation.eulerAngles.y, camTrans.rotation.eulerAngles.z);
            //camTrans.rotation = Quaternion.Euler(camTrans.eulerAngles + new Vector3(-mouseInput.y, 0f, 0f));

            
            if(camTrans.rotation.eulerAngles.x > maxViewAngle && camTrans.rotation.eulerAngles.x <180f)
            {
                camTrans.rotation = Quaternion.Euler(maxViewAngle, camTrans.rotation.eulerAngles.y, camTrans.rotation.eulerAngles.z);
            }
            if(camTrans.rotation.eulerAngles.x > 180f && camTrans.rotation.eulerAngles.x < 360f - maxViewAngle)
            {
                camTrans.rotation = Quaternion.Euler(-maxViewAngle, camTrans.rotation.eulerAngles.y, camTrans.rotation.eulerAngles.z);
            }

            muzzleFlash.SetActive(false);
            anim.SetFloat("moveSpeed", Mathf.Abs(moveInput.z));
            // Debug.Log(Mathf.Abs(moveInput.z));

            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;//invisible laser
                if (Physics.Raycast(camTrans.position, camTrans.forward, out hit, 1500f))//summon invisible stick/laser, length:1500 m
                {
                    firePoint.LookAt(hit.point + new Vector3(0f, 0.2f, 0f));//firepoint will look at the hit point
                }
                else
                {
                    firePoint.LookAt(camTrans.position + (camTrans.forward * 30f));//when we direct the gun in the air, it will juest lokk straight
                }
                FireShot();

            }
            //repeating shots
            if (Input.GetMouseButton(0) && activeGun.canAutoFire)
            {
                if (activeGun.fireCounter <= 0)
                {
                    FireShot();
                }
            }

            //change the gun
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                SwitchGun();
            }
        }

        }

        public void FireShot()
        {

            if (activeGun.currentAmmo > 0)//we have ammo
            {
                activeGun.currentAmmo--;

                Instantiate(activeGun.bullet, firePoint.position, firePoint.rotation);//summon the prefab object
                activeGun.fireCounter = activeGun.fireRate;
                UIController.instance.ammoText.text = "AMMO: " + activeGun.currentAmmo;

                muzzleFlash.SetActive(true);


            }
        }

        public void SwitchGun()
        {
            activeGun.gameObject.SetActive(false);

            currentGun++;
            if (currentGun >= allGuns.Count)
            {
                currentGun = 0;
            }
            activeGun = allGuns[currentGun];
            activeGun.gameObject.SetActive(true);

            UIController.instance.ammoText.text = "AMMO: " + activeGun.currentAmmo;
        }

        public void Bounce(float bounceForce)
        {
            bounceAmount = bounceForce;
            bounce = true;


        }

    }
