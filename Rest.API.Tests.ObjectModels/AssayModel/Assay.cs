using System;

namespace Rest.API.Tests.ObjectModels.AssayModel
{
    public class Assay
    {
        public String AssayId { get; set; }
        public String Name { get; set; }
        public String Percent { get; set; }
        public String SpecificGravity { get; set; }
    }
}
