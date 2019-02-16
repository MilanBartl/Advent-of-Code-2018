using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventCode.Day24
{
    public class Group
    {
        public bool IsImmunity { get; set; }

        public int Units { get; set; }

        public int UnitHitPoints { get; set; }

        public int Initiative { get; set; }

        public int Damage { get; set; }

        public int EffectivePower { get { return Units * Damage; } }

        public AttackType DamageType { get; set; }

        public List<AttackType> Immunity { get; set; }

        public List<AttackType> Weakness { get; set; }

        public bool IsTargeted { get; set; }

        public Group AttackTarget { get; set; }

        public Group(string input, bool isImmunity)
        {
            IsImmunity = isImmunity;

            var unitMatch = Regex.Match(input, @"^\d+ units");
            var hpMatch = Regex.Match(input, @"\d+ hit points");
            var immuneMatch = Regex.Match(input, @"immune to [a-z, ]+");
            var weakMatch = Regex.Match(input, @"weak to [a-z, ]+");
            var damageMatch = Regex.Match(input, @"that does \d+ [a-z]+");
            var initiativeMatch = Regex.Match(input, @"initiative \d+$");

            Units = int.Parse(unitMatch.Value.Split(' ')[0]);
            UnitHitPoints = int.Parse(hpMatch.Value.Split(' ')[0]);
            string[] damageSplits = damageMatch.Value.Split(' ');
            Damage = int.Parse(damageSplits[2]);
            DamageType = SelectAttackType(damageSplits[3]);
            Initiative = int.Parse(initiativeMatch.Value.Split(' ')[1]);

            Immunity = new List<AttackType>();
            var immuneSplits = immuneMatch.Value.Split(' ').Skip(2);
            foreach (string split in immuneSplits)
            {
                Immunity.Add(SelectAttackType(split.TrimEnd(new[] { ',', ';' })));
            }

            Weakness = new List<AttackType>();
            var weakSplits = weakMatch.Value.Split(' ').Skip(2);
            foreach (string split in weakSplits)
            {
                Weakness.Add(SelectAttackType(split.TrimEnd(new[] { ',', ')' })));
            }
        }

        // Empty ctor
        public Group()
        {
        }

        public Group CreateCopy()
        {
            return new Group()
            {
                IsImmunity = this.IsImmunity,
                Units = this.Units,
                UnitHitPoints = this.UnitHitPoints,
                Initiative = this.Initiative,
                Damage = this.Damage,
                DamageType = this.DamageType,
                Immunity = this.Immunity,
                Weakness = this.Weakness
            };
        }

        private AttackType SelectAttackType(string input)
        {
            if (AttackType.Bludgeoning.Text() == input)
                return AttackType.Bludgeoning;
            else if (AttackType.Cold.Text() == input)
                return AttackType.Cold;
            else if (AttackType.Fire.Text() == input)
                return AttackType.Fire;
            else if (AttackType.Radiation.Text() == input)
                return AttackType.Radiation;
            else if (AttackType.Slashing.Text() == input)
                return AttackType.Slashing;
            else
                throw new Exception("This should not happen.");
        }
    }

    public enum AttackType
    {
        [Description("fire")]
        Fire,
        [Description("cold")]
        Cold,
        [Description("slashing")]
        Slashing,
        [Description("bludgeoning")]
        Bludgeoning,
        [Description("radiation")]
        Radiation
    }
}
