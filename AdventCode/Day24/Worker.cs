using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode.Day24
{
    public class Worker
    {
        private List<Group> _immunity = new List<Group>();

        private List<Group> _infection = new List<Group>();

        public Worker()
        {
            var halves = _input.Split(new[] { $"{Environment.NewLine}{Environment.NewLine}" }, StringSplitOptions.RemoveEmptyEntries);
            var immunityStrings = halves[0].Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Skip(1);
            var infectionStrings = halves[1].Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Skip(1);

            foreach (string split in immunityStrings)
            {
                _immunity.Add(new Group(split, true));
            }
            foreach (string split in infectionStrings)
            {
                _infection.Add(new Group(split, false));
            }
        }

        public int Work1()
        {
            while (_immunity.Any() && _infection.Any())
            {
                // select targets
                var allGroups = _immunity.Concat(_infection);
                var orderedGroups = allGroups.OrderByDescending(x => x.EffectivePower).ThenByDescending(x => x.Initiative);
                foreach (var group in orderedGroups)
                {
                    IEnumerable<Group> possibleTargets;
                    if (group.IsImmunity)
                        possibleTargets = _infection.Where(x => !x.IsTargeted);
                    else
                        possibleTargets = _immunity.Where(x => !x.IsTargeted);

                    var target = TargetGroup(group, possibleTargets);
                    if (target == null)
                        continue;
                    target.IsTargeted = true;
                    group.AttackTarget = target;
                }

                // attack
                var attackers = allGroups.Where(x => x.AttackTarget != null).OrderByDescending(x => x.Initiative);
                foreach (var attacker in attackers)
                {
                    Attack(attacker, attacker.AttackTarget, attacker.AttackTarget.IsImmunity ? _immunity : _infection);
                }

                // clean up after phase
                foreach (var group in allGroups)
                {
                    group.AttackTarget = null;
                    group.IsTargeted = false;
                }
            }

            if (_immunity.Any())
                return _immunity.Sum(x => x.Units);
            else
                return _infection.Sum(x => x.Units);
        }

        public int Work2()
        {
            int boostStep = 1;
            int boost = 60; // figured out by modifying the boostStep
            List<Group> immunity, infection;
            int immunityUnits = 0, infectionUnits = 0;
            bool draw = false;

            while (true)
            {
                immunity = _immunity.Select(x => x.CreateCopy()).ToList();

                infection = _infection.Select(x => x.CreateCopy()).ToList();

                immunity.ForEach(x => x.Damage += boost);

                while (immunity.Any() && infection.Any())
                {
                    // select targets
                    var allGroups = immunity.Concat(infection);
                    var orderedGroups = allGroups.OrderByDescending(x => x.EffectivePower).ThenByDescending(x => x.Initiative);
                    foreach (var group in orderedGroups)
                    {
                        IEnumerable<Group> possibleTargets;
                        if (group.IsImmunity)
                            possibleTargets = infection.Where(x => !x.IsTargeted);
                        else
                            possibleTargets = immunity.Where(x => !x.IsTargeted);

                        var target = TargetGroup(group, possibleTargets);
                        if (target == null)
                            continue;
                        target.IsTargeted = true;
                        group.AttackTarget = target;
                    }

                    // attack
                    var attackers = allGroups.Where(x => x.AttackTarget != null).OrderByDescending(x => x.Initiative);
                    foreach (var attacker in attackers)
                    {
                        Attack(attacker, attacker.AttackTarget, attacker.AttackTarget.IsImmunity ? immunity : infection);
                    }

                    // clean up after phase
                    foreach (var group in allGroups)
                    {
                        group.AttackTarget = null;
                        group.IsTargeted = false;
                    }

                    int imSum = immunity.Sum(x => x.Units), inSum = infection.Sum(x => x.Units);
                    if (imSum == immunityUnits && inSum == infectionUnits)
                    {
                        draw = true;
                        break;
                    }
                    else
                    {
                        immunityUnits = imSum;
                        infectionUnits = inSum;
                    }
                }

                if (!draw && immunity.Any())
                    break;
                else
                {
                    boost += boostStep;
                    draw = false;
                }
            }

            return immunity.Sum(x => x.Units);
        }

        private void Attack(Group attacker, Group target, List<Group> targets)
        {
            if (attacker.Units <= 0)
                return;

            int damage = 0;
            if (target.Weakness.Contains(attacker.DamageType))
                damage = attacker.EffectivePower * 2;
            else
                damage = attacker.EffectivePower;

            int restHp = damage % target.UnitHitPoints;
            damage -= restHp;

            int killedUnits = damage / target.UnitHitPoints;
            target.Units -= killedUnits;
            if (target.Units <= 0)
                targets.Remove(target);
        }

        private Group TargetGroup(Group attacker, IEnumerable<Group> targets)
        {
            int damageToTargeted = 0;
            Group targetedGroup = null;

            foreach (var possibleTarget in targets)
            {
                if (possibleTarget.Immunity.Contains(attacker.DamageType))
                    continue;
                else if (possibleTarget.Weakness.Contains(attacker.DamageType))
                {
                    int currentDamage = attacker.EffectivePower * 2;
                    if (currentDamage > damageToTargeted)
                    {
                        damageToTargeted = currentDamage;
                        targetedGroup = possibleTarget;
                    }
                    else if (currentDamage == damageToTargeted)
                    {
                        if (targetedGroup == null || targetedGroup.EffectivePower < possibleTarget.EffectivePower)
                        {
                            damageToTargeted = currentDamage;
                            targetedGroup = possibleTarget;
                        }
                        else if (targetedGroup.EffectivePower == possibleTarget.EffectivePower)
                        {
                            if (targetedGroup.Initiative < possibleTarget.Initiative)
                            {
                                damageToTargeted = currentDamage;
                                targetedGroup = possibleTarget;
                            }
                        }
                    }
                }
                else
                {
                    int currentDamage = attacker.EffectivePower;
                    if (currentDamage > damageToTargeted)
                    {
                        damageToTargeted = currentDamage;
                        targetedGroup = possibleTarget;
                    }
                    else if (currentDamage == damageToTargeted)
                    {
                        if (targetedGroup == null || targetedGroup.EffectivePower < possibleTarget.EffectivePower)
                        {
                            damageToTargeted = currentDamage;
                            targetedGroup = possibleTarget;
                        }
                        else if (targetedGroup.EffectivePower == possibleTarget.EffectivePower)
                        {
                            if (targetedGroup.Initiative < possibleTarget.Initiative)
                            {
                                damageToTargeted = currentDamage;
                                targetedGroup = possibleTarget;
                            }
                        }
                    }
                }
            }
            return targetedGroup;
        }

        string _testInput = @"Immune System:
17 units each with 5390 hit points (weak to radiation, bludgeoning) with an attack that does 4507 fire damage at initiative 2
989 units each with 1274 hit points (immune to fire; weak to bludgeoning, slashing) with an attack that does 25 slashing dmage at initiative 3

Infection:
801 units each with 4706 hit points (weak to radiation) with an attack that does 116 bludgeoning damage at initiative 1
4485 units each with 2961 hit points (immune to radiation; weak to fire, cold) with an attack that does 12 slashing damage at initiative 4";

        string _input = @"Immune System:
4081 units each with 8009 hit points (immune to slashing, radiation; weak to bludgeoning, cold) with an attack that does 17 fire damage at initiative 7
2599 units each with 11625 hit points with an attack that does 36 bludgeoning damage at initiative 17
4232 units each with 4848 hit points (weak to slashing) with an attack that does 11 bludgeoning damage at initiative 13
2192 units each with 8410 hit points (immune to fire, radiation; weak to cold) with an attack that does 36 bludgeoning damage at initiative 18
4040 units each with 8260 hit points (immune to cold) with an attack that does 17 bludgeoning damage at initiative 20
1224 units each with 4983 hit points (immune to bludgeoning, cold, slashing, fire) with an attack that does 37 radiation damage at initiative 6
1462 units each with 6747 hit points with an attack that does 41 bludgeoning damage at initiative 10
815 units each with 2261 hit points (weak to cold) with an attack that does 22 cold damage at initiative 19
2129 units each with 1138 hit points (weak to radiation, cold) with an attack that does 5 bludgeoning damage at initiative 3
1836 units each with 8018 hit points (immune to radiation) with an attack that does 37 slashing damage at initiative 15

Infection:
909 units each with 34180 hit points (weak to slashing, bludgeoning) with an attack that does 72 bludgeoning damage at initiative 4
908 units each with 57557 hit points (weak to bludgeoning) with an attack that does 96 fire damage at initiative 14
65 units each with 32784 hit points (weak to cold; immune to bludgeoning) with an attack that does 957 fire damage at initiative 2
5427 units each with 50754 hit points with an attack that does 14 radiation damage at initiative 12
3788 units each with 27222 hit points (immune to cold, bludgeoning) with an attack that does 14 slashing damage at initiative 16
7704 units each with 14742 hit points (immune to cold) with an attack that does 3 fire damage at initiative 1
5428 units each with 51701 hit points (weak to fire) with an attack that does 14 fire damage at initiative 9
3271 units each with 32145 hit points (weak to bludgeoning, radiation) with an attack that does 19 bludgeoning damage at initiative 8
99 units each with 49137 hit points with an attack that does 855 fire damage at initiative 5
398 units each with 29275 hit points (weak to fire; immune to slashing) with an attack that does 137 cold damage at initiative 11";
    }
}
