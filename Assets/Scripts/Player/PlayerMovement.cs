using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Animator), typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    // Shop references
    public GameObject panel;
    public GameObject errorText;
    public TMPro.TMP_InputField cheatingInputField;
    public Transform shopKeeper;
    public float shopThresholdRange = 3f;
    public float errorTextShowTime = 2f;
    public float allowedShopTime = 15f;
    public GameObject attackPetPrefab;
    public GameObject healPetPrefab;
    float totalShopTime = 0f;
    float errorTextTimeShown = 0f;
    bool isShopping;
    public bool isCheating = false;

    public float baseSpeed = 5f;

    public float speed = 5f;

    Vector3 movement;
    Animator animator;
    Rigidbody playerRigidbody;
    int floorMask;
    float camRayLength = 100f;

    float duration = 0;

    float buffPercentage = 1f;

    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        animator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        speed = baseSpeed;
    }

    private void Start()
    {
        // Re-instantiate pet
        int attPetCount = GameManager.Instance.GetPetAmount("attack");
        int healPetCount = GameManager.Instance.GetPetAmount("heal");

        for (int i = 0; i < attPetCount; i++)
        {
            Instantiate(attackPetPrefab, this.transform.position, Quaternion.identity);
        }

        for (int i = 0; i < healPetCount; i++)
        {
            Instantiate(healPetPrefab, this.transform.position, Quaternion.identity);
        }
    }

    public void TwoTimeSpeed()
    {
        speed = speed * 2;
    }

    void FixedUpdate()
    {
        if (duration > 0)
        {
            duration -= Time.deltaTime;
        }
        else
        {
            buffPercentage = 0f;
        }
        speed = baseSpeed + baseSpeed * buffPercentage;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Move(h, v);
        Turning();
        Animating(h, v);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (totalShopTime <= allowedShopTime)
            {
                if (IsNearShopkeeper())
                {
                    errorText.SetActive(false);
                    errorTextTimeShown = 0f;
                    panel.SetActive(true);
                    isShopping = true;
                }
                else
                {
                    errorText.SetActive(true);
                    errorTextTimeShown = errorTextShowTime;
                }
            }
        } 
        else
        {
            if (errorTextTimeShown > 0)
            {
                errorTextTimeShown -= Time.deltaTime;
            } else
            {
                errorText.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isCheating = !isCheating;

            if (isCheating)
            {
                cheatingInputField.gameObject.SetActive(isCheating);
                cheatingInputField.onEndEdit.AddListener(EndInputField);
                cheatingInputField.Select();
                cheatingInputField.ActivateInputField();
                cheatingInputField.text = "";
            } 
            else
            {
                cheatingInputField.text = "";
                cheatingInputField.DeactivateInputField();
                cheatingInputField.gameObject.SetActive(isCheating);
            }
        }

        // Close shop if move too far away or total shopping time exceeded allowedShopTime
        if (!IsNearShopkeeper() && panel.activeInHierarchy || totalShopTime > allowedShopTime)
        {
            panel.SetActive(false);
        }

        // If shop panel is closed
        if (!panel.activeInHierarchy)
        {
            isShopping = false;
        }

        // Keep track on shopping time
        if (isShopping)
        {
            totalShopTime += Time.deltaTime;
        }
    }

    void EndInputField(string text)
    {
        cheatingInputField.text = "";
        cheatingInputField.DeactivateInputField();
        cheatingInputField.gameObject.SetActive(false);
        isCheating = false;
    }

    void Move(float h, float v)
    {
        movement.Set(h, 0, v);
        // Debug.Log("Speed");
        movement = movement.normalized * speed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + movement);
        GameEventsManager.instance.playerActionEvents.TriggerPlayerMovement(movement.magnitude);
    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0;

            var rotation = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(rotation);
        }
    }

    void Animating(float h, float v)
    {
        bool walking = h != 0f || v != 0f;
        animator.SetBool("IsWalking", walking);
    }

    public void Buff(float speedToAdd, float duration)
    {
        this.duration = duration;
        this.buffPercentage = speedToAdd;
    }

    public void AddBuff(float speedToAdd)
    {
        this.baseSpeed += speedToAdd;
    }

    bool IsNearShopkeeper()
    {
        if (Vector3.Distance(this.transform.position, shopKeeper.position) <= shopThresholdRange)
        {
            return true;
        }
        return false;
    }

    public void RefreshShopTime()
    {
        totalShopTime = 0f;
    }
}
