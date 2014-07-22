﻿using System.Xml.Linq;
using Catrobat.Data.Xml.XmlObjects.Formulas;

namespace Catrobat.Data.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlPlaceAtBrick : XmlBrick
    {
        public XmlFormula XPosition { get; set; }

        public XmlFormula YPosition { get; set; }

        public XmlPlaceAtBrick() {}

        public XmlPlaceAtBrick(XElement xElement) : base(xElement) {}

        public override void LoadFromXml(XElement xRoot)
        {
            XPosition = new XmlFormula(xRoot.Element("xPosition"));
            YPosition = new XmlFormula(xRoot.Element("yPosition"));
        }

        public override XElement CreateXml()
        {
            var xRoot = new XElement("placeAtBrick");

            var xVariable1 = new XElement("xPosition");
            xVariable1.Add(XPosition.CreateXml());
            xRoot.Add(xVariable1);

            var xVariable2 = new XElement("yPosition");
            xVariable2.Add(YPosition.CreateXml());
            xRoot.Add(xVariable2);

            return xRoot;
        }

        public override void LoadReference()
        {
            if (XPosition != null)
                XPosition.LoadReference();
            if (YPosition != null)
                YPosition.LoadReference();
        }
    }
}