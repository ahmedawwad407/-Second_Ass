
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class GameManager : MonoBehaviour {


    public static GameManager instance;

    public int scoreValue;

    [SerializeField]
    private Text textScore;

    [SerializeField]
    private Image imageCurrentHealthBar;

    [SerializeField]
    private Animator deathAnimator;

    public GameObject mySystem;

    public moveCharacter myPlayer;

    private void Awake() {
        if (instance != null) {
            Destroy(gameObject);
            Destroy(mySystem.gameObject);
            return;
        }
        instance = this;
        SceneManager.sceneLoaded += GetSavedState;
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void Update() {

        ApplyChange();

    }


    public void ChangeScoreValue(int _score) {
        SaveState();
        scoreValue += _score;
    }

    void ApplyChange() {

        textScore.text = scoreValue.ToString();
        imageCurrentHealthBar.transform.localScale = new Vector2((float)myPlayer.currentHealth / myPlayer.maxHealth, 1f);
    }

    /*        public void Die(string _playerTagName){
        *//*       GameObject.FindGameObjectWithTag(_playerTagName).transform.position  =
                GameObject.Find("StartPoint").transform.position ;*//*

            myPlayer.gameObject.transform.position = GameObject.Find("StartPoint").transform.position;
            deathAnimator.SetTrigger("Clicked");
        }*/

    public void Die() {
        myPlayer.gameObject.transform.position = GameObject.Find("StartPoint").transform.position;
        deathAnimator.SetTrigger("Clicked");
    }


    public void PlayerGotDamged(int damage) {
        myPlayer.currentHealth -= damage;
        //imageCurrentHealthBar.transform.localScale = new Vector2((float)myPlayer.currentHealth/myPlayer.maxHealth, 1f);

        if (myPlayer.currentHealth <= 0) {
            Die();
            myPlayer.currentHealth = myPlayer.maxHealth;
        }


    }

    public void DeleteProgress() {

        PlayerPrefs.DeleteAll();
        myPlayer.currentHealth = myPlayer.maxHealth;
        scoreValue = 0;
        SceneManager.LoadScene(0);
    }
    public void SaveState()
    {
        string s = myPlayer.tag.ToString() + '|';
        s += myPlayer.currentHealth.ToString() + '|';
        s += scoreValue.ToString() + '|';
        s += SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("SaveState", s);

    }

    private void GetSavedState(Scene scene, LoadSceneMode mode) {
        SceneManager.sceneLoaded -= GetSavedState;
        if (!PlayerPrefs.HasKey("SaveState"))
            return;

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');
        myPlayer.currentHealth = int.Parse(data[1]);
        scoreValue = int.Parse(data[2]);
        SceneManager.LoadScene(data[3]);
    }


    private void OnSceneLoad(Scene scene, LoadSceneMode mode){

        myPlayer.transform.position = GameObject.Find("StartPoint").transform.position;
    }

}

