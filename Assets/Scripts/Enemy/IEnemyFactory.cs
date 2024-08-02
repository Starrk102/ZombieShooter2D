using UnityEngine;

namespace Enemy
{
    public interface IEnemyFactory
    {
        GameObject CreateEnemy(Vector3 position);
    }
}