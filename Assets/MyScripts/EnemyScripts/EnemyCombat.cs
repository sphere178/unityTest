using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using static EnemyCombat;

public class EnemyCombat : CharacterCombatSystemBase
{
    [System.Serializable]
    public class SkillInfo
    {
        public CharacterSkillSystemBase skillBaseInfo;
        public bool cdCondition;
        public bool distanceCondition;
        public SkillInfo(CharacterSkillSystemBase _skill, bool _cdcondiiton, bool _distancecondition)
        {
            skillBaseInfo = _skill;
            cdCondition = _cdcondiiton;
            distanceCondition = _distancecondition;

        }

    }
    [SerializeField] public SkillInfo[] skillInfos;
    [SerializeField] private CharacterSkillSystemBase[] skills;
    [SerializeField] private int currentSkillID;
    [SerializeField] private bool isUsingSkill;
    private EnemyMovement enemyMovement;
    [SerializeField] private Dictionary<int, SkillInfo> skillConditionDictionary = new Dictionary<int, SkillInfo>();

   
    protected override void Awake()
    {
        base.Awake();
        enemyMovement = GetComponentInParent<EnemyMovement>();
        
        

    }
    void Start()
    {
        InitSkillInfo();
        InitSkillDictionary();
        InitAllSkillCD();
    }

    
    void Update()
    {
        AIView();
        LockOnCurrentTargetEnemy();
        AIAttack();
        UpdateUsingSkill();


    }

    private void AIView()
    {
        DetectEnemy();
        FindCurrentTargetEnemy();
    }

    private void AIAttack()
    {
        UseSkill();
    }

    private void UseSkill()
    {
        if (currentSkillID == 0)
        {
            
            currentSkillID = GetAvailableSkill();
        }
       if(currentSkillID != 0)
        {
            if (!isUsingSkill)
            {
                SkillInfo currentSkill = GetSkillConditionByID(currentSkillID);
                if (currentSkill.distanceCondition)
                {
                    
                    PlaySkillAnimation();
                    InitSkillCD(currentSkill.skillBaseInfo.skillID);
                    currentSkill.distanceCondition = false;
                    currentSkill.cdCondition = false;
                    currentSkillID = 0;
                }
                else
                {
                    
                    enemyMovement.CloseToEnemyAndMeetUseDistance(currentTargetEnemy,currentSkill.skillBaseInfo.useDistance,currentSkillID);
                }
            }
            
        }
    }

    private void UpdateUsingSkill()
    {
        if (animator.CheckAnimationTag("Attack"))
        {
            isUsingSkill = true;
        }
        else
        {
            isUsingSkill = false;
        }
    }
    private int GetAvailableSkill()
    {
        int currentAvailableSkillID =0;
        int currentPriority=0;
        
        foreach( var skill in skillInfos)
        {
            if (skill.cdCondition&&skill.skillBaseInfo.priority>currentPriority)
            {
                currentPriority = skill.skillBaseInfo.priority;
                currentAvailableSkillID = skill.skillBaseInfo.skillID;
               
            }
        }
        return currentAvailableSkillID;
    }

    private void PlaySkillAnimation()
    {
        animator.Play(GetSkillConditionByID(currentSkillID).skillBaseInfo.skillAnimationName, 0, 0);
    }

    

    private void SetisUseSkillFalse()
    {
        isUsingSkill = false;
    }

    private void InitAllSkillCD()
    {
        for (int i = 0; i < skillInfos.Length; i++)
        {
            InitSkillCD(skillInfos[i].skillBaseInfo.skillID);
        }
    }

    private void InitSkillDictionary()
    {
        for (int i = 0; i < skillInfos.Length; i++)
        {
            skillConditionDictionary.Add(skillInfos[i].skillBaseInfo.skillID, skillInfos[i]);
        }

    }

    private void InitSkillCD(int _skillID) //对象池取计时器，等cd转好
    {
        GameObjectPoolSystem.instance.TakeAvailableTimer().InitTimer(() =>
        {
            GetSkillConditionByID(_skillID).cdCondition = true;
        }, GetSkillConditionByID(_skillID).skillBaseInfo.skillCD);
    }

    public SkillInfo GetSkillConditionByID(int _skillID)
    {
       return skillConditionDictionary[_skillID];
    }

    private void InitSkillInfo()
    {
        skillInfos = new SkillInfo[skills.Length];
        for (int i = 0; i < skills.Length; i++)
        {
            skillInfos[i] = new SkillInfo(skills[i], false, false); // 初始化每个元素
        }
    }

}

