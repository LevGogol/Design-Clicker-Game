using System.Collections.Generic;
using Time = UnityEngine.Time;

public class CoinsPerSecondInfo
{
    public struct RegistrateCoins
    {
        public int Coins;
        public float Time;

        public RegistrateCoins(int coins, float time)
        {
            Coins = coins;
            Time = time;
        }
    }

    private Queue<RegistrateCoins> _queue = new Queue<RegistrateCoins>();

    public void Registrate(int takeCoins)
    {
        var registrateCoins = new RegistrateCoins(takeCoins, Time.time);

        _queue.Enqueue(registrateCoins);
    }

    public int Get()
    {
        while(_queue.Count > 0)
        {
            var registrateCoin = _queue.Peek();
            
            var timeInterval = Time.time - registrateCoin.Time;
            if (timeInterval < 1f)
            {
                break;
            }

            _queue.Dequeue();
        }

        var result = 0;
        foreach (var registrateCoin in _queue)
        {
            result += registrateCoin.Coins;
        }
        return result;
    }
}