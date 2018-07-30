using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using UnityEngine;

public class Trait
{
    public string traitName { get; set; }
    public int attack { get; set; }
    public int defense { get; set; }
    public int magic { get; set; }
    public int strategy { get; set; }
    public int showcreatureship { get; set; }
    public int comebackitude { get; set; }

    public float health { get; set; }
    public float energy { get; set; }
}
