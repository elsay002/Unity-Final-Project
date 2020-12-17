using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//public enum BattleState {START, PLAYERTURN, ENEMYTURN, WON, LOST}

public class BossBattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    Unit playerUnit;
    Unit enemyUnit;
    GameObject playerGO;
    GameObject enemyGO;

    public Text dialogueText;

    public BattleState state;
    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public AudioSource BattleMusic;
    public AudioSource Fanfare;

    public GameObject BulletObj;
    public Transform BulletSpawn;

    // Start is called before the first frame update
    void Start()
    {

        state = BattleState.START;
        BattleMusic.Play();
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();

        enemyGO=Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();

        dialogueText.text= "Defeat " + enemyUnit.unitName;

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }
    
    IEnumerator PlayerAttack()
    {
        //Damage 
        state= BattleState.ENEMYTURN;
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        dialogueText.text = "You attack the enemy!";
        Fire();
        yield return new WaitForSeconds(1f);

        enemyHUD.SetHP(enemyUnit);

        dialogueText.text = "The attack hits!";

        yield return new WaitForSeconds(2f);
        
        //Change if enemy is dead
        //change state based on what happened
        
        if(isDead)
        {
            //end battle
            state = BattleState.WON;
            Destroy(enemyGO);
            StartCoroutine(EndBattle());
        }else
        {
            //enemy turn
            state= BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
        
    }

    IEnumerator EnemyTurn()
    {
        
        dialogueText.text = enemyUnit.unitName + "'s turn!";


        yield return new WaitForSeconds(2f);
        
        dialogueText.text = enemyUnit.unitName + " attacks!";

        yield return new WaitForSeconds(1f);
        bool isDead =playerUnit.TakeDamage(enemyUnit.damage);

        playerHUD.SetHP(playerUnit);

        yield return new WaitForSeconds(1f);

        if(isDead)
        {
            state = BattleState.LOST;
            StartCoroutine(EndBattle());
        }
        else
        {
            state= BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    IEnumerator EndBattle()
    {
        if(state == BattleState.WON)
        {
            BattleMusic.Stop();
            Fanfare.Play();

            dialogueText.text = "You've won!!";
            yield return new WaitForSeconds(5f);

            dialogueText.text = "Gained 50XP";

            yield return new WaitForSeconds(5);

            //string returnScene = PlayerPrefs.GetString("lastScene");

            //SceneManager.LoadScene(returnScene);

            SceneManager.LoadScene("EndScreen");

        }else if(state == BattleState.LOST)
        {

            dialogueText.text = "You've been defeated!";
            Destroy(playerGO);
            yield return new WaitForSeconds(3);
            SceneManager.LoadScene("BossBattle");
        }
    }


    void PlayerTurn()
    {
        dialogueText.text= "Choose an action:";

        // OnGUI();
    }

    public void OnAttackButton()
    {
        if(state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack());
    }

    public void OnHealButton()
    {
        if(state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerHeal());
    }


   /* void OnGUI()
    {
        if (GUI.Button(new Rect(427,287,72,65), "Attack"))
        {
            StartCoroutine(PlayerAttack());
        }
        if(GUI.Button(new Rect(520,287,72,65), "Heal"))
        {
            StartCoroutine(PlayerHeal());
        }
    }*/

    IEnumerator PlayerHeal()
    {
        state = BattleState.ENEMYTURN;

        playerUnit.Heal(12);
        playerHUD.SetHP(playerUnit);
        dialogueText.text = "You recovered HP";

        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    void Fire()
    {
      var Bullet = (GameObject)Instantiate(BulletObj, BulletSpawn.position, BulletSpawn.rotation); 
      Bullet.GetComponent<Rigidbody>().velocity = Bullet.transform.forward * 4; 
      Destroy(Bullet,1.0f);  
    }
}

