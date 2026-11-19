using UnityEngine;
using UnityEngine.InputSystem;

public class PipeSpawner : MonoBehaviour
{
    public GameObject pipePrefab;
    public float spawnInterval = 2f;
    public float minY = -1.5f;
    public float maxY = 2f;
    public float spawnX = 11f;

    private float timer;
    private bool isStarted;

    private void Update()
    {
        if (!isStarted)
        {
            bool jumpPressed =
                (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame) ||
                (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame);

            if (jumpPressed)
            {
                isStarted = true;
                SpawnPipe();
            }
            return;
        }

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnPipe();
        }
    }

    private void SpawnPipe()
    {
        float y = Random.Range(minY, maxY);
        Instantiate(pipePrefab, new Vector3(spawnX, y, 0f), Quaternion.identity);
    }
}
