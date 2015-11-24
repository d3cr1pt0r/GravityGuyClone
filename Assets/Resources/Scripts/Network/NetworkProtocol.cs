using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Resources.Scripts.Network
{
    class NetworkProtocol
    {
        public static byte CONNECTION_REQUESTED = 0x00;
        public static byte CONNECTION_ACCEPTED = 0x01;
        public static byte CONNECTION_REJECTED = 0x02;
        public static byte DISCONNECT_REQUESTED = 0x03;
        public static byte TOUCH_PRESSED = 0x04;
        public static byte GAME_OVER = 0x05;

    }
}
