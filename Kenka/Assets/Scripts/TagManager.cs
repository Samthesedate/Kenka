

public class AnimationTags
{
    //PLAYER TAGS
    public const string PUNCH1_TRIGGER = "Jab";
    public const string PUNCH2_TRIGGER = "Hook";
    public const string PUNCH3_TRIGGER = "UpperCut";
    public const string EVADE_TRIGGER = "Evade";
    public const string KICK1_TRIGGER = "Kick1";
    public const string KICK2_TRIGGER = "Kick2";
    //If someone uses a "weak i.e. upper defense lowkick" when the opponent tries a high kick. The lowkick
    //will be ciritical breaking player defense for x seconds.

    //use one of the stamina giving you higher critical for 4 seconds
    public const string SPECIAL_TRIGGER = "Special";
    //use all 4 of the stamina with a special attack and animation reel if it hits
    public const string SPECIAL_PLUS_TRIGGER = "SpecialPlus";
    //both will have different keys


    public const string RUN_MOVEMENT = "Run";
    public const string DASH_MOVEMENT = "Dash";

    //ENEMY TAGS
    public const string ENEMY_MOVEMENT = "RunE";
    public const string ENEMY_DASH = "DashE";

    public const string E_PUNCH1_TRIGGER = "Punch1E";
    public const string E_PUNCH2_TRIGGER = "Punch2E";
    public const string E_PUNCH3_TRIGGER = "Punch3E";
    public const string E_EVADE_TRIGGER = "EvadeE";
    public const string E_KICK1_TRIGGER = "Kick1E";
    public const string E_KICK2_TRIGGER = "Kick2E";

}

public class Tags
{
    public const string GROUND_TAG = "Ground";
    public const string PLAYER_TAG = "Player";
    public const string ENEMY_TAG = "Enemy";
    public const string HEALTH_UI = "HealthUI";
}

public class Axis
{
    public const string HORIZONTAL_AXIS = "Horizontal";
    public const string VERTICAL_AXIS = "Vertical";
}