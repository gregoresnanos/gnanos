using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyDice.Board;
using MyDice.Players;
using TMPro;

public class gameController : MonoBehaviour
{
    public GameObject CurrentPlayer;
    public GameObject canvasPrefab;
    public GameObject canvasPrefClone;
    public Player player;
    
    public void CheckNodesType(ElementNode node)
    {
        
        ElementNodeCreator ENC = FindObjectOfType<ElementNodeCreator>();
        ENC.allowRolling = false;

        GameObject CPlayer = currentPlayerF();
        player = CPlayer.GetComponent<Player>();







        if (EndGame(player) == false)
        {

            if (node.NodeType == 1)
            {
                if (node.Owned)
                {
                    if (!CheckOwnership(CPlayer, node.Owner))
                    {
                        UpdateUI(node.PropertyName, node.PropertyPrice.ToString(), node.PayingPrice.ToString(), node.Owned, true);
                        PayPlayer(node, player);

                    }
                    else
                    {
                        UpdateUI(node.PropertyName, node.PropertyPrice.ToString(), node.PayingPrice.ToString(), node.Owned, true);

                    }
                }
                else
                {
                    UpdateUI(node.PropertyName, node.PropertyPrice.ToString(), node.PayingPrice.ToString(), node.Owned, true); //Edw apla peirazw to ui gia na vgazei to swsto kathe fora

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
                payFine(player, node.PropertyPrice);
            }
            else if (node.NodeType == 5)
            {
                getBonus(player, node.PropertyPrice);
            }
            else if (node.NodeType == 6)
            {
                //call insurance func    /// auto gia pio meta einai pio periploko
            }
            else if (node.NodeType == 7)
            {
                //call bill func /// auto episis pio meta
            }
        }
        else
        {
            // Edw tha mpei ena ui gia to telos tou paixnidiou kai oles oi func pou tha elenxoun poios nikise
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

    public void UpdateUI(string Title, string Price, string PricePaying, bool owned, bool prop)
    {
        canvasPrefClone = Instantiate(canvasPrefab);
        
        GameObject title = GameObject.Find("Title");
        Debug.Log(title);
        GameObject price = GameObject.Find("Price");
        GameObject buyB = GameObject.Find("BuyButton");
        TextMeshProUGUI titleText = title.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI priceText = price.GetComponent<TextMeshProUGUI>();

        titleText.text = Title;


        if (prop)
        {
            if (owned)
            {

                priceText.text = "Price: " + PricePaying + "$";
                buyB.SetActive(false);


            }
            else
            {
                priceText.text = "Price: " + Price + "$";
                buyB.SetActive(true);

            }
        }
        else
        {

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
    public void endTurn()
    {

        
        DestroyImmediate(GameObject.FindWithTag("UIPROP"));
    }
    public void payFine(Player player, int fine)
    {
        player.Wallet = player.Wallet - fine;
    }
    public void getBonus(Player player, int bonus)
    {
        player.Wallet = player.Wallet + bonus;
    }
    public void PassStart(Player player, int startBonus)
    {
        player.Wallet = player.Wallet + startBonus;
        player.RoundCounter = player.RoundCounter + 1;
    }
    public bool EndGame(Player player)
    {
        if (player.RoundCounter < 2)
        {
            return false;
        }
        else
        {
            return true;
        }
    }


}
