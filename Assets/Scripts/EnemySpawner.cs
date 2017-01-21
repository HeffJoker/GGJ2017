using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public float MaxX = 100;
    public float MaxY = 100;
    public float MinX = -100;
    public float MinY = -100;

    public ObjectPool EnemyPool;
    public ObjectPool TorpedoPool;
    public int NumEnemies = 5;

    public void SpawnEnemies()
    {
        for(int i = 0; i < NumEnemies; ++i)
        {
            GameObject enemy = EnemyPool.GetObject(true);

            enemy.transform.position = new Vector3(Random.Range(MinX, MaxX), Random.Range(MinY, MaxY));

            SpriteRenderer sprite = enemy.GetComponentInChildren<SpriteRenderer>();
            sprite.transform.rotation = Quaternion.AngleAxis(Random.Range(0, 360), -Vector3.back);

            WeaponSlot[] weapons = enemy.GetComponentsInChildren<WeaponSlot>();

            foreach (WeaponSlot slot in weapons)
                slot.ProjectilePool = TorpedoPool;
        }
    }

    public void ClearEnemies()
    {
        EnemyPool.Instances.ForEach(x => x.SetActive(false));
    }

    private void Start()
    {
        SpawnEnemies();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(new Vector3(MinX, MinY), new Vector3(MaxX, MinY));
        Gizmos.DrawLine(new Vector3(MaxX, MinY), new Vector3(MaxX, MaxY));
        Gizmos.DrawLine(new Vector3(MaxX, MaxY), new Vector3(MinX, MaxY));
        Gizmos.DrawLine(new Vector3(MinX, MaxY), new Vector3(MinX, MinY));
    }
}
