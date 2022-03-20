using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineContainer : MonoBehaviour
{
  public static LineContainer Instance { get; private set; }
  private Dictionary<string, LineRenderer> lineRenderers;

  private void Awake()
  {
    Instance = this;
    
    lineRenderers = new Dictionary<string, LineRenderer>();
  }

  public void AddLine(string tag, Vector3 start, Vector3 end, float width, Color color)
  {
    GameObject myLine = new GameObject();
    myLine.transform.position = start;
    myLine.AddComponent<LineRenderer>();
    LineRenderer lr = myLine.GetComponent<LineRenderer>();
    lr.startWidth = 0.1f;
    lr.endWidth = 0.1f;
    lr.startColor = color;
    lr.endColor = color;
    lr.positionCount = 2;
    lr.SetPosition(0, start);
    lr.SetPosition(0, end);
    lineRenderers.Add(tag, lr);
  }

  public void UpdateLine(string tag, Vector3 start, Vector3 end)
  {
    LineRenderer lr = lineRenderers[tag];
    if (lr != null)
    {
      lr.SetPosition(0, start);
      lr.SetPosition(1, end);
    }
  }

  public void RemoveLine(string tag)
  {
    LineRenderer lr = lineRenderers[tag];
    if (lr != null)
    {
      lineRenderers.Remove(tag);
      Destroy(lr.gameObject);
    }
  }
}
