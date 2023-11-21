using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{

  public GameObject pos1, pos2;
  public float Speed;
  Vector2 targetPos;

  // Start is called before the first frame update
  void Start()
  {
      targetPos = pos2.transform.position;
  }

  // Update is called once per frame
  void Update()
  {
    if (Vector3.Distance(transform.position, pos1.transform.position) < .1f)
    {
      targetPos = pos2.transform.position;
    }
    else
    {
      targetPos = pos1.transform.position;
    }

    transform.position = Vector3.MoveTowards(transform.position, targetPos, Speed * Time.deltaTime);

  }
}
