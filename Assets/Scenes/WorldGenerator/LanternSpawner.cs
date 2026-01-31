using System.Collections.Generic;
using UnityEngine;

namespace WorldGenerator.LanternSpawner
{

    public class LanternSpawner : MonoBehaviour
    {
        public int quantity = 2;
        public GameObject lanternPrefab;

        private void Awake()
        {
            
            // find all by LanternSpawner tag
            GameObject[] spawners = GameObject.FindGameObjectsWithTag("LanternSpawner");

            
            if(quantity > spawners.Length)
            {
                quantity = spawners.Length;
            }
            
            HashSet<int> addedSpawners = new HashSet<int>();
            while (addedSpawners.Count < quantity)
            {
                int randomIndex = Random.Range(0, spawners.Length);
                
                if(addedSpawners.Contains(randomIndex)) continue;
                
                addedSpawners.Add(randomIndex);
                
                Vector3 spawnPosition = spawners[randomIndex].transform.position;
                Instantiate(lanternPrefab, spawnPosition, Quaternion.identity);
            }
            
            foreach (GameObject spawner in spawners)
            {
                Destroy(spawner);
            }
        }
    }
}