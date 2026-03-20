using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float rotationSpeed = 8f;
    [SerializeField] private Transform model;

    private Rigidbody rb;
    private Animator animator;
    private Vector3 inputDirection;
    private float currentAnimSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();

        if (model == null && transform.childCount > 0)
            model = transform.GetChild(0);
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        inputDirection = new Vector3(horizontal, 0f, vertical).normalized;

        float targetAnimSpeed = inputDirection.sqrMagnitude > 0.001f ? 1f : 0f;
        currentAnimSpeed = Mathf.Lerp(currentAnimSpeed, targetAnimSpeed, 12f * Time.deltaTime);

        if (animator != null)
        {
            animator.SetFloat("Speed", currentAnimSpeed);
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = inputDirection * moveSpeed;
        Vector3 newPosition = rb.position + movement * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);

        if (inputDirection.sqrMagnitude > 0.001f && model != null)
        {
            Quaternion targetRotation = Quaternion.LookRotation(inputDirection, Vector3.up);
            model.rotation = Quaternion.Slerp(
                model.rotation,
                targetRotation,
                rotationSpeed * Time.fixedDeltaTime
            );
        }
    }
}