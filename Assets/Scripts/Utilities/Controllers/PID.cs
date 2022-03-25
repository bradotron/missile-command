public class PID
{
  public float Kp, Ki, Kd;
  private float P, I, D;
  private float prevError;

  public PID(float kp, float ki, float kd)
  {
    Kp = kp;
    Ki = ki;
    Kd = kd;
  }
  public float GetOutput(float currentError, float deltaTime)
  {
    P = currentError;
    I += P * deltaTime;
    D = (P - prevError) / deltaTime;
    prevError = currentError;

    return P * Kp + I * Ki + D * Kd;
  }

  public void ResetIntegral()
  {
    I = 0f;
  }

}
