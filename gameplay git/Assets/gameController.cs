using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyDice.Board;
using MyDice.Players;
using TMPro;

public class gameController : MonoBehaviour
{
    public GameObject CurrentPlayer;

    
    public void CheckNodesType(ElementNode node)
    {
        ElementNodeCreator ENC = FindObjectOfType<ElementNodeCreator>();
        ENC.allowRolling = false;

        GameObject CPlayer = currentPlayerF();
        Player player = CPlayer.GetComponent<Player>();
        
        Canvas Canvas = FindObjectOfType<Canvas>();
        Canvas.enabled = false;

        
        
        
            

            if (node.NodeType == 1)
            {
                if (node.Owned)
                {
                    if (!CheckOwnership(CPlayer, node.Owner))
                    {
                        UpdateUI(node.PropertyName, node.PropertyPrice.ToString(), node.PayingPrice.ToString(), node.Owned, Canvas);
                        PayPlayer(node, player);
                    }
                    else
                    {
                        UpdateUI(node.PropertyName, node.PropertyPrice.ToString(), node.PayingPrice.ToString(), node.Owned, Canvas);
                    }
                }
                else
                {
                    UpdateUI(node.PropertyName, node.PropertyPrice.ToString(), node.PayingPrice.ToString(), node.Owned, Canvas); //Edw apla peirazw to ui gia na vgazei to swsto kathe fora
                }
            }
            else if (node.NodeType == 2)
            {
                //call order func|||   Edw thelei apla na ginei ena func pou na exei ena switch case gia kathe karta entolis kai antisoixa na exei i kathe karta (kathe case ) to analogo apotelesma
            }
            else if (node.NodeType == 3)
            {
                //call decision func\\\ Edw to idio me panw
            }
            else if (node.NodeType == 4)
            {
                //call fine func///   edw na diavazei to PropertyPrice toy ElementNode script kai na mionei to wallet tou currentPlayer oso einai to price
            }
            else if (node.NodeType == 5)
            {
                //call bonus func  ///      Edw to idio me panw apla na auksanei
            }
            else if (node.NodeType == 6)
            {
                //call insurance func    /// auto gia pio meta einai pio periploko
            }
            else if (node.NodeType == 7)
            {
                //call bill func /// auto episis pio meta
            }
            else if (node.NodeType == 8)
            {
                //call start func///  edw apla na dinei ta lefta tis afetirias alla kai na metraei girous diladi na kanei +1 kapoion metriti panw ston paikti kai na elenxei oti an einai megalitero toy 2 na stamataei to game.
            }
    }
    public bool CheckOwnership(GameObject player, GameObject OwnerPlayer)
    {
        if (player == OwnerPlayer)
        {
            return true;
        }
        else
        {
            return false;
        }
    }



    public void BuyProp()
    {
        GameObject cplayer = currentPlayerF();
        currentNode cnode = FindObjectOfType<currentNode>();
        ElementNode node = cnode.cNode;


        Player player = cplayer.GetComponent<Player>();
        player.Wallet = player.Wallet - node.PropertyPrice;
        node.Owned = true;
        node.Owner = player.gameObject;
        DisableBuyButton();
    }

    public void PayPlayer(ElementNode node, Player player)
    {
        GameObject playerReceive = node.Owner;
        
        Player playerR = playerReceive.GetComponent<Player>();
        player.Wallet = player.Wallet - node.PayingPrice;
        playerR.Wallet = playerR.Wallet + node.PayingPrice;
    }

    public void UpdateUI(string Title, string Price, string PricePaying, bool owned, Canvas canvas)
    {
        GameObject title = GameObject.Find("Title");
        Debug.Log(title);
        GameObject price = GameObject.Find("Price");
        GameObject buyB = GameObject.Find("BuyButton");
        TextMeshProUGUI titleText = title.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI priceText = price.GetComponent<TextMeshProUGUI>();

        titleText.text = Title;

        

        if (owned)
        {
            priceText.text = "Price: " + PricePaying + "$";
            buyB.SetActive(false);
            canvas.enabled = true;
        }
        else
        {
            priceText.text = "Price: " + Price + "$";
            buyB.SetActive(true);
            canvas.enabled = true;
        }
    }
    public void DisableBuyButton()
    {
        GameObject buyB = GameObject.Find("BuyButton");
        buyB.SetActive(false);
    }
    public GameObject currentPlayerF()
    {
        CurrentPlayer = GameObject.Find("CurrentPlayer");
        currentPlayer cPlayer = CurrentPlayer.GetComponent<currentPlayer>();
        GameObject CPlayer = cPlayer._currentplayer;
        return CPlayer;
    }
    public void curNode(ElementNode node)
    {
        currentNode cnode = FindObjectOfType<currentNode>();
        cnode.cNode = node;
    }
    public void endTurn(Canvas canvas)
    {
        if (!(canvas == null))
        {
            canvas.enabled = false;
            GameObject cplayer = currentPlayerF();
            Player player = cplayer.GetComponent<Player>();

            ElementNodeCreator ElNoCr = FindObjectOfType<ElementNodeCreator>();
            ElNoCr.PlayerState_MovingComplete(player);
            ElNoCr.allowRolling = true;
        }
    }

}
