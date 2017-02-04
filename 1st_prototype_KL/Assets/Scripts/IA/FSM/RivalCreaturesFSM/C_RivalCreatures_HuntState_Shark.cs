﻿using UnityEngine;
using System.Collections;
using System;

public class C_RivalCreatures_HuntState_Shark : I_State
{
    private static C_RivalCreatures_HuntState_Shark instance = null;
    private static readonly object padlock = new object();
    private float initialTimer = 2;
    private float timer = 0;

    private C_RivalCreatures_HuntState_Shark() { }

    public static C_RivalCreatures_HuntState_Shark Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new C_RivalCreatures_HuntState_Shark();
                }
                return instance;
            }
        }
    }

    public void Enter(C_BaseGameEntity entity)
    {
        timer = initialTimer;
    }

    public void Execute(C_BaseGameEntity entity)
    {
        C_RivalCreature_Shark rivalCreature = (C_RivalCreature_Shark)entity;

        GameObject prey = rivalCreature.getTarget();

        if (prey != null && (prey.tag.Contains("Fish") || prey.tag == "Orbs"))
        {
            bool preyIsHidden = false;

            if (prey.tag.Contains("Fish"))
            {
                preyIsHidden = rivalCreature.getTarget().GetComponent<C_Fish>().getIsHidden();
            }

            if (rivalCreature.GetComponentInChildren<BehaviourScript>().targetPosition != null && !preyIsHidden)
            {
                timer = initialTimer;
                rivalCreature.GetComponentInChildren<BehaviourScript>().behaviour = "Seek";
            }
            else if (preyIsHidden)
            {
                timer -= Time.deltaTime;
                if(timer <= 0)
                {
                    rivalCreature.setTarget(null);
                    rivalCreature.getFSM().ChangeState(C_RivalCreatures_WanderState_Shark.Instance);
                }
            }
            else
            {
                rivalCreature.setTarget(null);
                rivalCreature.getFSM().ChangeState(C_RivalCreatures_WanderState_Shark.Instance);
            }
        }
        else
        {
            rivalCreature.setTarget(null);
            rivalCreature.getFSM().ChangeState(C_RivalCreatures_WanderState_Shark.Instance);
        }
    }

    public void Exit(C_BaseGameEntity entity)
    {

    }
}
