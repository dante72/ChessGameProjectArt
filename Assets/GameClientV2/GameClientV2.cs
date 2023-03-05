using ChessGameClient;
using System;

    public class GameClientV2 : GameClient
    {
        public GameClientV2() : base()
        {
        }

        readonly private static GameClientV2 client = new GameClientV2();
        public new static GameClientV2 Client => client;
        public override Type GetGameHubServiceType() => typeof(GameHubServiceImplV2);
    }
