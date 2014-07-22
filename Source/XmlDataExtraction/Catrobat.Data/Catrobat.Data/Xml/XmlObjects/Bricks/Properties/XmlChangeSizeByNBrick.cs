﻿using System.Xml.Linq;
using Catrobat.Data.Xml.XmlObjects.Formulas;

namespace Catrobat.Data.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlChangeSizeByNBrick : XmlBrick
    {
        public XmlFormula Size { get; set; }

        public XmlChangeSizeByNBrick() {}

        public XmlChangeSizeByNBrick(XElement xElement) : base(xElement) {}

        public override void LoadFromXml(XElement xRoot)
        {
            Size = new XmlFormula(xRoot.Element("size"));
        }

        public override XElement CreateXml()
        {
            var xRoot = new XElement("changeSizeByNBrick");

            var xVariable = new XElement("size");
            xVariable.Add(Size.CreateXml());
            xRoot.Add(xVariable);

            return xRoot;
        }

        public override void LoadReference()
        {
            if (Size != null)
                Size.LoadReference();
        }
    }
}