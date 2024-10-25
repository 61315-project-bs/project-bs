using System;

public interface IStat<T>
{
    public StatType Type { get; set; }
    public T DeepCopy();
    public void Add(T other);
    public void Multiply(T other);
    public T StatCalculator(Func<float, int, float> calculator, int num);
}
public interface IStat_Single<T>
{
    public StatType Type { get; set; }
    public int Index { get; set; }
    public float Value { get; set; }
    public T DeepCopy();
    public void Add(int index, float value);
    public void Multiply(int index, float value);
    public void Remove(int index);
}

public enum StatType
{
    Add,
    Multiple,
    Override
}