

public class Switcher
{
    private bool attack;
    private bool movement;
    private bool lowkick;

    public bool Getattack()
    {
        return attack;
    }

    public bool Getmovement()
    {
        return movement;
    }

    public bool Getlowkick()
    {
        return lowkick;
    }
    public void SetDisableMovement()
    {
        movement = false;
    }
    public void SetEnableMovement()
    {
        movement = true;
    }
    public void SetDisableAttack()
    {
        attack = false;
    }
    public void SetEnableAttack()
    {
        attack = true;
    }
    public void SetDisableLowkick()
    {
        lowkick = false;
    }
    public void SetEnableLowkick()
    {
        lowkick = true;
    }
    /*public void DisableAll()
    {
        attack = false;
        movement = false;
    }//end of disable everything

    public void EnableAll()
    {
        attack = true;
        movement = true;
        lowkick = true;
    }//end of enable everything

    public void DisableLowKick()
    {
        lowkick = false;
        movement = false;
    }//end of disable only lowkicks*/
}
