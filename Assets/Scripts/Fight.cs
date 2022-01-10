using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Fight : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject playerStats, opponentStats, opponentTextDamage, playerTextDamage;
    public GameObject sword, hax, player, opponent, initialOpponent, openWorldHpBar, fightHpBar, ui, card1, card2, blackBackground,cloak, won;
    public Text bonusCard1, bonusCard2;
    public Sprite[] spriteCards;
    public XpBar xpBar;
    public bool cardIsSelected = false;
    private readonly string[] textes={ "You drank too much again, wake up stupid !", "You went too far this time, i won't hesitate to kill you dirty drunk !","I had told the tavern keeper that he shouldn't let you in ! " };
    public GameObject message;
    public Text messageText;
    public Sprite bossSprite;

    private void Start()
    {
        xpBar.SetScale(0);
     


    }
    

    private void Awake()
    {


    }

    // Update is called once per frame
    private void Update()
    {

    }

    public IEnumerator ToFight()
    {
        
       opponent.SetActive(false);
        var swordAnimator = sword.GetComponent<Animator>();
        var haxAnimator = hax.GetComponent<Animator>();
        var playerTextDamageAnimator = playerTextDamage.GetComponent<Animator>();
        var opponentTextDamageAnimator = opponentTextDamage.GetComponent<Animator>();
        opponentTextDamageAnimator.GetComponent<Text>().text = " -" + player.GetComponent<Player>().attack.ToString();
        var playerScript = player.GetComponent<Player>();
        var opponentScript = opponent.GetComponent<Fighter>();
        var fightHpBarScript = fightHpBar.GetComponent<HpBar>();
        var openWorldHpBarScript = openWorldHpBar.GetComponent<HpBar>();
        var playerStatsText = playerStats.GetComponent<Text>();
        var opponentStatsText = opponentStats.GetComponent<Text>();
        void UpdatePlayerStatsText()
        {
            playerStatsText.text = "Hp : " + playerScript.hp + "/" + playerScript.maxHp + "\n";
            playerStatsText.text += "Attack : " + playerScript.attack + "\n";
            playerStatsText.text += "Crit Rate : " + playerScript.critRate + "\n";
        }

        void UpdateOpponentStatsText()
        {
            opponentStatsText.text = "Hp : " + opponentScript.hp + "/" + opponentScript.maxHp + "\n";
            opponentStatsText.text += "Attack : " + opponentScript.attack + "\n";
            opponentStatsText.text += "Crit Rate : " + opponentScript.critRate + "\n";
        }

        UpdatePlayerStatsText();
        UpdateOpponentStatsText();

        fightHpBarScript.SetScale((float) playerScript.hp / playerScript.maxHp);
        playerScript.hpBar = fightHpBarScript;
        playerScript.SetHp(playerScript.hp);
        opponentScript.SetHp(opponentScript.hp);
        int proba = Random.Range(0, 10);
        if (proba <= 2)
        {
         
            messageText.text = textes[proba];
            message.SetActive(true);
            yield return new WaitForSeconds(3f);
            message.SetActive(false);
        }
    
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            var attack = playerScript.Attack();
            swordAnimator.Play("sword");
            yield return new WaitForSeconds(0.5f);
            opponentTextDamageAnimator.GetComponent<Text>().text = " -" + attack;
            opponentTextDamageAnimator.Play("opponent_text_damage");
            opponentScript.SetHp(opponentScript.hp - attack);
            UpdateOpponentStatsText();

            if (opponentScript.hp <= 0) break;
            yield return new WaitForSeconds(0.1f);
            attack = opponentScript.Attack();
            haxAnimator.Play("hax");
            yield return new WaitForSeconds(0.5f);
            playerTextDamageAnimator.GetComponent<Text>().text = " -" + attack;
            playerTextDamageAnimator.Play("player_text_damage");
            playerScript.SetHp(playerScript.hp - attack);
            UpdatePlayerStatsText();

            if (playerScript.hp > 0) continue;
            yield return new WaitForSeconds(1.3f);
            GameObject.Find("SceneController").GetComponent<SceneController>().Die();
       
            
            yield break;

        }
        
        var nbXpGain = (float)opponentScript.level / playerScript.level*10;

        int nbLevel = ((int)nbXpGain + playerScript.xp) / 100;


        void PrintCards()
        {
            blackBackground.SetActive(true);
            cardIsSelected = false;
            int rdm1 = Random.Range(0, 6), rdm2 = Random.Range(0, 6);
            while (rdm2 == rdm1) rdm2 = Random.Range(0, 5);
            card1.GetComponent<SpriteRenderer>().sprite = spriteCards[rdm1];
            card2.GetComponent<SpriteRenderer>().sprite = spriteCards[rdm2];
            card1.SetActive(true);
            card2.SetActive(true);


            float GetBonus(int idx)
            {
                var wisdomBonus = 1 + (playerScript.wisdom) / 100f;
                switch (idx)
                {
                    case 0:
                        return (int) (Random.Range(1, 3) * wisdomBonus);
                    case 1:
                        return (float) Decimal.Round((decimal) (Random.Range(0.003f, 0.006f) * wisdomBonus), 3);
                    case 2:
                        return (int) (Random.Range(5, 10) * wisdomBonus);
                    case 3:
                        return (int) (wisdomBonus);
                    case 4:
                        return (float) Decimal.Round((decimal) (Random.Range(0.01f, 0.03f) * wisdomBonus), 2);
                    case 5:
                        return (int) (Random.Range(1, 3) * wisdomBonus);
                }

                return 0;
            }
            var bonus1 = GetBonus(rdm1);
            var bonus2 = GetBonus(rdm2);
            card1.GetComponent<Card>().bonusValue = bonus1;
            card2.GetComponent<Card>().bonusValue = bonus2;
            card1.GetComponent<Card>().cardIdx = rdm1;
            card2.GetComponent<Card>().cardIdx = rdm2;
            bonusCard1.text = "+ " + bonus1;
            bonusCard2.text = "+ " + bonus2;
        }

        void UnprintCards()
        {

            blackBackground.SetActive(false);
            card1.SetActive(false);
            card2.SetActive(false);
            bonusCard1.text = "";
            bonusCard2.text = "";
        }

        while (nbLevel-- > 0)
        {
            StartCoroutine(xpBar.setXpAnimating(100));
            yield return new WaitForSeconds(2f);
            PrintCards();
            while (!cardIsSelected) yield return new WaitForSeconds(0.1f);
            UnprintCards();
            playerScript.level++;
        }

        playerScript.xp = ((int)nbXpGain + playerScript.xp) % 100;
        StartCoroutine(xpBar.setXpAnimating(playerScript.xp));
        openWorldHpBarScript.SetScale((float) playerScript.hp / playerScript.maxHp);
        playerScript.hpBar = openWorldHpBar.GetComponent<HpBar>();
        playerScript.SetHp(playerScript.hp);
        yield return new WaitForSeconds(1.3f);
        playerScript.textHp.text="Lvl " + playerScript.level;
        if (playerScript.level >= 100)
        {
            opponent = initialOpponent;
             
                opponent.gameObject.transform.Translate(5,0,0);

            playerScript.hpBar = fightHpBarScript;
            playerScript.SetHp(playerScript.maxHp);
            ui.SetActive(false);
            opponent.gameObject.GetComponent<SpriteRenderer>().sprite = bossSprite;

            for (int i = 0; i < 100; i++)
            {
                opponent.transform.Translate(-5/100f,0,0);
                yield return new WaitForSeconds(0.02f);
            }

            messageText.text = "You just killed all my clients you junk, you're gonna pay for it with your life !";
            message.SetActive(true);
            yield return new WaitForSeconds(5f);
            message.SetActive(false);
            yield return new WaitForSeconds(1f);
            
            ui.SetActive(true);
            xpBar.gameObject.SetActive(false);
            opponentScript.maxHp = 300;
            opponentScript.SetHp(300);
            
            opponentScript.attack = 30;
            opponentScript.critRate = 0.33f;

            UpdatePlayerStatsText();
            UpdateOpponentStatsText();
            while (true)
            {
                yield return new WaitForSeconds(0.1f);
                var attack = playerScript.Attack();
                swordAnimator.Play("sword");
                yield return new WaitForSeconds(0.5f);
                opponentTextDamageAnimator.GetComponent<Text>().text = " -" + attack;
                opponentTextDamageAnimator.Play("opponent_text_damage");
                opponentScript.SetHp(opponentScript.hp - attack);
                UpdateOpponentStatsText();

                if (opponentScript.hp <= 0) break;
                yield return new WaitForSeconds(0.1f);
                attack = opponentScript.Attack();
                haxAnimator.Play("hax");
                yield return new WaitForSeconds(0.5f);
                playerTextDamageAnimator.GetComponent<Text>().text = " -" + attack;
                playerTextDamageAnimator.Play("player_text_damage");
                playerScript.SetHp(playerScript.hp - attack);
                UpdatePlayerStatsText();

                if (playerScript.hp > 0) continue;
                yield return new WaitForSeconds(1.3f);
                GameObject.Find("SceneController").GetComponent<SceneController>().Die();
                yield break;

            }
            yield return new WaitForSeconds(1f);
            ui.SetActive(false);
            opponent.SetActive(false);
            GameObject.Find("Player_fight").SetActive(false);
            won.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            cloak.GetComponent<Cloak>().isFrozen = true;
            yield return new WaitForSeconds(6);
            SceneManager.LoadScene("Main");

        }
        else
            GameObject.Find("SceneController").GetComponent<SceneController>().EndFight(opponent);
    }

    public void SetBonus(int idx, float value)
    {
        var playerScript = player.GetComponent<Player>();
        switch (idx)
        {
            case 0:
                playerScript.attack += (int) value;
                break;
            case 1:
                playerScript.critRate += value;
                break;
            case 2:
                playerScript.hp += (int) value;
                playerScript.maxHp += (int) value;
                break;
            case 3:
                playerScript.regen += (int) value;
                break;
            case 4:
                playerScript.velocity += value;
                break;
            case 5:
                playerScript.wisdom += (int) value;
                break;
        }


    }
    
    
}
