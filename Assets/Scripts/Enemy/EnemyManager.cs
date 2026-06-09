using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private Spawner spawn1;
    [SerializeField] private Spawner spawn2;
    [SerializeField] private Spawner spawn3;
    [SerializeField] private Spawner spawn4;
    [SerializeField] private Spawner spawn5;
    [SerializeField] private Spawner spawn6;
    [SerializeField] private Spawner spawn7;
    [SerializeField] private Spawner spawn8;
    [SerializeField] private Spawner spawn9;
    [SerializeField] private Spawner spawn10;
    //[SerializeField] private EnemyManager nextCombat;
    [SerializeField] private CampActivationTrigger nextCombatTrigger;
    [SerializeField] private CampActivationTrigger nextWallTrigger;
    [SerializeField] private int goblinsToKill;
    [SerializeField] private int coptersToKill;
    [SerializeField] private int mechasToKill;
    [SerializeField] private int initialGoblins;
    [SerializeField] private int initialCopters;
    [SerializeField] private int initialMechas;
    [SerializeField] private float goblinCoolDown;
    [SerializeField] private float copterCoolDown;
    //[SerializeField] private int mechaCoolDown;
    [SerializeField] private bool isActive;
    
    private int numberOfSpawns;
    private int spawnRotation;
    private int currentGoblins;
    private int currentCopters;
    private int currentMechas;
    private int goblinsKilled;
    private int coptersKilled;
    private int mechasKilled;
    private bool isSpawning;
    private bool isSpawningGoblin;
    private bool isSpawningCopter;
    private bool combatStarted;
    private bool goblinSpawnActivated;
    private bool copterSpawnActivated;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (spawn1 != null) numberOfSpawns ++;
        if (spawn2 != null) numberOfSpawns ++;
        if (spawn3 != null) numberOfSpawns ++;
        if (spawn4 != null) numberOfSpawns ++;
        if (spawn5 != null) numberOfSpawns ++;
        if (spawn6 != null) numberOfSpawns ++;
        if (spawn7 != null) numberOfSpawns ++;
        if (spawn8 != null) numberOfSpawns ++;
        if (spawn9 != null) numberOfSpawns ++;
        if (spawn10 != null) numberOfSpawns ++;
    }

    // Update is called once per frame
    void Update()
    {
        //UnityEngine.Debug.Log(isSpawning);
        if ( isActive && goblinsKilled >= goblinsToKill && coptersKilled >= coptersToKill && mechasKilled >= mechasToKill)
        {
            isActive = false;
            if(nextCombatTrigger != null)nextCombatTrigger.ActivateTrigger();
            if(nextWallTrigger != null)nextWallTrigger.ActivateTrigger();
        }
        else if (isActive && !isSpawning && combatStarted)
        {
            if (currentGoblins < initialGoblins && goblinsToKill > goblinsKilled && !isSpawningGoblin) 
            {
                isSpawningGoblin = true;
                SpawnActivartor();
            }
            if (currentCopters < initialCopters && coptersToKill > coptersKilled && !isSpawningCopter) 
            {
                isSpawningCopter = true;
                SpawnActivartor();
            }
        } 
        else if (isActive && !isSpawning && !combatStarted)
        {
            combatStarted = true;
            isSpawning = true;
            CombatStart();
        }
    }
    private void CombatStart()
    {
        for(int i = 0; i < initialGoblins; i++)
        {
            if(spawnRotation==0) spawnRotation = numberOfSpawns;
            switch(spawnRotation)
            {
                case 1:
                spawn1.SpawnGoblin();
                spawnRotation-=1;
                break;
                case 2:
                spawn2.SpawnGoblin();
                spawnRotation-=1;
                break;
                case 3:
                spawn3.SpawnGoblin();
                spawnRotation-=1;
                break;
                case 4:
                spawn4.SpawnGoblin();
                spawnRotation-=1;
                break;
                case 5:
                spawn5.SpawnGoblin();
                spawnRotation-=1;
                break;
                case 6:
                spawn6.SpawnGoblin();
                spawnRotation-=1;
                break;
                case 7:
                spawn7.SpawnGoblin();
                spawnRotation-=1;
                break;
                case 8:
                spawn8.SpawnGoblin();
                spawnRotation-=1;
                break;
                case 9:
                spawn9.SpawnGoblin();
                spawnRotation-=1;
                break;
                case 10:
                spawn10.SpawnGoblin();
                spawnRotation-=1;
                break;
            }
        }
        for(int i = 0; i < initialCopters; i++)
        {
            if(spawnRotation==0) spawnRotation = numberOfSpawns;
            switch(spawnRotation)
            {
                case 1:
                spawn1.SpawnGoblinCopter();
                spawnRotation-=1;
                break;
                case 2:
                spawn2.SpawnGoblinCopter();
                spawnRotation-=1;
                break;
                case 3:
                spawn3.SpawnGoblinCopter();
                spawnRotation-=1;
                break;
                case 4:
                spawn4.SpawnGoblinCopter();
                spawnRotation-=1;
                break;
                case 5:
                spawn5.SpawnGoblinCopter();
                spawnRotation-=1;
                break;
                case 6:
                spawn6.SpawnGoblinCopter();
                spawnRotation-=1;
                break;
                case 7:
                spawn7.SpawnGoblinCopter();
                spawnRotation-=1;
                break;
                case 8:
                spawn8.SpawnGoblinCopter();
                spawnRotation-=1;
                break;
                case 9:
                spawn9.SpawnGoblinCopter();
                spawnRotation-=1;
                break;
                case 10:
                spawn10.SpawnGoblinCopter();
                spawnRotation-=1;
                break;
            }
        }
        isSpawning = false;
    }
    private void SpawnActivartor()
    {
        if (currentGoblins < initialGoblins && goblinsToKill > goblinsKilled)
        {
            if(spawnRotation==0) spawnRotation = numberOfSpawns;
            switch(spawnRotation)
            {
                case 1:
                spawn1.SpawnGoblin();
                spawnRotation-=1;
                break;
                case 2:
                spawn2.SpawnGoblin();
                spawnRotation-=1;
                break;
                case 3:
                spawn3.SpawnGoblin();
                spawnRotation-=1;
                break;
                case 4:
                spawn4.SpawnGoblin();
                spawnRotation-=1;
                break;
                case 5:
                spawn5.SpawnGoblin();
                spawnRotation-=1;
                break;
                case 6:
                spawn6.SpawnGoblin();
                spawnRotation-=1;
                break;
                case 7:
                spawn7.SpawnGoblin();
                spawnRotation-=1;
                break;
                case 8:
                spawn8.SpawnGoblin();
                spawnRotation-=1;
                break;
                case 9:
                spawn9.SpawnGoblin();
                spawnRotation-=1;
                break;
                case 10:
                spawn10.SpawnGoblin();
                spawnRotation-=1;
                break;
            }
            goblinSpawnActivated = true;
        }
        if (currentCopters < initialCopters && coptersToKill > coptersKilled)
        {
            if(spawnRotation==0) spawnRotation = numberOfSpawns;
            switch(spawnRotation)
            {
                case 1:
                spawn1.SpawnGoblinCopter();
                spawnRotation-=1;
                break;
                case 2:
                spawn2.SpawnGoblinCopter();
                spawnRotation-=1;
                break;
                case 3:
                spawn3.SpawnGoblinCopter();
                spawnRotation-=1;
                break;
                case 4:
                spawn4.SpawnGoblinCopter();
                spawnRotation-=1;
                break;
                case 5:
                spawn5.SpawnGoblinCopter();
                spawnRotation-=1;
                break;
                case 6:
                spawn6.SpawnGoblinCopter();
                spawnRotation-=1;
                break;
                case 7:
                spawn7.SpawnGoblinCopter();
                spawnRotation-=1;
                break;
                case 8:
                spawn8.SpawnGoblinCopter();
                spawnRotation-=1;
                break;
                case 9:
                spawn9.SpawnGoblinCopter();
                spawnRotation-=1;
                break;
                case 10:
                spawn10.SpawnGoblinCopter();
                spawnRotation-=1;
                break;
            }
            copterSpawnActivated = true;
        }
        if (goblinSpawnActivated)
        {
            goblinSpawnActivated = false;
            StartCoroutine(SpawnCooldownSequence(goblinCoolDown));
        }
        else if (copterSpawnActivated)
        {
            copterSpawnActivated = false;
            StartCoroutine(SpawnCooldownSequence(copterCoolDown));
        }
    }
    private IEnumerator SpawnCooldownSequence(float coolDown)
    {
        yield return new WaitForSeconds(coolDown);
        if (coolDown == goblinCoolDown) isSpawningGoblin = false;
        else if (coolDown == copterCoolDown) isSpawningCopter = false;
    }
    public void GoblinSpawned()
    {
        currentGoblins++;
    }
    public void GoblinDead()
    {
        currentGoblins-=1;
        goblinsKilled++;
    }
    public void CopterSpawned()
    {
        currentCopters++;
    }
    public void CopterDead()
    {
        currentCopters--;
        coptersKilled++;
    }
    public void MechaDead()
    {
        currentMechas--;
        mechasKilled++;
    }
    public void ActivateCamp()
    {
        isActive = true;
    }
}
