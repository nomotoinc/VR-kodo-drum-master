using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class DrumSound : MonoBehaviourPunCallbacks
{
    //使う音のコンポ―ネント定義
    public AudioSource audio;

    [SerializeField]
    public AudioClip sound;
    [SerializeField]
    public AudioClip soundA;
    [SerializeField]
    public AudioClip soundB;
    // スタート関数

    void Start()
    {
        //音コンポーネント取得
        audio = gameObject.AddComponent<AudioSource>();
    }

    //アップデート関数
    void Update()
    {
       
    }

    //オブジェクトとオブジェクトの衝突時
     void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Stick_B" )
        {

            
            
            if (photonView.IsMine)
            {
                audio.PlayOneShot(soundB);
            }
            else
            {
                audio.PlayOneShot(sound);
            }
            

            //コリジョン衝突時にルーム内プレイヤー全員にメッセージを送信


            //メッセージ確認用コード
           /* RpcSendMessage("太鼓が鳴りました");
            photonView.RPC(nameof(RpcSendMessage), RpcTarget.All, "太鼓が鳴りました");
            */

            //プレイヤーBが太鼓を鳴らした場合、Bの音が鳴る
            RpcDrumSound();
            photonView.RPC(nameof(RpcDrumSound), RpcTarget.OthersBuffered);

        }
        else if(collision.gameObject.name == "Stick_A")
        {
            if (photonView.IsMine)
            {
                audio.PlayOneShot(soundA);
            }
            else
            {
                audio.PlayOneShot(sound);
            }

            //プレイヤーAが太鼓を鳴らした場合、Aの音が鳴る
            RpcDrumSound();
            photonView.RPC(nameof(RpcDrumSound), RpcTarget.OthersBuffered);

        }
        else if(collision.gameObject.name == "Sphere")
        {
            //太鼓のテスト用、ボールを落とすと太鼓音が鳴る(普段はボールは非表示)
            audio.PlayOneShot(sound);

    
        }

        
       

    }



    //テスト用コード、太鼓とバチが当たるとコンソールに出力
    /*[PunRPC]
    private void RpcSendMessage(string message)
    {
        Debug.Log(message);
    }
    */


    //太鼓音が鳴るメソッド
    [PunRPC]
    private void RpcDrumSound()
    {
        audio.PlayOneShot(sound);
    }
}
