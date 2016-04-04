namespace MapleCLB.Packets {
    /***************SEND**************
        PONG            - Client response to every PING
         * 
        CLIENT_HELLO    - Sent immediately after handshake (first time)
        CLIENT_LOGIN    - Sent when logging in
        SERVER_LOGIN    - Sent when selecting the channel
        CHAR_SELECT     - Sent after entering PIC
        PLAYER_LOGGEDIN - Sent immediately after handshake (rest of the time)
        GET_SERVERS     - Sending this causes server to respond with SERVERLIST
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

        TRADE           - Send when opening a permit or trade
        USE_MUSHY       - Send when double clicking a mushroom in inventory
        BEFORE_MOVE     - Send before first MOVE_PLAYER [Header][TimeStamp]
         ********************************/

    internal static class SendOps {
        public const ushort
            /* login */
            CLIENT_HELLO = 0x67,
            CLIENT_LOGIN = 0x69,
            SERVER_LOGIN = 0x6A,
            CHAR_SELECT = 0x6B,
            PLAYER_LOGGEDIN = 0x6E,
            PONG = 0x93,

            GET_SERVERS = 0x9E, // Useless

            MOVE_BASE = 0xAC,
            ENTER_PORTAL = MOVE_BASE,
            CHANGE_CHANNEL = MOVE_BASE + 0x01,
            ENTER_CASHSHOP = MOVE_BASE + 0x05,
            MOVE_PLAYER = MOVE_BASE + 0x0F,

            SPECIAL_PORTAL = 0x01EE,

            HIT_REACTOR = 0x0331,
            LOOT_ITEM = 0x032E,
            TRADE = 0x0179,
            USE_MUSHY = 0x00E1,
            /* Chat */
            GENERAL_CHAT = 0xC7,
            WHISPER = 0x0177,

            DROP_ITEM = 0xEC,
            DROP_MESO = 0x012F,

            BEFORE_MOVE = 0x022E,

            /* NPC */
            NPC_CHAT = 0xDA, 
            NPC_CHAT_MORE = NPC_CHAT + 0x02; //why do i have this?
    }

    internal static class GameConsts {
        public const int FM1_CRC = 0x26F611E3;
    }

    /*************RECEIVE************
    PING          - Sent by server, respond with PONG
     * 
    LOGIN_SECOND  - Received after PLAYER_LOGGEDIN (SERVER_LOGIN?)
    CHARLIST      - Received at character select screen
    SERVER_IP     - Received right before connecting to server (first time)
    CHANNEL_IP    - Received right before connecting to channel (rest of the time)
     * 
    SPAWN_PLAYER  - Received when player enters map or when you enter a new map
    REMOVE_PLAYER - Received when player leaves map
    FINISH_LOAD   - Received when client is done loading [HEADER] 
    CHAR_INFO     - Received when client loads Character/Map Info *Very Large Packet*

    LOAD_SEED    - Received when you login [HEADER][4 Bytes] (Not Zeros?)
    LOAD_MUSHY   - Received when client loads mushrooms in FM
    BLUE_POP     - Received when opening a Permit shop
    CLOSE_PERMIT - Received when someone closes a permit
    CLOSE_MUSHY  - Recieved when someone closes a mushroom
    ********************************/

    internal static class RecvOps {
        public const ushort
            /* server */
            LOGIN_STATUS = 0x00,
            CHARLIST = 0x06,
            SERVER_IP = 0x07,
            LOGIN_SECOND = 0x08,
            CHANNEL_IP = 0x11,
            PING = 0x12,

            SERVERLIST = 0x01, //Useless

            /* player */
            CHAR_INFO = 0x019E,
            UPDATE_INVENTORY = 0x42,
            UPDATE_STATUS = 0x44,
            UPDATE_REACTOR = 0x03C4,
            SPAWN_REACTOR = 0x03C6,
            SEED = 0x0172,

            BLUE_POP = 0x60,

            /* shop */
            LOAD_MUSHY = 0x03B1,
            CLOSE_MUSHY = LOAD_MUSHY + 0x02,
            CLOSE_PERMIT = 0x01F2,
            UPDATE_SHOP = 0x04D6,

            /* map */
            SPAWN_PLAYER = 0x01EE,
            REMOVE_PLAYER = SPAWN_PLAYER + 0x01,
            ALL_CHAT = SPAWN_PLAYER + 0x02,
            SPAWN_ITEM = 0x03B4,
            REMOVE_ITEM = SPAWN_ITEM + 0x02,
            FINISH_LOAD = 0x0047,

            /* Mob */
            MOB_BASE = 0x35B,
            SPAWN_MOB = MOB_BASE,
            REMOVE_MOB = MOB_BASE + 0x01,
            CONTROL_MOB = MOB_BASE + 0x02;
    }
}