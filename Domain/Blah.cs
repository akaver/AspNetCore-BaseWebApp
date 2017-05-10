using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    // Dummy entity, just for testing
    public class Blah
    {
        public int BlahId { get; set; }
        public string BlahValue { get; set; }


        // there are several navigation properties on other side, so we have to show specifically which one to use
        [InverseProperty(property: nameof(FooBar.BlahOne))]
        public List<FooBar> BlahOneFooBars { get; set; }

        [InverseProperty(property: nameof(FooBar.BlahTwo))]
        public List<FooBar> BlahTwoFooBars { get; set; }

        [InverseProperty(property: nameof(FooBar.BlahThree))]
        public List<FooBar> BlahThreeFooBars { get; set; }
    }
}
