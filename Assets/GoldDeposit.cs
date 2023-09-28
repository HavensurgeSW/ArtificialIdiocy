using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldDeposit : MonoBehaviour
{
    [SerializeField]int maxGold = 1000;
    [SerializeField]int currentGold = 0;
    [SerializeField]public int foodStored = 50;

    private void OnEnable()
    {
        Actions.OnWorkerMines += getExtracted;
        Actions.OnWorkerHungry += provideFood;
    }

    private void OnDisable()
    {
        Actions.OnWorkerMines -= getExtracted;
        Actions.OnWorkerHungry -= provideFood;
    }

    void Start()
    {
        currentGold = maxGold;
    }

    public void getExtracted(){
        currentGold -= 1;
    }

    public void provideFood() {
        foodStored -= 10;
    }

}
