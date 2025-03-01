using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BallController : MonoBehaviour
{

  GameObject panelSelesai;

  Text txPemenang;
  public int force;
  Rigidbody2D rigid;
  int scoreP1;
  int scoreP2;
  TMP_Text scoreUIP1;
  TMP_Text scoreUIP2;

  // Use this for initialization 
  void Start()
  {
    panelSelesai = GameObject.Find("PanelSelesai");
    panelSelesai.SetActive(false);

    rigid = GetComponent<Rigidbody2D>();
    Vector2 arah = new Vector2(2, 0).normalized;
    rigid.AddForce(arah * force);
    scoreP1 = 0;
    scoreP2 = 0;
    scoreUIP1 = GameObject.Find("score1").GetComponent<TMP_Text>();
    scoreUIP2 = GameObject.Find("score2").GetComponent<TMP_Text>();

  }
  // Update is called once per frame 
  void Update()
  {
  }
  private void OnCollisionEnter2D(Collision2D coll)
  {
    if (coll.gameObject.name == "TepiKanan")
    {
      scoreP1 += 1;
      TampilkanScore();
      if (scoreP1 == 5)
      {
        panelSelesai.SetActive(true);
        txPemenang = GameObject.Find("Pemenang").GetComponent<Text>();
        txPemenang.text = "Player Kanan Wins!";
        Destroy(gameObject);
        return;
      }
      ResetBall();
      Vector2 arah = new Vector2(2, 0).normalized;
      rigid.AddForce(arah * force);
    }
    if (coll.gameObject.name == "TepiKiri")
    {
      scoreP2 += 1;
      TampilkanScore();
      if (scoreP2 == 5)
      {
        panelSelesai.SetActive(true);
        txPemenang = GameObject.Find("Pemenang").GetComponent<Text>();
        txPemenang.text = "Player kiri Wins!";
        Destroy(gameObject);
        return;
      }
      ResetBall();
      Vector2 arah = new Vector2(-2, 0).normalized;
      rigid.AddForce(arah * force);
    }
    if (coll.gameObject.name == "player" || coll.gameObject.name == "bot")
    {
      float sudut = (transform.position.y - coll.transform.position.y) * 5f;
      Vector2 arah = new Vector2(rigid.velocity.x, sudut).normalized;
      rigid.velocity = new Vector2(0, 0);
      rigid.AddForce(arah * force * 2);
    }
  }
  void ResetBall()
  {
    transform.localPosition = new Vector2(0, 0);
    rigid.velocity = new Vector2(0, 0);
  }
  void TampilkanScore()
  {
    Debug.Log("Score P1: " + scoreP1 + " Score P2: " + scoreP2);
    scoreUIP1.text = scoreP1 + "";
    scoreUIP2.text = scoreP2 + "";
  }
}