using AAEmu.Commons.Network;
using AAEmu.Game.Core.Network.Game;
using AAEmu.Game.Models.Game.Mails;

namespace AAEmu.Game.Core.Packets.G2C
{
    public class SCMailReturnedPacket : GamePacket
    {
        private readonly long _mailId;
        private readonly Mail _mail;
        
        public SCMailReturnedPacket(long mailId, Mail mail) : base(0x11b, 1)
        {
            _mailId = mailId;
            _mail = mail;
        }

        public override PacketStream Write(PacketStream stream)
        {
            stream.Write(_mailId);
            stream.Write(_mail);
            return stream;
        }
    }
}
