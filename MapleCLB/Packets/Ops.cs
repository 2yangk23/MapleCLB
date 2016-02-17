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
         ********************************/

    internal class SendOps {
        public const ushort
            PONG = 0x9E,

            /* login */
            CLIENT_HELLO = 0x67,
            CLIENT_LOGIN = 0x69,
            SERVER_LOGIN = 0x6A,
            CHAR_SELECT = 0x6B,
            PLAYER_LOGGEDIN = 0x6E,
            GET_SERVERS = 0x82,
            CHANGE_MAP = 0xAA,
            CHANGE_CHANNEL = CHANGE_MAP + 0x01,
            TRADE = 0x0196,
            /* Chat */
            GENERAL_CHAT = 0xC5,
            WHISPER = 0x0193,
            SEND_CHAT = 0x00,
            /* cashshop */
            ENTER_CASHSHOP = 0xAF,
            MOVE_PLAYER = 0xB9,
            NPC_TALK = 0x00,
            NPC_TALK_MORE = NPC_TALK + 0x02; //why do i have this?

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

    LOAD_SEED    - Received when you login [HEADER][4 Bytes]
    LOAD_MUSHY   - Received when client loads mushrooms in FM
    ********************************/

    internal class RecvOps {
        public const ushort
            PING = 0x18,

            /* server */
            LOGIN_STATUS = 0x00,
            LOGIN_SECOND = 0x0E,
            CHARLIST = 0x0C,
            SERVERLIST = 0x07, // Useless
            SERVER_IP = 0x0D,
            CHANNEL_IP = 0x17,
            LOAD_MUSHY = 0x03AD,
            CHAR_INFO = 0x019B,
            FINISH_LOAD = 0x0045,
            LOAD_SEED = 0x73,  //4A DC E4 FF is magic number 
            SPAWN_PLAYER = 0x01EB, //0x01D3
            REMOVE_PLAYER = SPAWN_PLAYER + 0x01, //0x01D4

            ALL_CHAT = 0x01ED;
    }
}