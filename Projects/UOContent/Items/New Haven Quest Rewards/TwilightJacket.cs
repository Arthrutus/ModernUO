namespace Server.Items
{
    public class TwilightJacket : LeatherNinjaJacket
    {
        [Constructible]
        public TwilightJacket()
        {
            LootType = LootType.Blessed;

            Attributes.ReflectPhysical = 5;
        }

        public TwilightJacket(Serial serial) : base(serial)
        {
        }

        public override int LabelNumber => 1078183; // Twilight Jacket

        public override int BasePhysicalResistance => 6;
        public override int BaseFireResistance => 12;
        public override int BaseColdResistance => 3;
        public override int BasePoisonResistance => 3;
        public override int BaseEnergyResistance => 3;

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
