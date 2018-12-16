﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gifts : MonoBehaviour
{
    public enum Buffs {
        moreLife,
        moreSpeed

    }

    public enum Debuffs {
        lessLife,
        lessSpeed,
        newEnemy
    }

    public List<Button> choiceButtons;
    Dictionary<Button,Buffs> choiceBuffs = new Dictionary<Button,Buffs>();
    Dictionary<Button,Debuffs> choiceDebuffs = new Dictionary<Button,Debuffs>();

    /// <summary>
    /// Aleatoriza um buff
    /// </summary>
    /// <returns>buff</returns>
    public Buffs RandomizeBuff(){
        int enumLen = Enum.GetNames(typeof(Buffs)).Length;
        int rand = UnityEngine.Random.Range(0,enumLen);

        return (Buffs) rand;
    }

    /// <summary>
    /// Aleatoriza um debuff
    /// </summary>
    /// <returns>debuff</returns>
    public Debuffs RandomizeDebuff(){
        int enumLen = Enum.GetNames(typeof(Debuffs)).Length;
        int rand = UnityEngine.Random.Range(0,enumLen);

        return (Debuffs) rand;
    }

    /// <summary>
    /// Gera strings dos botões
    /// </summary>
    /// <param name="buff">buff que ocorre quando o botão é pressionado</param>
    /// <param name="debuff">debuff que ocorre quando o botão é pressionado</param>
    /// <returns></returns>
    public String GenerateButtonText(Buffs buff, Debuffs debuff){
        string buffStr, debuffStr;

        switch(buff){
            case Buffs.moreLife:
                buffStr = "Increase your life";
                break;
            case Buffs.moreSpeed:
                buffStr = "Increase your speed";
                break;
            default:
                Debug.LogError("Buff inválido");
                buffStr = "";
                break;
        }

        switch(debuff){
            case Debuffs.lessLife:
                debuffStr = "decrease your life";
                break;
            case Debuffs.lessSpeed:
                debuffStr = "decrease your speed";
                break;
            case Debuffs.newEnemy:
                debuffStr = "a new enemy appears";
                break;
            default:
                Debug.LogError("Debuff inválido");
                debuffStr = "";
                break;           
        }

        return buffStr + " but " + debuffStr;
    }

    /// <summary>
    /// Gera todos os efeitos dos botões
    /// </summary>
    public void GenerateChoices(){
        //define buffs e debuffs
        foreach(Button btn in choiceButtons){
            //regera buffs e debuffs até que eles não sejam incompatíveis
            while(true){
                Buffs buff = RandomizeBuff();
                Debuffs debuff = RandomizeDebuff();

                //checa se buff e debuff são incompatíveis
                if(!(
                    (buff == Buffs.moreLife && debuff == Debuffs.lessLife) ||
                    (buff == Buffs.moreSpeed && debuff == Debuffs.lessSpeed)
                )){
                    //atualiza dicionários de efeitos
                    choiceBuffs.Add(btn, buff);
                    choiceDebuffs.Add(btn, debuff);

                    btn.transform.GetComponentInChildren<Text>().text = GenerateButtonText(buff, debuff); //gera texto do botão
                    break; //sai do while
                }
            }
        }
    }

    /// <summary>
    /// Aplica o efeito do buff
    /// </summary>
    /// <param name="effect">buff</param>
    public void ApplyBuff(Buffs effect){
        switch(effect){
            case Buffs.moreLife:
                Debug.Log("TODO: aumentar vida do player");
                break;
            case Buffs.moreSpeed:
                Debug.Log("TODO: aumentar velocidade do player");
                break;
            default:
                Debug.LogError("Buff inválido");
                break;
        }
    }

    /// <summary>
    /// Aplica o efeito do debuff
    /// </summary>
    /// <param name="effect">debuff</param>
    public void ApplyDebuff(Debuffs effect){
        switch(effect){
            case Debuffs.lessLife:
                Debug.Log("TODO: reduzir vida do player");
                break;
            case Debuffs.lessSpeed:
                Debug.Log("TODO: reduzir velocidade do player");
                break;
            case Debuffs.newEnemy:
                Debug.Log("TODO: acrescentar novo inimigo");
                break;
            default:
                Debug.LogError("Debuff inválido");
                break;
        }
    }

    /// <summary>
    /// OnClick do botão escolhido
    /// </summary>
    /// <param name="btn">referencia para o botão</param>
    public void Choose(Button btn){
        ApplyBuff(choiceBuffs[btn]);
        ApplyDebuff(choiceDebuffs[btn]);

        GameManager.instance.SpawnNextBoss();

        transform.parent.gameObject.SetActive(false);
    }

    public void OnEnable(){
        GenerateChoices();
    }
}