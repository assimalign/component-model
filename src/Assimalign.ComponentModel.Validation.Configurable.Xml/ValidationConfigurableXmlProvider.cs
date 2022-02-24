using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Assimalign.ComponentModel.Validation.Configurable;

public sealed class ValidationConfigurableXmlProvider<T> : IValidationConfigurableProvider, IXmlSerializable
{
    private readonly ValidationConfigurableJsonProfile<T> profile;


    public ValidationConfigurableXmlProvider(ValidationConfigurableJsonProfile<T> profile)
    {
       
        this.profile = profile;
    }


    public IValidationProfile GetProfile() => this.profile;

    public XmlSchema GetSchema()
    {
        throw new NotImplementedException();
    }

    public void ReadXml(XmlReader reader)
    {
        reader.
    }

    public bool TryGetProfile(Type type, out IValidationProfile profile)
    {
        if ((IValidationProfile)this.profile == type)
        {
            profile = this.profile;
            return true;
        }
        else
        {
            profile=null;
            return false;
        }
    }

    public void WriteXml(XmlWriter writer)
    {
       writer.
    }
}