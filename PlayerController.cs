using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool FacingLeft {get{return facingLeft;} set {facingLeft = value;}}
    [SerializeField] private float moveSpeed = 5f;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D myBody;
    private Animator myAnim; 
    private SpriteRenderer mySpriteRenderer;

    private bool facingLeft = false;

    // Start is called before the first frame update

    private void Awake() {
        playerControls = new PlayerControls();
        myBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable() {
        playerControls.Enable();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayertInput(); // Good for Player Input
    }

    private void FixedUpdate() {
        AdjustPlayerFacingDirection();
        Move();
    }

    private void PlayertInput() {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();
        
        myAnim.SetFloat("moveX", movement.x);
        myAnim.SetFloat("moveY", movement.y);
    }

    private void Move(){
        myBody.MovePosition(myBody.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void AdjustPlayerFacingDirection(){ //Checks if mouse position is on the left or right side of the screen from the middle point
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if(mousePos.x < playerScreenPoint.x) {
            mySpriteRenderer.flipX = true;
            FacingLeft = true;
        } else {
            mySpriteRenderer.flipX = false;
            FacingLeft = false;
        }
    }
}
