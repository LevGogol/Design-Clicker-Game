using System.Collections.Generic;
using System.Linq;
using DG.Tweening;

public class TweenChain
{
    public Queue<Sequence> SequenceQueue = new Queue<Sequence>();

    public void AddAndPlay(Tween tween)
    {
        var sequence = DG.Tweening.DOTween.Sequence();
        sequence.Pause();
        sequence.Append(tween);
        SequenceQueue.Enqueue(sequence);
        if (SequenceQueue.Count == 1)
        {
            SequenceQueue.Peek().Play();
        }
        sequence.OnComplete(OnComplete);
    }

    private void OnComplete()
    {
        SequenceQueue.Dequeue();
        if (SequenceQueue.Count > 0)
        {
            SequenceQueue.Peek().Play();
        }
    }

    public bool IsRunning()
    {
        return (SequenceQueue.Count > 0);
    }

    public void Destroy()
    {
        foreach (var sequence in SequenceQueue)
        {
            sequence.Kill();
        }
        SequenceQueue.Clear();
    }

    public void AddSequence(Sequence sequence)
    {
        sequence.Pause();
        SequenceQueue.Append(sequence);
    }
}