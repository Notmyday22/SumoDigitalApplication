using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
  [SerializeField] private float moveSpeed = 2f;

  private Rigidbody2D myBody;
  private Vector2 moveDir;

  private void Awake() {
    myBody = GetComponent<Rigidbody2D>();
  }

  private  void FixedUpdate() {
    myBody.MovePosition(myBody.position + moveDir*(moveSpeed * Time.fixedDeltaTime));
  }

  public void MoveTo(Vector2 targetposition){
    moveDir = targetposition;
  }
}
