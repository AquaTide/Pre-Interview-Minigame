using System;

[Flags]
public enum Ailment
{
    Poison = 1 << 0,
    Stun = 1 << 1,
    Paralyze = 1 << 2,
    Sleep = 1 << 3,
    Dead = 1 << 4,
}

public interface IAilment
{
    Ailment ActiveAilment { get; }
    void SetActiveAilment(Ailment activeAilment);
    void CureAilment(Ailment ailment);
    void CureAllAilments();
    void PoisonOccur();
    void StunOccur();
    void ParalyzeOccur();
    void SleepOccur();
    void DeadOccur();
}
