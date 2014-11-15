namespace MapleCLB.Packets
{
    /***************SEND**************
    PONG            - Client response to every PING
     * 
    CLIENT_HELLO    - Sent immediately after handshake (first time)
    PLAYER_LOGGEDIN - Sent immediately after handshake (rest of the time)
    SERVER_LOGIN    - Sent when selecting the channel
    CHAR_SELECT     - Sent after entering PIC
     * 
    CHANGE_MAP      - Sent when using portal or exiting cash shop
    CHANGE_CHANNEL  - Sent when changing channels
     * 
    ENTER_CASH_SHOP - Sent when entering cash shop
     * 
    MOVE_PLAYER     - Sent when moving player
     * 
    NPC_TALK        - Sent when first talking to a NPC
    NPC_TALK_MORE   - Send when continuing chat with NPC
     ********************************/
    class SendOps
    {
        public const short
            PONG = 0x46,

            /* login */
            CLIENT_HELLO = 0x3F,
            PLAYER_LOGGEDIN = 0x27, //CONST (Usually)
            SERVER_LOGIN = 0x43,
            CHAR_SELECT = 0x47,

            CHANGE_MAP = 0x50,
            CHANGE_CHANNEL = CHANGE_MAP + 0x01,

            /* cashshop */
            ENTER_CASHSHOP = 0x53,

            MOVE_PLAYER = 0x5D,

            NPC_TALK = 0x7E,
            NPC_TALK_MORE = NPC_TALK + 0x02;
    }

    /*************RECEIVE************
    PING          - Sent by server, respond with PONG
     * 
    LOGIN_SECOND  - Received after PLAYER_LOGGEDIN
    CHARLIST      - Received at character select screen
    SERVER_IP     - Received right before connecting to server (first time)
    CHANNEL_IP    - Received right before connecting to channel (rest of the time)
     * 
    SPAWN_PLAYER  - Received when player enters map or when you enter a new map
    REMOVE_PLAYER - Received when player leaves map
    ********************************/
    class RecvOps
    {
        public const short
            PING = 0x13,

            /* server */
            LOGIN_SECOND = 0x02, //CONST (Usually)
            CHARLIST = 0x0A,
            SERVER_IP = 0x0B,
            CHANNEL_IP = 0x12,

            SPAWN_PLAYER = 0x0197,
            REMOVE_PLAYER = SPAWN_PLAYER + 0x01;
    }
}