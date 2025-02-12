using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    EnemyPathfinding enemyPathfiding;
    State _state;

    enum State
    {
        Roaming
    }

    void Awake()
    {
        enemyPathfiding = GetComponent<EnemyPathfinding>();

        _state = State.Roaming;
    }

    void Start()
    {
        StartCoroutine(Roaming());
    }

    IEnumerator Roaming()
    {
        while (_state == State.Roaming)
        {
            Vector2 randomRoamingDirection = GetRoamingDirection();
            enemyPathfiding.MoveTo(randomRoamingDirection);

            yield return new WaitForSeconds(2f);
        }
    }

    Vector2 GetRoamingDirection()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
