using System.Collections;
using System.Collections.Generic;
using Thirdweb;
using UnityEngine;
using UnityEngine.Events;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private UnityEvent _onInitialNFTNotClaimed;
    [SerializeField] private UnityEvent _onInitialNFTClaimed;

    public void OnWalletConnected(string walletAddress)
    {
        FetchInitialNFT("0x94894F65d93eb124839C667Fc04F97723e5C4544");
    }

    private async void FetchInitialNFT(string contractAddress)
    {
        var contract = ThirdwebManager.Instance.SDK.GetContract(contractAddress);
        var nftBalance = await contract.ERC1155.BalanceOf(contractAddress, "0");
        var nftBalanceInt = int.Parse(nftBalance.ToString());

        if (nftBalanceInt > 0)
        {
            _onInitialNFTClaimed?.Invoke();
        }
        else
        {
            _onInitialNFTNotClaimed?.Invoke();
        }
    }

    public void ClaimInitial(string contractAddress = "0x94894F65d93eb124839C667Fc04F97723e5C4544")
    {
        Claim(contractAddress);
    }

    private async void Claim(string contractAddress, string tokenId = "0", int amount = 1)
    {
        var contract = ThirdwebManager.Instance.SDK.GetContract(contractAddress);
        var nftBalance = await contract.ERC1155.BalanceOf(contractAddress, "0");
        var data = await contract.ERC1155.Get(tokenId);
        if (int.Parse(nftBalance.ToString()) > 0)
        {
            Debug.Log("Already Claimed");
        }
        else
        {
            TransactionResult claimResult = await contract.ERC1155.Claim(tokenId, amount);
        }
        // if (claimResult != null)
        // {
        //     
        // }
        
        Debug.Log("Breakpoint");
    }

    public async void GetAllNFTData(string contractAddress)
    {
        var contract = ThirdwebManager.Instance.SDK.GetContract(contractAddress);
        var data = await contract.ERC721.GetAll();
        Debug.Log("Get all completed");
    }

    public async void MintAdditionSupply(string contractAddress)
    {
        var contract = ThirdwebManager.Instance.SDK.GetContract(contractAddress);
        var data = await contract.ERC1155.MintAdditionalSupply("0",1);
        Debug.Log(data);
    }
}