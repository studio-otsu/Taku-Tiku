﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellAttackMelee : SpellBase {
    public SpellAttackMelee() {
        name = "Corps à corps";
        description = "Inflige 20 dégâts sur une ligne perpendiculaire de 1 case / Inflige 30 dégâts sur une ligne perpendiculaire de 1 case.";
        iconPath = "Sprites/Spells/bleeding-wound";
        rangeLight = new AreaProfile(AreaType.Circle, 1, 1);
        rangeHeavy = new AreaProfile(AreaType.Circle, 1, 1);
        cooldownLight = 1;
        cooldownHeavy = 1;
    }
    public override IEnumerator SolveSpellLight(Player caster, Cell target, Map map) {
        runningSpells++;
        List<Cell> affectedCells = null;
        if (caster.currentCell.x != target.x) { // attack left or right
            affectedCells = map.GetCellsVerticalLine(target, 0, 1);
        } else if (caster.currentCell.y != target.y) { // attack up or down
            affectedCells = map.GetCellsHorizontalLine(target, 0, 1);
        } else throw new System.Exception("Can't aim at the caster cell!");
        foreach (Cell c in affectedCells) {
            // damage units
            if (c.currentUnit != null)
                c.currentUnit.Damage(20);
            // do pretty explosions. pew pew!
            // ...
        }
        runningSpells--;
        yield return null;
    }
    public override IEnumerator SolveSpellHeavy(Player caster, Cell target, Map map) {
        runningSpells++;
        List<Cell> affectedCells = null;
        if (caster.currentCell.x != target.x) { // attack left or right
            affectedCells = map.GetCellsVerticalLine(target, 0, 1);
        } else if (caster.currentCell.y != target.y) { // attack up or down
            affectedCells = map.GetCellsHorizontalLine(target, 0, 1);
        } else throw new System.Exception("Can't aim at the caster cell!");
        foreach (Cell c in affectedCells) {
            // damage units
            if (c.currentUnit != null)
                c.currentUnit.Damage(30);
            // do pretty explosions. pew pew!
            // ...
        }
        runningSpells--;
        yield return null;
    }

    public override List<Cell> GetEffectAreaPreviewHeavy(Player caster, Cell target, Map map) {
        List<Cell> affectedCells = null;
        if (caster.nextCell.x != target.x) { // attack left or right
            affectedCells = map.GetCellsVerticalLine(target, 0, 1);
        } else if (caster.nextCell.y != target.y) { // attack up or down
            affectedCells = map.GetCellsHorizontalLine(target, 0, 1);
        } else throw new System.Exception("Can't aim at the caster cell!");
        return affectedCells;
    }
    public override List<Cell> GetEffectAreaPreviewLight(Player caster, Cell target, Map map) {
        List<Cell> affectedCells = null;
        if (caster.nextCell.x != target.x) { // attack left or right
            affectedCells = map.GetCellsVerticalLine(target, 0, 1);
        } else if (caster.nextCell.y != target.y) { // attack up or down
            affectedCells = map.GetCellsHorizontalLine(target, 0, 1);
        } else throw new System.Exception("Can't aim at the caster cell!");
        return affectedCells;
    }
}

public class SpellAttackShort : SpellBase {
    public SpellAttackShort() {
        name = "Attaque courte";
        description = "Inflige 16 dégâts sur une case / Inflige 20 dégâts sur une case.";
        iconPath = "Sprites/Spells/ice-spear";
        rangeLight = new AreaProfile(AreaType.Circle, 1, 4);
        rangeHeavy = new AreaProfile(AreaType.Circle, 1, 4);
        cooldownLight = 2;
        cooldownHeavy = 2;
    }
    public override IEnumerator SolveSpellLight(Player caster, Cell target, Map map) {
        runningSpells++;
        List<Cell> affectedCells = map.GetCellsCross(target, 0, 0);
        foreach (Cell c in affectedCells) {
            // damage units
            if (c.currentUnit != null)
                c.currentUnit.Damage(16);
            // do pretty explosions. pew pew!
            // ...
        }
        runningSpells--;
        yield return null;
    }
    public override IEnumerator SolveSpellHeavy(Player caster, Cell target, Map map) {
        runningSpells++;
        List<Cell> affectedCells = map.GetCellsCross(target, 0, 0);
        foreach (Cell c in affectedCells) {
            // damage units
            if (c.currentUnit != null)
                c.currentUnit.Damage(20);
            // do pretty explosions. pew pew!
            // ...
        }
        runningSpells--;
        yield return null;
    }
}
public class SpellAttackLarge : SpellBase {
    public SpellAttackLarge() {
        name = "Attaque large";
        description = "Inflige 12 dégâts dans une croix de 1 case / Inflige 16 dégâts dans une croix de 1 case.";
        iconPath = "Sprites/Spells/shatter";
        rangeLight = new AreaProfile(AreaType.Circle, 2, 4);
        rangeHeavy = new AreaProfile(AreaType.Circle, 2, 4);
        cooldownLight = 2;
        cooldownHeavy = 2;
    }
    public override IEnumerator SolveSpellLight(Player caster, Cell target, Map map) {
        runningSpells++;
        List<Cell> affectedCells = map.GetCellsCross(target, 0, 1);
        foreach (Cell c in affectedCells) {
            // damage units
            if (c.currentUnit != null)
                c.currentUnit.Damage(12);
            // do pretty explosions. pew pew!
            // ...
        }
        runningSpells--;
        yield return null;
    }
    public override IEnumerator SolveSpellHeavy(Player caster, Cell target, Map map) {
        runningSpells++;
        List<Cell> affectedCells = map.GetCellsCross(target, 0, 1);
        foreach (Cell c in affectedCells) {
            // damage units
            if (c.currentUnit != null)
                c.currentUnit.Damage(16);
            // do pretty explosions. pew pew!
            // ...
        }
        runningSpells--;
        yield return null;
    }

