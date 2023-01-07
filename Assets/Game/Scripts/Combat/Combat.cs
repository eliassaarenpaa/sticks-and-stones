public class Combat
{
    private IAttacker attacker;
    private IDefender defender;

    public Combat(IAttacker attacker, IDefender defender)
    {
        this.attacker = attacker;
        this.defender = defender;
    }

    public void Fight()
    {
        defender.Defend(attacker);
    }
}
