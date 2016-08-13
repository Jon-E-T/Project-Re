using UnityEngine;
using System.Collections;

public class CoinGenerator : MonoBehaviour
{
    // ObjectPooler is a seperate script
    // Drag GameObjects from the Hierarchy
    public ObjectPooler[] coinPool;
    public float distanceBetweenCoins;
    public int coinSelector;

    void Update()
    {
        // Picks a coin to spawn
        coinSelector = Random.Range(0, coinPool.Length);
    }

    // '.GetPooledObject()' is a method in the 'ObjectPooler' Script

    public void SpawnCoins(Vector3 startPosition)
    {
        // Gold Coin
        if (coinSelector == 0)
        {
            // Picks how many Gold coins to place 
            int goldCoinAmount = Random.Range(0, 3);

            switch (goldCoinAmount)
            {
                case 1:
                    {
                        GameObject coinOne = coinPool[coinSelector].GetPooledObject();
                        coinOne.transform.position = startPosition;
                        coinOne.SetActive(true);
                        break;
                    }
                case 2:
                    {
                        GameObject coinOne = coinPool[coinSelector].GetPooledObject();
                        coinOne.transform.position = startPosition;
                        coinOne.SetActive(true);

                        GameObject coinTwo = coinPool[coinSelector].GetPooledObject();
                        coinTwo.transform.position = new Vector3(startPosition.x - distanceBetweenCoins, startPosition.y, startPosition.z);
                        coinTwo.SetActive(true);
                        break;
                    }
                default:
                    {
                        GameObject coinOne = coinPool[coinSelector].GetPooledObject();
                        coinOne.transform.position = startPosition;
                        coinOne.SetActive(true);

                        GameObject coinTwo = coinPool[coinSelector].GetPooledObject();
                        coinTwo.transform.position = new Vector3(startPosition.x - distanceBetweenCoins, startPosition.y, startPosition.z);
                        coinTwo.SetActive(true);

                        GameObject coinThree = coinPool[coinSelector].GetPooledObject();
                        coinThree.transform.position = new Vector3(startPosition.x + distanceBetweenCoins, startPosition.y, startPosition.z);
                        coinThree.SetActive(true);
                        break;
                    }
            }

        }

        // Bronze Coin
        //else if (coinSelector == 1)
        //{
        //    GameObject coinOne = coinPool[coinSelector].GetPooledObject();
        //    coinOne.transform.position = startPosition;
        //    coinOne.SetActive(true);
        //}
    }
}
