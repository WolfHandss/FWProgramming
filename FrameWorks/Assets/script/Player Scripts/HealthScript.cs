//Programmer :  Phil
//Capstone 2019
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealthScript : MonoBehaviour {

    private EnemyAnimator enemy_Anim;
    private NavMeshAgent navAgent;
    private EnemyController enemy_Controller;

    public float health = 100f;

    public bool is_Player, is_Enemy1, is_Enemy2;

    private bool is_Dead;

    private EnemyAudio enemyAudio;

    private PlayerStats player_Stats;

	void Awake () {
	    
        if(is_Enemy1 || is_Enemy2) {
            enemy_Anim = GetComponent<EnemyAnimator>();
            enemy_Controller = GetComponent<EnemyController>();
            navAgent = GetComponent<NavMeshAgent>();

            // get enemy audio
            enemyAudio = GetComponentInChildren<EnemyAudio>();
        }

        if(is_Player) {
            player_Stats = GetComponent<PlayerStats>();
        }

	}
	
    public void ApplyDamage(float damage) {

        // if we died don't execute the rest of the code
        if (is_Dead)
            return;

        health -= damage;

        if(is_Player) {
            // show the stats(display the health UI value)
            player_Stats.Display_HealthStats(health);
        }

        if(is_Enemy1 || is_Enemy2) {
            if(enemy_Controller.Enemy_State == EnemyState.PATROL) {
                enemy_Controller.chase_Distance = 50f;
            }
        }

        if(health <= 0f) {

            PlayerDied();

            is_Dead = true;
        }

    } // apply damage

    void PlayerDied() {

        if(is_Enemy2) {

            GetComponent<Animator>().enabled = false;
            GetComponent<BoxCollider>().isTrigger = false;
            GetComponent<Rigidbody>().AddTorque(-transform.forward * 5f);

            enemy_Controller.enabled = false;
            navAgent.enabled = false;
            enemy_Anim.enabled = false;

            Debug.Log("Enemy 2 Died!!");
            StartCoroutine(DeadSound());

            // EnemyManager spawn more enemies
            EnemyManager.instance.EnemyDied(true);
        }

        if(is_Enemy1) {

            navAgent.velocity = Vector3.zero;
            navAgent.isStopped = true;
            enemy_Controller.enabled = false;

            enemy_Anim.Dead();
            Debug.Log("Enemy 1 Died!!");
            StartCoroutine(DeadSound());

            // EnemyManager spawn more enemies
            EnemyManager.instance.EnemyDied(false);
        }

        if(is_Player) {

            GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.ENEMY_TAG);

            for (int i = 0; i < enemies.Length; i++) {
                enemies[i].GetComponent<EnemyController>().enabled = false;
            }

            // call enemy manager to stop spawning enemis
            EnemyManager.instance.StopSpawning();

            Debug.Log("Player Died!!");

        }

        if(tag == Tags.PLAYER_TAG) {

            Invoke("RestartGame", 3f);

        } else {

            Invoke("TurnOffGameObject", 3f);

        }

    } // player died

    void RestartGame() {
        // UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");

        Debug.Log("Future Coding - for the SceneManager.LoadScene .");
        Debug.Log("Restart Game!!");
    }

    void TurnOffGameObject() {
        gameObject.SetActive(false);
    }

    IEnumerator DeadSound() {
        yield return new WaitForSeconds(0.3f);
        enemyAudio.Play_DeadSound();
    }

} // class









































