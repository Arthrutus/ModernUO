using System;
using Server.Engines.ConPVP;
using Server.Network;

namespace Server.Items
{
  public abstract class BaseHealPotion : BasePotion
  {
    public BaseHealPotion(PotionEffect effect) : base(0xF0C, effect)
    {
    }

    public BaseHealPotion(Serial serial) : base(serial)
    {
    }

    public abstract int MinHeal{ get; }
    public abstract int MaxHeal{ get; }
    public abstract double Delay{ get; }

    public override void Serialize(GenericWriter writer)
    {
      base.Serialize(writer);

      writer.Write(0); // version
    }

    public override void Deserialize(GenericReader reader)
    {
      base.Deserialize(reader);

      int version = reader.ReadInt();
    }

    public void DoHeal(Mobile from)
    {
      int min = Scale(from, MinHeal);
      int max = Scale(from, MaxHeal);

      from.Heal(Utility.RandomMinMax(min, max));
    }

    public override void Drink(Mobile from)
    {
      if (from.Hits < from.HitsMax)
      {
        if (from.Poisoned || MortalStrike.IsWounded(from))
        {
          from.LocalOverheadMessage(MessageType.Regular, 0x22,
            1005000); // You can not heal yourself in your current state.
        }
        else
        {
          if (from.BeginAction<BaseHealPotion>())
          {
            DoHeal(from);

            PlayDrinkEffect(from);

            if (!DuelContext.IsFreeConsume(from))
              Consume();

            Timer.DelayCall(TimeSpan.FromSeconds(Delay), from.EndAction<BaseHealPotion>);
          }
          else
          {
            from.LocalOverheadMessage(MessageType.Regular, 0x22,
              500235); // You must wait 10 seconds before using another healing potion.
          }
        }
      }
      else
      {
        from.SendLocalizedMessage(
          1049547); // You decide against drinking this potion, as you are already at full health.
      }
    }
  }
}