    public override List<Cell> GetEffectAreaPreviewHeavy(Player caster, Cell target, Map map) {
        List<Cell> affectedCells = map.GetCellsCross(target, 0, 1);
        return affectedCells;
    }
    public override List<Cell> GetEffectAreaPreviewLight(Player caster, Cell target, Map map) {
        List<Cell> affectedCells = map.GetCellsCross(target, 0, 1);
        return affectedCells;
    }
}
public class SpellAttackLong : SpellBase {
    public SpellAttackLong() {
        name = "Attaque précise";
        description = "Inflige 20 dégâts sur une case / Inflige 30 dégâts sur uen case.";
        iconPath = "Sprites/Spells/spark";
        rangeLight = new AreaProfile(AreaType.Circle, 4, 7);
        rangeHeavy = new AreaProfile(AreaType.Circle, 4, 7);
        cooldownLight = 2;
        cooldownHeavy = 2;
    }
    public override IEnumerator SolveSpellLight(Player caster, Cell target, Map map) {
        runningSpells++;
        List<Cell> affectedCells = map.GetCellsCross(target, 0, 0);
        foreach (Cell c in affectedCells) {
            // damage units
            if (c.currentUnit != null)
                c.currentUnit.Damage(20);
            // do pretty explosions. pew pew!
            // ...
        }
        runningSpells--;
        yield return null;
    }
    public override IEnumerator SolveSpellHeavy(Player caster, Cell target, Map map) {
        runningSpells++;
        List<Cell> affectedCells = map.GetCellsCross(target, 0, 0);
        foreach (Cell c in affectedCells) {
            // damage units
            if (c.currentUnit != null)
                c.currentUnit.Damage(30);
            // do pretty explosions. pew pew!
            // ...
        }
        runningSpells--;
        yield return null;
    }
}

public class SpellDash : SpellBase {
    public SpellDash() {
        name = "Précipitation";
        description = "Avance de 2 cases / Avance de 3 cases.";
        iconPath = "Sprites/Spells/fire-dash";
        rangeLight = new AreaProfile(AreaType.Cross, 2, 2);
        rangeHeavy = new AreaProfile(AreaType.Cross, 3, 3);
        cooldownLight = 4;
        cooldownHeavy = 4;
    }
    public override IEnumerator SolveSpellLight(Player caster, Cell target, Map map) {
        runningSpells++;
        //Map.MovePlayerToAdjacentCell(caster,target);
        runningSpells--;
        yield return null;
    }
    public override IEnumerator SolveSpellHeavy(Player caster, Cell target, Map map) {
        runningSpells++;
        //Map.MovePlayerToAdjacentCell(caster, target);
        runningSpells--;
        yield return null;
    }

    public override List<Cell> GetEffectAreaPreviewHeavy(Player caster, Cell target, Map map) {
        List<Cell> affectedCells = null;
        if (caster.nextCell.x > target.x) { // aim left
            affectedCells = map.GetCellsRightLine(target, 0, 2);
        } else if (caster.nextCell.x < target.x) { // aim right
            affectedCells = map.GetCellsLeftLine(target, 0, 2);
        } else if (caster.nextCell.y > target.y) { // aim down
            affectedCells = map.GetCellsTopLine(target, 0, 2);
        } else if (caster.nextCell.y < target.y) { // aim up
            affectedCells = map.GetCellsBottomLine(target, 0, 2);
        } else throw new System.Exception("Can't aim at the caster cell!");
        return affectedCells;
    }
    public override List<Cell> GetEffectAreaPreviewLight(Player caster, Cell target, Map map) {
        List<Cell> affectedCells = null;
        if (caster.nextCell.x > target.x) { // aim left
            affectedCells = map.GetCellsRightLine(target, 0, 2);
        } else if (caster.nextCell.x < target.x) { // aim right
            affectedCells = map.GetCellsLeftLine(target, 0, 2);
        } else if (caster.nextCell.y > target.y) { // aim down
            affectedCells = map.GetCellsTopLine(target, 0, 2);
        } else if (caster.nextCell.y < target.y) { // aim up
            affectedCells = map.GetCellsBottomLine(target, 0, 2);
        } else throw new System.Exception("Can't aim at the caster cell!");
        return affectedCells;
    }
}
public class SpellHeal : SpellBase {
    public SpellHeal() {
        name = "Soin";
        description = "Rend 8 points de vie / Rend 12 points de vie.";
        iconPath = "Sprites/Spells/regeneration";
        rangeLight = new AreaProfile(AreaType.Circle, 0, 0);
        rangeHeavy = new AreaProfile(AreaType.Circle, 0, 0);
        cooldownLight = 4;
        cooldownHeavy = 4;
    }
    public override IEnumerator SolveSpellLight(Player caster, Cell target, Map map) {
        runningSpells++;
        caster.Heal(8);
        runningSpells--;
        yield return null;
    }
    public override IEnumerator SolveSpellHeavy(Player caster, Cell target, Map map) {
        runningSpells++;
        caster.Heal(12);
        runningSpells--;
        yield return null;
    }
}
