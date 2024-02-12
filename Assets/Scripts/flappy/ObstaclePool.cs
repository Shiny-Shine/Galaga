using UnityEngine;

public class ObstaclePool : MonoBehaviour {
    public GameObject prefColumn; // Obstacle 프리팹
    private GameObject[] pColumns; // Obstacle들을 미리 구성해 놓을 예정
    private int colPoolSize = 5; // 미리 구성될 Obstacle들의 개수
    private int currentColIndex = 0; // Re-position 시킬 column의 index
    private float colSpawnRate = 3f; // Re-position 시키는 시간 간격
    private float spawnXPosition = 10f; // Re-position 시킬 때의 x-position
    private float colYPositionMax = 3f; // Re-position 시킬 때의 y-position 최대값
    private float colYPositionMin = -0.5f; // Re-position 시킬 때의 y-position 최소값

    void Start () {
    }

    public void InitColumnCreate () {
        pColumns = new GameObject[colPoolSize];
        for (int i = 0; i < pColumns.Length; i++)
            pColumns[i] =
                Instantiate (prefColumn, new Vector2 (-15, -25), Quaternion.identity);

        InvokeRepeating ("Spawn", 0f, colSpawnRate);
    }

    void Spawn () {
        if (FlappyManager.instFM.isGameOver) return;

        float _y_position = Random.Range (colYPositionMin, colYPositionMax);
        pColumns[currentColIndex].transform.position =
            new Vector2 (spawnXPosition, _y_position);

        currentColIndex = (currentColIndex + 1) % colPoolSize;
    }
}