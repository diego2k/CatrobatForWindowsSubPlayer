﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Catrobat.Core.Misc.Helpers;

namespace Catrobat.Core.Objects.Variables
{
    public class UserVariableReference : DataObject
    {
        private string _reference;

        private UserVariable _userVariable;
        public UserVariable UserVariable
        {
            get { return _userVariable; }
            set
            {
                if (_userVariable == value)
                {
                    return;
                }

                _userVariable = value;
                RaisePropertyChanged();
            }
        }


        public UserVariableReference()
        {
        }

        public UserVariableReference(XElement xElement)
        {
            LoadFromXML(xElement);
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            _reference = xRoot.Attribute("reference").Value;
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("userVariableRef");

            xRoot.Add(new XAttribute("reference", ReferenceHelper.GetReferenceString(this)));

            return xRoot;
        }

        public DataObject Copy()
        {
            var newUserVariableRef = new UserVariableReference();
            newUserVariableRef.UserVariable = _userVariable;

            return newUserVariableRef;
        }

        internal override void LoadReference()
        {
            UserVariable = ReferenceHelper.GetReferenceObject(this, _reference) as UserVariable;
        }
    }
}
