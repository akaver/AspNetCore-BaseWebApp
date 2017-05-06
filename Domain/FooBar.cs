using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{

    // Dummy entity, just for testing
    public class FooBar
    {
        public int FooBarId { get; set; }

        public string StringValue { get; set; }
        public int IntValue { get; set; }


        public int ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }


    }
}
