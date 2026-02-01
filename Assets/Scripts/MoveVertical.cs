using UnityEngine;

public class MoveVertical : MonoBehaviour
{
    public float speed = 2f;     // How fast it oscillates
    public float height = 2f;    // Max distance up/down

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float yOffset = Mathf.Sin(Time.time * speed) * height;

        transform.position = startPosition + new Vector3(0f, yOffset, 0f);
    }
}
