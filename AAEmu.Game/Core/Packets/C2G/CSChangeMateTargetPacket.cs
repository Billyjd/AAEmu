using AAEmu.Commons.Network;
using AAEmu.Game.Core.Network.Game;

namespace AAEmu.Game.Core.Packets.C2G
{
    public class CSChangeMateTargetPacket : GamePacket
    {
        public CSChangeMateTargetPacket() : base(0x0a3, 1)
        {
        }

        public override void Read(PacketStream stream)
        {
            var tl = stream.ReadUInt16();
            var objId = stream.ReadBc();

            _log.Warn("ChangeMateTarget, TlId: {0}, ObjId: {1}", tl, objId);
        }
    }
}
