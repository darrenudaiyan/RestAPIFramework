using Rest.API.Tests.ObjectModels.AssayModel;
using System;
using System.Collections.Generic;

namespace Rest.API.Tests.ObjectModels.WhiskeyModel
{
    public class Whiskey
    {
        public String WhiskeyId { get; set; }
        public String Name { get; set; }
        public List<Assay> Assays { get; set; }
    }
}
