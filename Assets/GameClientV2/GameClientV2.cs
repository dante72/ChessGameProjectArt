using ChessGameClient;
using System;

    public class GameClientV2 : GameClient
    {
        public GameClientV2() : base()
        {
        }

        private static GameClientV2 client;
        public new static GameClientV2 Client
        {
            get
            {
                if (client == null)
                {
                    client = new GameClientV2();
                }

                return client;
            }
        }

        public override Type GetGameHubServiceType() => typeof(GameHubServiceImplV2);
    }
