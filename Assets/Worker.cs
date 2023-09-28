using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Worker : MonoBehaviour
{
    enum States
    {
        Idle,
        MovingTowardsRC,
        Deposit,
        MovingTowardsRes,
        Gather,
        Fleeing
    }
    enum Flags
    {
        OnEmptyBag,
        OnNextToTarget,
        OnFullBag,
        OnHungry,
        OnAlarm,
        OnSafety
    }

    public Transform CurrentTarget;
    public Transform[] Targets;

    private float speed = 5;
    private FSM fsm;

    [SerializeField] int resourceGatherValue;
    [SerializeField] int resourceBag;
    [SerializeField] int foodBag;

    private int exhaustion;
    private int extractionRate;

    // <Action call outs>

    private void OnEnable()
    {
        Actions.OnAlarmSound += alarmRang;
        Actions.OnAlarmSoundOff += alarmOff;
    }

    private void OnDisable()
    {
        Actions.OnAlarmSound -= alarmRang;
        Actions.OnAlarmSoundOff -= alarmOff;
    }

    private void Start()
    {
        extractionRate = 1;

        fsm = new FSM(Enum.GetValues(typeof(States)).Length, Enum.GetValues(typeof(Flags)).Length);
        

    
        fsm.SetRelation((int)States.Idle, (int)Flags.OnEmptyBag, (int)States.MovingTowardsRes);
        fsm.SetRelation((int)States.MovingTowardsRes, (int)Flags.OnNextToTarget, (int)States.Gather);
        fsm.SetRelation((int)States.Gather, (int)Flags.OnFullBag, (int)States.MovingTowardsRC);
        fsm.SetRelation((int)States.MovingTowardsRC, (int)Flags.OnNextToTarget, (int)States.Deposit);
        fsm.SetRelation((int)States.Deposit, (int)Flags.OnEmptyBag, (int)States.MovingTowardsRes);

        fsm.SetRelation((int)States.Deposit, (int)Flags.OnAlarm, (int)States.Fleeing);
        fsm.SetRelation((int)States.MovingTowardsRC, (int)Flags.OnAlarm, (int)States.Fleeing);
        fsm.SetRelation((int)States.MovingTowardsRes, (int)Flags.OnAlarm, (int)States.Fleeing);
        fsm.SetRelation((int)States.Gather, (int)Flags.OnAlarm, (int)States.Fleeing);
        fsm.SetRelation((int)States.Idle, (int)Flags.OnAlarm, (int)States.Fleeing);

        fsm.SetRelation((int)States.Fleeing, (int)Flags.OnSafety, (int)States.Idle);



        //fsm.AddBehaviour((int)States.Gather, () => { });

        fsm.AddBehaviour((int)States.MovingTowardsRes, () =>
        {
            CurrentTarget = Targets[1];
            transform.position += (CurrentTarget.transform.position - transform.position).normalized * speed * Time.deltaTime;

            if (Vector3.Distance(transform.position, CurrentTarget.transform.position) < 1.0f)
            {
                fsm.SetFlag((int)Flags.OnNextToTarget);
            }
        });

        fsm.AddBehaviour((int)States.Gather, () =>
        {
            gatherResource();

            if (resourceBag >= 15) {
                fsm.SetFlag((int)Flags.OnFullBag);
            }
        });


        fsm.AddBehaviour((int)States.MovingTowardsRC, () => {
            CurrentTarget = Targets[0];
            transform.position += (CurrentTarget.transform.position - transform.position).normalized * speed * Time.deltaTime;
            if (Vector3.Distance(transform.position, CurrentTarget.transform.position) < 1.0f)
            {
                fsm.SetFlag((int)Flags.OnNextToTarget);
            }
        });

        fsm.AddBehaviour((int)States.Deposit, () => {
            resourceBag = 0;
            fsm.SetFlag((int)Flags.OnEmptyBag);
        });

        fsm.AddBehaviour((int)States.Idle, () => {
            if(resourceBag==0)
                fsm.SetFlag((int)Flags.OnEmptyBag);
            
            if(resourceBag<=15&&resourceBag>0)
                fsm.SetFlag((int)Flags.OnFullBag);

        });

        fsm.AddBehaviour((int)States.Fleeing, () => {
            CurrentTarget = Targets[0];
            transform.position += (CurrentTarget.transform.position - transform.position).normalized * speed * Time.deltaTime;

            
        });

        fsm.SetCurrentStateForced((int)States.Idle);

    }

    private void Update()
    {
        fsm.Update();
    }

    void gatherResource() {

        if (foodBag >= 1)
        {
            resourceBag++;
            increaseExhaustion();
            Actions.OnWorkerMines();
        }
        else {
            if (Targets[1].GetComponent<GoldDeposit>().foodStored >= 5) {
                foodBag =+ 10;
                Actions.OnWorkerHungry();                
            }
        }
    }

    void increaseExhaustion() {
        exhaustion++;
        if (exhaustion >= 3) {
            consumeFood();
            exhaustion = 0;
        }
    }

    void consumeFood(){
        if(foodBag>0) foodBag--;
       
    }

    void alarmRang() {
        fsm.SetFlag((int)Flags.OnAlarm);
    }

    void alarmOff() {
        fsm.SetFlag((int)Flags.OnSafety);
    }
}
