using System;

public class Wallet
{
    public int CoinCount { get; set; }

    public Wallet(int coinCount = 0)
    {
        CoinCount = coinCount;
    }
}