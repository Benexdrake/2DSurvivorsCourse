public class GameConstants
{
    // Animation
    public const string ANIM_WALK = "walk";
    public const string ANIM_DEATH = "death";
    public const string ANIM_IN = "in";
    public const string ANIM_OUT = "out";
    public const string ANIM_HIT = "hit";
    public const string ANIM_CARD_HOVER = "hover";
    public const string ANIM_CARD_SELECTED = "selected";
    public const string ANIM_CARD_DISCARD = "discard";
    
    // public const string ANIM_IDLE = "Idle";
    // public const string ANIM_MOVE = "Move";
    // public const string ANIM_DASH = "Dash";
    // public const string ANIM_ATTACK = "Attack";
    // public const string ANIM_KICK = "Kick";
    // public const string ANIM_DEATH = "Death";
    // public const string ANIM_EXPAND = "Expand";
    // public const string ANIM_EXPLOSION = "Explosion";
    // public const string ANIM_STUN = "Stun";

    // Input
    public const string INPUT_MOVE_LEFT = "MoveLeft";
    public const string INPUT_MOVE_RIGHT = "MoveRight";
    public const string INPUT_MOVE_UP = "MoveUp";
    public const string INPUT_MOVE_DOWN = "MoveDown";
    public const string INPUT_LEFT_CLICK = "LeftClick";
    public const string INPUT_DASH = "Dash";
    public const string INPUT_PAUSE = "Pause";
    public const string INPUT_INTERACT = "Interact";

    // Groups
    public const string GROUP_ENTITIES_LAYER = "entities_layer";
    public const string GROUP_FOREGROUND_LAYER = "foreground_layer";
    public const string GROUP_UPGRADE_CARD = "upgrade_card";
    public const string GROUP_META_CARD = "meta_card";

    // Abilitys
    public const string ABILITY_PLAYER_SPEED = "player_speed";
    public const string ABILITY_SWORD_RATE = "sword_rate";
    public const string ABILITY_SWORD_DMG = "sword_damage";
    public const string ABILITY_AXE = "axe";
    public const string ABILITY_AXE_DMG = "axe_damage";

    // Entities
    public const string PLAYER = "Player";

    // Scenes
    public const string MAIN_SCENE = "res://Files/Scenes/Levels/Level1/main.tscn";
    public const string MAIN_MENU_SCENE = "res://Files/Scenes/UI/main_menu.tscn";
    public const string OPTIONS_MENU_SCENE = "res://Files/Scenes/UI/options_menu.tscn";
    public const string CHARACTER_MENU_SCENE = "res://Files/Scenes/UI/character_screen.tscn";
    public const string META_MENU_SCENE = "res://Files/Scenes/UI/meta_menu.tscn";

    // Label
    public const string DEFEAT_TITLE_LABEL = "Defeat";
    public const string DEFEAT_DESCRIPTION_LABEL = "You Lost!";
    public const string WIN_TITLE_LABEL = "Winner";
    public const string WIN_DESCRIPTION_LABEL = "You Won!";

    // Gameplay
    public const float DIFFICULTY_INTERVAL = 1;
    public const float DIFFICULTY_ENEMY_SPAWN_TIME = 1;
    public const int SKILL_SWORD_DMG = 1;
    public const int SKILL_AXE_DMG = 1;
    public const int SKILL_AXE_RADIUS = 100;

    // Save File
    public const string SAVE_FILE_PATH = "user://game.save";

    // Meta Upgrades
    public const string META_VIAL_DROP = "experience_gain";

}