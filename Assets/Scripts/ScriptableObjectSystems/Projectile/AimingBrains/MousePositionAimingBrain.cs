using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Aiming Brain/Mouse Position Aiming Brain")]
public class MousePositionAimingBrain : AimingBrain
{
  // maybe have a maximum rotational velocity
  private Rigidbody2D rb2d;
  private Camera main;

  public override void Initialize(ProjectileLauncherController launcherController)
  {
    main = Camera.main;
    rb2d = launcherController.GetComponent<Rigidbody2D>();
  }

  public override void Think(ProjectileLauncherController launcherController)
  {
    Vector2 lookDirection = (GetMouseWorldPosition() - rb2d.position).normalized;
    Debug.DrawLine(rb2d.position, GetMouseWorldPosition(), Color.magenta);
    float angle = Vector2.SignedAngle(Vector2.right, lookDirection);
    rb2d.SetRotation(angle);
  }

  private Vector2 GetMouseWorldPosition()
  {
    return main.ScreenToWorldPoint(Input.mousePosition);
  }
}
