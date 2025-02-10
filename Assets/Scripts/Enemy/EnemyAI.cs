using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    EnemyPathfiding enemyPathfiding;
    State _state;

    enum State
    {
        Roaming
    }

    void Awake()
    {
        enemyPathfiding = GetComponent<EnemyPathfiding>();

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
            Vector2 randomRoamingPosition = GetRoamingPosition();
            enemyPathfiding.MoveTo(randomRoamingPosition);

            yield return new WaitForSeconds(2f);
        }
    }

    Vector2 GetRoamingPosition()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
