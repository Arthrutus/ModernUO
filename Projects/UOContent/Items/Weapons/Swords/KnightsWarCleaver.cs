namespace Server.Items
{
    public class KnightsWarCleaver : WarCleaver
    {
        [Constructible]
        public KnightsWarCleaver() => Attributes.RegenHits = 3;

        public KnightsWarCleaver(Serial serial) : base(serial)
        {
        }

        public override int LabelNumber => 1073525; // knight's war cleaver

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            var version = reader.ReadEncodedInt();
        }
    }
}
