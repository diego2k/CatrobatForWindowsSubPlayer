﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml.Linq;

namespace Catrobat.Core.Objects.Costumes
{
    public class CostumeList : DataObject
    {
        public ObservableCollection<Costume> Costumes { get; set; }

        public CostumeList()
        {
            Costumes = new ObservableCollection<Costume>();
        }

        public CostumeList(XElement xElement)
        {
            LoadFromXML(xElement);
        } 

        internal override void LoadFromXML(XElement xRoot)
        {
            Costumes = new ObservableCollection<Costume>();
            foreach (XElement element in xRoot.Elements("look"))
            {
                Costumes.Add(new Costume(element));
            }
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("lookList");

            foreach (Costume costume in Costumes)
            {
                xRoot.Add(costume.CreateXML());
            }

            return xRoot;
        }

        public DataObject Copy()
        {
            var newCostumeDataList = new CostumeList();
            foreach (Costume costume in Costumes)
            {
                newCostumeDataList.Costumes.Add(costume.Copy() as Costume);
            }

            return newCostumeDataList;
        }

        public void Delete()
        {
            foreach (Costume costume in Costumes)
            {
                costume.Delete();
            }
        }
    }
}